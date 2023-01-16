// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================

using System;
using System.CodeDom;
using System.Runtime.CompilerServices;
using System.Text;

namespace Vavavoom.SIMB.Game.Library.Crypto.LZ4
{
	/// <summary>
	///     LZ4 codec selecting best implementation depending on platform.
	/// </summary>
	public static partial class LZ4Codec
	{
		#region fields

		/// <summary>Encoding service.</summary>
		private static readonly ILZ4Service Encoder;

		/// <summary>Encoding service for HC algorithm.</summary>
		private static readonly ILZ4Service EncoderHC;

		/// <summary>Decoding service.</summary>
		private static readonly ILZ4Service Decoder;

		// ReSharper disable InconsistentNaming

		// safe c#
		private static ILZ4Service _service_S32;
		private static ILZ4Service _service_S64;

		// ReSharper restore InconsistentNaming
		
		//cache
		private const int _DecodeInitSize = 1024;
		private static byte[] _DecodeBuff = new byte[_DecodeInitSize];

		#endregion

		#region initialization

		/// <summary>Initializes the <see cref="LZ4Codec" /> class.</summary>
		static LZ4Codec()
		{
			// NOTE: this method exploits the fact that assemblies are loaded first time they
			// are needed so we can safely try load and handle if not loaded
			// I may change in future versions of .NET

			_service_S32 = TryService<Services.Safe32LZ4Service>();
			_service_S64 = TryService<Services.Safe64LZ4Service>();

			ILZ4Service encoder, decoder, encoderHC;
			SelectCodec(out encoder, out decoder, out encoderHC);

			Encoder = encoder;
			Decoder = decoder;
			EncoderHC = encoderHC;

			if (Encoder == null || Decoder == null)
			{
				throw new NotSupportedException("No LZ4 compression service found");
			}
		}

		private static void SelectCodec(out ILZ4Service encoder, out ILZ4Service decoder, out ILZ4Service encoderHC)
		{
			// refer to: http://lz4net.codeplex.com/wikipage?title=Performance%20Testing for explanation about this order
			// feel free to change preferred order, just don't do it willy-nilly back it up with some evidence
			// it has been tested for Intel on Microsoft .NET only but looks reasonable for Mono as well
			if (IntPtr.Size == 4)
			{
				encoder =
					_service_S32 ??
					_service_S64;
				decoder =
					_service_S64 ??
					_service_S32;
				encoderHC =
					_service_S32 ??
					_service_S64;
			}
			else
			{
				encoder =
					_service_S32 ??
					_service_S64;
				decoder =
					_service_S64 ??
					_service_S32;
				encoderHC =
					_service_S32 ??
					_service_S64;
			}
		}

		/// <summary>Performs the quick auto-test on given compression service.</summary>
		/// <param name="service">The service.</param>
		/// <returns>A service or <c>null</c> if it failed.</returns>
		private static ILZ4Service AutoTest(ILZ4Service service)
		{
			const string loremIpsum =
				"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut " +
				"labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
				"laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in " +
				"voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat " +
				"non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

			// generate some well-known array of bytes
			const string inputText = loremIpsum + loremIpsum + loremIpsum + loremIpsum + loremIpsum;
			var original = Encoding.UTF8.GetBytes(inputText);

			// LZ4 test
			{
				// compress it
				var encoded = new byte[MaximumOutputLength(original.Length)];
				var encodedLength = service.Encode(original, 0, original.Length, encoded, 0, encoded.Length);
				if (encodedLength < 0)
					return null;

				// decompress it (knowing original length)
				var decoded = new byte[original.Length];
				var decodedLength1 = service.Decode(encoded, 0, encodedLength, decoded, 0, decoded.Length, true);
				if (decodedLength1 != original.Length)
					return null;
				var outputText1 = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
				if (outputText1 != inputText)
					return null;

				// decompress it (not knowing original length)
				var decodedLength2 = service.Decode(encoded, 0, encodedLength, decoded, 0, decoded.Length, false);
				if (decodedLength2 != original.Length)
					return null;
				var outputText2 = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
				if (outputText2 != inputText)
					return null;
			}

			// LZ4HC
			{
				// compress it
				var encoded = new byte[MaximumOutputLength(original.Length)];
				var encodedLength = service.EncodeHC(original, 0, original.Length, encoded, 0, encoded.Length);
				if (encodedLength < 0)
					return null;

				// decompress it (knowing original length)
				var decoded = new byte[original.Length];
				var decodedLength1 = service.Decode(encoded, 0, encodedLength, decoded, 0, decoded.Length, true);
				if (decodedLength1 != original.Length)
					return null;
				var outputText1 = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
				if (outputText1 != inputText)
					return null;

				// decompress it (not knowing original length)
				var decodedLength2 = service.Decode(encoded, 0, encodedLength, decoded, 0, decoded.Length, false);
				if (decodedLength2 != original.Length)
					return null;
				var outputText2 = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
				if (outputText2 != inputText)
					return null;
			}

			return service;
		}

		/// <summary>Tries to create a specified <seealso cref="ILZ4Service" /> and tests it.</summary>
		/// <typeparam name="T">Concrete <seealso cref="ILZ4Service" /> type.</typeparam>
		/// <returns>A service if succeeded or <c>null</c> if it failed.</returns>
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ILZ4Service TryService<T>()
			where T: ILZ4Service, new()
		{
			try
			{
				return AutoTest(new T());
			}
			catch (Exception)
			{
				// I could use Trace here but portable profile does not have Trace
				return null;
			}
		}

		#endregion

		#region public interface

		/// <summary>Gets the name of selected codec(s).</summary>
		/// <value>The name of the codec.</value>
		public static string CodecName
		{
			get
			{
				return string.Format(
					"{0}/{1}/{2}HC",
					Encoder == null ? "<none>" : Encoder.CodecName,
					Decoder == null ? "<none>" : Decoder.CodecName,
					EncoderHC == null ? "<none>" : EncoderHC.CodecName);
			}
		}

		/// <summary>Get maximum output length.</summary>
		/// <param name="inputLength">Input length.</param>
		/// <returns>Output length.</returns>
		public static int MaximumOutputLength(int inputLength)
		{
			return inputLength + (inputLength / 255) + 16;
		}

		#region Encode

		/// <summary>Encodes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <param name="output">The output.</param>
		/// <param name="outputOffset">The output offset.</param>
		/// <param name="outputLength">Length of the output.</param>
		/// <returns>Number of bytes written.</returns>
		public static int Encode(
			byte[] input,
			int inputOffset,
			int inputLength,
			byte[] output,
			int outputOffset,
			int outputLength)
		{
			return Encoder.Encode(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}

		/// <summary>Encodes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <returns>Compressed buffer.</returns>
		public static byte[] Encode(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength < 0)
				inputLength = input.Length - inputOffset;

			if (input == null)
				throw new ArgumentNullException("input");
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");

			var result = new byte[MaximumOutputLength(inputLength)];
			var length = Encode(input, inputOffset, inputLength, result, 0, result.Length);

			if (length != result.Length)
			{
				if (length < 0)
					throw new InvalidOperationException("Compression has been corrupted");
				var buffer = new byte[length];
				Buffer.BlockCopy(result, 0, buffer, 0, length);
				return buffer;
			}
			return result;
		}

		/// <summary>Encodes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <param name="output">The output.</param>
		/// <param name="outputOffset">The output offset.</param>
		/// <param name="outputLength">Length of the output.</param>
		/// <returns>Number of bytes written.</returns>
		public static int EncodeHC(
			byte[] input,
			int inputOffset,
			int inputLength,
			byte[] output,
			int outputOffset,
			int outputLength)
		{
			return (EncoderHC ?? Encoder)
				.EncodeHC(input, inputOffset, inputLength, output, outputOffset, outputLength);
		}

		/// <summary>Encodes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <returns>Compressed buffer.</returns>
		public static byte[] EncodeHC(byte[] input, int inputOffset, int inputLength)
		{
			if (inputLength < 0)
				inputLength = input.Length - inputOffset;

			if (input == null)
				throw new ArgumentNullException("input");
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");

			var result = new byte[MaximumOutputLength(inputLength)];
			var length = EncodeHC(input, inputOffset, inputLength, result, 0, result.Length);

			if (length != result.Length)
			{
				if (length < 0)
					throw new InvalidOperationException("Compression has been corrupted");
				var buffer = new byte[length];
				Buffer.BlockCopy(result, 0, buffer, 0, length);
				return buffer;
			}
			return result;
		}

		#endregion

		#region Decode

		/// <summary>Decodes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <param name="output">The output.</param>
		/// <param name="outputOffset">The output offset.</param>
		/// <param name="outputLength">Length of the output.</param>
		/// <param name="knownOutputLength">Set it to <c>true</c> if output length is known.</param>
		/// <returns>Number of bytes written.</returns>
		public static int Decode(
			byte[] input,
			int inputOffset,
			int inputLength,
			byte[] output,
			int outputOffset,
			int outputLength = 0,
			bool knownOutputLength = false)
		{
			return Decoder.Decode(input, inputOffset, inputLength, output, outputOffset, outputLength, knownOutputLength);
		}

		/// <summary>Decodes the specified input.</summary>
		/// <param name="input">The input.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <param name="outputLength">Length of the output.</param>
		/// <returns>Decompressed buffer.</returns>
		public static byte[] Decode(byte[] input, int inputOffset, int inputLength, int outputLength, out int decodeLen)
		{
			if (inputLength < 0)
				inputLength = input.Length - inputOffset;

			if (input == null)
				throw new ArgumentNullException("input");
			if (inputOffset < 0 || inputOffset + inputLength > input.Length)
				throw new ArgumentException("inputOffset and inputLength are invalid for given input");

			decodeLen = outputLength;
			ResizeDecodeBuff(outputLength);
			
			var length = Decode(input, inputOffset, inputLength, _DecodeBuff, 0, outputLength, true);
			if (length != outputLength)
				throw new ArgumentException("outputLength is not valid");
			
			return _DecodeBuff;
		}

		private static void ResizeDecodeBuff(int newSize)
		{
			if (null == _DecodeBuff)
			{
				return;;
			}

			if (newSize < _DecodeBuff.Length)
			{
				return;
			}

			while (_DecodeBuff.Length < newSize)
			{
				Array.Resize(ref _DecodeBuff, _DecodeBuff.Length + _DecodeInitSize);
			}
		}

		#endregion

		#endregion

		#region Wrap

		private const int WRAP_OFFSET_0 = 0;
		private const int WRAP_OFFSET_4 = sizeof(int);
		private const int WRAP_OFFSET_8 = 2 * sizeof(int);
		private const int WRAP_LENGTH = WRAP_OFFSET_8;

		/// <summary>Sets uint32 value in byte buffer.</summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="value">The value.</param>
		private static void Poke4(byte[] buffer, int offset, uint value)
		{
			buffer[offset + 0] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			buffer[offset + 2] = (byte)(value >> 16);
			buffer[offset + 3] = (byte)(value >> 24);
		}

		/// <summary>Gets uint32 from byte buffer.</summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The offset.</param>
		/// <returns>The value.</returns>
		private static uint Peek4(byte[] buffer, int offset)
		{
			// NOTE: It's faster than BitConverter.ToUInt32 (suprised? me too)
			return
				// ReSharper disable once RedundantCast
				((uint)buffer[offset]) |
				((uint)buffer[offset + 1] << 8) |
				((uint)buffer[offset + 2] << 16) |
				((uint)buffer[offset + 3] << 24);
		}

		/// <summary>Compresses and wraps given input byte buffer.</summary>
		/// <param name="inputBuffer">The input buffer.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <param name="highCompression">if set to <c>true</c> uses high compression.</param>
		/// <returns>Compressed buffer.</returns>
		/// <exception cref="System.ArgumentException">inputBuffer size of inputLength is invalid</exception>
		private static byte[] Wrap(byte[] inputBuffer, int inputOffset, int inputLength, bool highCompression)
		{
			inputLength = Math.Min(inputBuffer.Length - inputOffset, inputLength);
			if (inputLength < 0)
				throw new ArgumentException("inputBuffer size of inputLength is invalid");
			if (inputLength == 0)
				return new byte[WRAP_LENGTH];

			var outputLength = inputLength; // MaximumOutputLength(inputLength);
			var outputBuffer = new byte[outputLength];

			outputLength = highCompression
				? EncodeHC(inputBuffer, inputOffset, inputLength, outputBuffer, 0, outputLength)
				: Encode(inputBuffer, inputOffset, inputLength, outputBuffer, 0, outputLength);

			byte[] result;

			if (outputLength >= inputLength || outputLength <= 0)
			{
				result = new byte[inputLength + WRAP_LENGTH];
				Poke4(result, WRAP_OFFSET_0, (uint)inputLength);
				Poke4(result, WRAP_OFFSET_4, (uint)inputLength);
				Buffer.BlockCopy(inputBuffer, inputOffset, result, WRAP_OFFSET_8, inputLength);
			}
			else
			{
				result = new byte[outputLength + WRAP_LENGTH];
				Poke4(result, WRAP_OFFSET_0, (uint)inputLength);
				Poke4(result, WRAP_OFFSET_4, (uint)outputLength);
				Buffer.BlockCopy(outputBuffer, 0, result, WRAP_OFFSET_8, outputLength);
			}

			return result;
		}

		/// <summary>Compresses and wraps given input byte buffer.</summary>
		/// <param name="inputBuffer">The input buffer.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <returns>Compressed buffer.</returns>
		/// <exception cref="System.ArgumentException">inputBuffer size of inputLength is invalid</exception>
		public static byte[] Wrap(byte[] inputBuffer, int inputOffset = 0, int inputLength = int.MaxValue)
		{
			return Wrap(inputBuffer, inputOffset, inputLength, false);
		}

		/// <summary>Compresses (with high compression algorithm) and wraps given input byte buffer.</summary>
		/// <param name="inputBuffer">The input buffer.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <param name="inputLength">Length of the input.</param>
		/// <returns>Compressed buffer.</returns>
		/// <exception cref="System.ArgumentException">inputBuffer size of inputLength is invalid</exception>
		public static byte[] WrapHC(byte[] inputBuffer, int inputOffset = 0, int inputLength = int.MaxValue)
		{
			return Wrap(inputBuffer, inputOffset, inputLength, true);
		}

		/// <summary>Unwraps the specified compressed buffer.</summary>
		/// <param name="inputBuffer">The input buffer.</param>
		/// <param name="inputOffset">The input offset.</param>
		/// <returns>Uncompressed buffer.</returns>
		/// <exception cref="System.ArgumentException">
		///     inputBuffer size is invalid or inputBuffer size is invalid or has been corrupted
		/// </exception>
		public static byte[] Unwrap(byte[] inputBuffer, int inputOffset = 0)
		{
			var inputLength = inputBuffer.Length - inputOffset;
			if (inputLength < WRAP_LENGTH)
				throw new ArgumentException("inputBuffer size is invalid");

			var outputLength = (int)Peek4(inputBuffer, inputOffset + WRAP_OFFSET_0);
			inputLength = (int)Peek4(inputBuffer, inputOffset + WRAP_OFFSET_4);
			if (inputLength > inputBuffer.Length - inputOffset - WRAP_LENGTH)
				throw new ArgumentException("inputBuffer size is invalid or has been corrupted");

			byte[] result;

			if (inputLength >= outputLength)
			{
				result = new byte[inputLength];
				Buffer.BlockCopy(inputBuffer, inputOffset + WRAP_OFFSET_8, result, 0, inputLength);
			}
			else
			{
				result = new byte[outputLength];
				Decode(inputBuffer, inputOffset + WRAP_OFFSET_8, inputLength, result, 0, outputLength, true);
			}

			return result;
		}

		#endregion
	}
}
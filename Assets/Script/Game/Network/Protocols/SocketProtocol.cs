// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 16:02:39
// ========================================================

//套接字+协议

using System;
using SEngine.Net;
using Google.Protobuf;
using UnityEngine;
using Vavavoom.SIMB.Game.Interface;
using Vavavoom.SIMB.Game.Library.Crypto;
using XLua;
using Random = UnityEngine.Random;
using SEngine;

namespace Game.Network
{
    [BlackList]
    public class SocketProtocol : SingletonT<SocketProtocol>, ISocketProtocol
    {
        #region ###Property###

        private ISerialize _Serialize;

        private IDeserialize _Deserialize;

        private ICrypto _Crypto;

        private IZip _Zip;

        private bool _Inited = false;

        private FrameHeadless _FrameHeadless;

        private Frame32 _Frame32;

        #endregion

        public void Init(ISerialize serialize, IDeserialize deserialize, ICrypto crypto, IZip zip)
        {
            _Serialize = serialize;
            _Deserialize = deserialize;
            _Crypto = crypto;
            _Zip = zip;
            _FrameHeadless = new FrameHeadless();
            _Frame32 = new Frame32();
            _FrameHeadless.Init(512);
            _Frame32.Init(512);
            

            if (null != _Serialize
                && null != _Deserialize
                && null != _Crypto
                && null != _Zip
            )
            {
                _Inited = true;   
            }
        }

        public void UnInit()
        {
            _Serialize = null;
            _Deserialize = null;
            _Crypto = null;
            _Zip = null;
            _FrameHeadless = null;
            _Frame32 = null;
            _Inited = false;
        }
        
        public Frame EncodeMessage(ProtocolEnum cmd, IMessage message, bool kcp)
        {
            if (false == _Inited)
            {
                return null;
            }
            
            byte[] data = _Serialize.Serialize(message);

            Frame f;
            if (true == kcp)
            {
               f = _FrameHeadless;
            }
            else
            {
               f = _Frame32;
            }
            
            f.Reset();

            var compressFlag = (data.Length > 1024);
            var controlFlag = 0;
            controlFlag = (controlFlag | (compressFlag ? 1 : 0) | (NetworkKit.IsIOSMJ ? 2 : 0));

            f.PutByte((byte)controlFlag);
            f.PutShort((short)cmd);
            
            if (true == NetworkKit.IsIOSMJ)
            {
                byte[] mjBytes = new byte[256];

                for (int i=0; i<mjBytes.Length; i++)
                {
                    mjBytes[i] = (byte)Random.Range(0, 255);
                }
                
                f.PutBytes(mjBytes);
            }
            
            byte[] encryptedData = _Crypto.Encryption(data);
            byte[] compressData = null;
            if (compressFlag)
            {
                compressData = _Zip.Encode((short)cmd, encryptedData);
            }
            else
            {
                compressData = encryptedData;
            }

            f.PutBytes(compressData);
            
            f.End();

            return f;
        }

        public Frame EncodeMessage(short cmd, byte[] data, bool kcp)
        {
            Frame f = null;
            if (kcp)
            {
               f = _FrameHeadless;
            }
            else
            {
               f = _Frame32;
            }
            
            f.Reset();

            var compressFlag = (data.Length > 1024);
            var controlFlag = 0;
            controlFlag = (controlFlag | (compressFlag ? 1 : 0) | (NetworkKit.IsIOSMJ ? 2 : 0));
            
            f.PutByte((byte)controlFlag);
            f.PutShort(cmd);
            
            if (true == NetworkKit.IsIOSMJ)
            {
                byte[] mjBytes = new byte[256];

                for (int i=0; i<mjBytes.Length; i++)
                {
                    mjBytes[i] = (byte)Random.Range(0, 255);
                }
                
                f.PutBytes(mjBytes);
            }

            byte[] encryptedData = _Crypto.Encryption(data);
            byte[] compressData = null;
            if (compressFlag)
            {
                compressData = _Zip.Encode(cmd, encryptedData);
            }
            else
            {
                compressData = encryptedData;
            }

            f.PutBytes(compressData);
            f.End();
            
            return f;
        } 

        public object DecodeMessage(Type type, byte[] data, int len, ushort controlFlag = 0)
        {
            if (false == _Inited)
            {
                return false;
            }
            
            var msg = Activator.CreateInstance(type) as IMessage;
            if (null == msg)
            {
                return false;
            }
            
            var decryptionData = _Crypto.Decryption(data);
            if (null == decryptionData)
            {
                return false;
            }
            
            _Deserialize.Deserialize(msg, decryptionData, 0, len);

            string objStr = string.Empty;

            if (true == Debug.isDebugBuild)
            {
                objStr = msg.ToString();
            }
            
            #if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
            if (objStr.Length > 10000)
            {
                objStr = objStr.Substring(0, 10000);
            }
            #endif
            
            Debug.Log($"SocketRequest Response MessageData: {objStr}");
            
            return msg;
        }
        
        public byte[] DecodeCompress(short cmd, byte[] compressData, int len, out int decodeLen)
        {
            if (false == _Inited)
            {
                decodeLen = 0;
                return null;
            }
            
            var data = _Zip.Decode(cmd, compressData, len, out decodeLen);
            return data;
        }
    }
}
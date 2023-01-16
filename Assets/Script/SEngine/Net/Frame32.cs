using System;
using System.Text;

namespace SEngine.Net
{
	public class Frame32 : Frame
	{
		public Frame32()
		{
			payload.Clear();
		}

		public Frame Init(int len)
		{
			Len = len;
			return this;
		}

		public override void Reset()
		{
			end = false;
			payload = new ByteBuf (Len);
			payload.WriteInt(0);
		}

		/**
		 * 写入一个字符串
		 **/ 
		public override Frame PutString(string s,byte[] ks)
		{
			if (!end)
			{
				byte[] content = Encoding.UTF8.GetBytes (s.ToCharArray ());
				this.payload.WriteInt (content.Length);
				this.payload.WriteBytes (content);
			}
			return this;
		}

		/**
		 * 封包
		 **/ 
		public override void End()
		{
			int reader = payload.ReaderIndex();
			int writer = payload.WriterIndex();
		    int l = writer - reader - 4; //数据长度
		    payload.WriterIndex(reader);
		    payload.WriteInt(l);
		    payload.WriterIndex(writer);
			end = true;
		}
	}
}


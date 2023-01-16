using System;

namespace SEngine.Net
{
    public class LV4Protocol : Protocol
    {
        private int status;
        private int h;
        private int hl;
        private int lh;
        private int l;
        private int len;
        private ByteBuf frame;

        public LV4Protocol()
        {

        }
        /**
		 * 分帧逻辑
		 * 
		 **/
        public ByteBuf TranslateFrame(ref ByteBuf src)
        {
            while (src.ReadableBytes() > 0)
            {
                switch (status)
                {
                    case 0:
                        h = src.ReadByte();
                        status = 1;
                        break;
                    case 1:
                        hl = src.ReadByte();
                        status = 2;
                        break;
                    case 2:
                        lh = src.ReadByte();
                        status = 3;
                        break;
                    case 3:
                        l = src.ReadByte();
                        len = h << 24 | hl << 16 | lh << 8 | l;
                        //frame.Release();
                        frame = new ByteBuf(len + 4);
                        frame.WriteInt(len);
                        status = 4;
                        break;
                    case 4:
                        int min = frame.WritableBytes();
                        min = src.ReadableBytes() < min ? src.ReadableBytes() : min;
                        if (min > 0)
                        {
                            frame.WriteBytes(ref src, min);
                        }
                        if (frame.WritableBytes() <= 0)
                        {
                            status = 0;
                            return frame;
                        }
                        break;
                }
            }
            
            return new ByteBuf();
        }
        /**
		 * 头部长度
		 * 
		 */
        public int HeaderLen()
        {
            return 4;
        }
    }
}


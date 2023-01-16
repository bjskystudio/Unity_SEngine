using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEngine.Net
{
    public class FrameHeadless : Frame 
    {
        public FrameHeadless()
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
            payload = new ByteBuf(Len);
            payload.WriteShort(0);
        }

        public override void End()
        {
            end = true;
        }
    }
}

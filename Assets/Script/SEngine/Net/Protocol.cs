using System;

namespace SEngine.Net
{
	public interface Protocol
	{
		ByteBuf TranslateFrame (ref ByteBuf src); 
	    int HeaderLen();
	}
}


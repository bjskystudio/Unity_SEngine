using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEngine.Net
{
    public interface ISocket
    {
        Protocol GetProtocol();
        string GetIP();
        int GetPort();

        void Update();
    }
}

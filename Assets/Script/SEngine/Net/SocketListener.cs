using System;

namespace SEngine.Net
{
	public abstract  class SocketListener
	{
	    public class SocketCloseEventArg : EventArgs
        {
	        public ISocket Socket { get; set; }
            public bool FromRemote { get; set; }
	    }
	    public class SocketErrorEventArg : EventArgs
	    {
	        public ISocket Socket { get; set; }
	        public string Error { get; set; }
        }

	    public event Action<ISocket> SocketOpenedEvent;
        public event Action<ISocket> SocketConnectTimetoutEvent;
        public event EventHandler<SocketCloseEventArg> SocketClosedEvent;
	    public event EventHandler<SocketErrorEventArg> SocketErrorEvent;
	    public event Action<int, ISocket, string> SocketConnectCollectEvent;
		public bool ForceClose;

	    protected virtual void OnSocketOpened(ISocket us)
	    {
		    SocketConnectCollectEvent?.Invoke(1, us, "Connect Succ!");
	        SocketOpenedEvent?.Invoke(us);
        }

        protected virtual void OnSockeConnectTimeout(ISocket us)
        {
	        SocketConnectCollectEvent?.Invoke(3, us, "Is connecting!");
            SocketConnectTimetoutEvent?.Invoke(us);
        }

        public virtual void OnSocketClosed(ISocket us, bool fromRemote)
	    {
		    SocketConnectCollectEvent?.Invoke(2, us, "Connect Closed!");
			SocketClosedEvent?.Invoke(this, new SocketCloseEventArg { Socket = us, FromRemote = fromRemote });
	    }

	    protected virtual void OnSocketError(ISocket us, string err)
	    {
		    SocketConnectCollectEvent?.Invoke(4, us, "Connect error: " + err);
	        SocketErrorEvent?.Invoke(this, new SocketErrorEventArg { Socket = us, Error = err });
        }

        public abstract void OnMessage(ISocket us,ByteBuf bb);
		public abstract void OnClose(ISocket us,bool fromRemote);
		public abstract void OnIdle(ISocket us);
		public abstract void OnOpen(ISocket us);
		public abstract void OnError(ISocket us,string err);

	  
	}
}


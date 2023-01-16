using UnityEngine;
using System.Collections;

namespace Game.Network 
{
	public enum QueueType
	{
		CMD,
		ERROR,
	}
	public struct QueueItem
	{
		public QueueType Type;
		public int CMD;
		public MessageData MsgData;
		public string Msg;
	}
}

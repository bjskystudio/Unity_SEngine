// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/09 19:06:33
// ========================================================

//网络消息队列

using System;
using System.Collections.Generic;
using SEngine.Net;
using Google.Protobuf;
using XLua;
using SEngine;

namespace Game.Network
{
    [BlackList]
	public class MessageQueueHandler : MonoSingleton<MessageQueueHandler>
	{
		public class SocketCloseItem
		{
			public SocketListener SocketListener;
			public ISocket Socket;
			public bool FromRemote;
		}
		
		private Queue<QueueItem> _MessageQueue = new Queue<QueueItem>();
		private Queue<QueueItem> _ErrorQueue = new Queue<QueueItem>();
		
		//每帧处理消息的上限(毫秒数6/33)
		private const int MAX_INVOKE_TIME = 6;

		private const int _MsgInitSize = 1024;
		private byte[] _MsgContect;
		private byte[] _MsgMJTemp;
		private int _MsgContentLength = 0;
		private int _MsgMJTempLength = 0;

		private bool _Inited;

		[CSharpCallLua]
		public delegate void LuaOnRecvMessage(int cmd, byte[] msg);
		/// <summary>
		/// 绑定到lua的消息委托
		/// </summary>
		[CSharpCallLua]
		public static LuaOnRecvMessage luaOnRecvMessage;

		protected override void Init()
		{
			_MsgContect = new byte[_MsgInitSize];
			_MsgMJTemp = new byte[_MsgInitSize];

            //FrameUpdateRegister.RegisterUpdate(this, Update);

            //if (null != _LuaSystem)
            //{
            //	var cmdConfig = KResourceKit.AssetBundleLoadAsset<TextAsset>("Lua/Protocol/CmdConfig.txt");
            //	if (false == ReferenceEquals(null, cmdConfig))
            //	{
            //		_LuaSystem.InitCmdConfigDic(cmdConfig.text);
            //	}

            _Inited = true;
            //}
        }

		public void UnInit()
		{
			lock (_MessageQueue)
			{
				_MessageQueue.Clear();
			}

			lock (_ErrorQueue)
			{
				_ErrorQueue.Clear();
			}
			
			_MsgContect = null;
			_MsgMJTemp = null;
			
			//FrameUpdateRegister.UnregisterUpdate(this);
		}
		
		public void PushQueue(int cmd, MessageData msgData) 
		{
			if (false == _Inited)
			{
				return;
			}
			
			lock(_MessageQueue) 
			{
				var queue = new QueueItem {Type = QueueType.CMD, CMD = cmd, MsgData = msgData};
				
				_MessageQueue.Enqueue(queue);
			}
		}
		
		public void PushError(string msg, short state = 0) 
		{
			if (false == _Inited)
			{
				return;
			}
			
			lock(_ErrorQueue)
			{
				var queue = new QueueItem {Type = QueueType.ERROR, Msg =  msg};
				
				_ErrorQueue.Enqueue(queue);
			}
		}
        private void Update()
        {
			if (false == _Inited)
			{
				return;
			}
			
			lock(_MessageQueue)
			{
				var tkCnt = Environment.TickCount;
				for(int i = 0; i < _MessageQueue.Count; ++i)
				{
					var a = Environment.TickCount - tkCnt;
					if (Environment.TickCount - tkCnt > MAX_INVOKE_TIME)
					{
						break;
					}

					QueueItem queueItem = _MessageQueue.Dequeue();
					//直接分发lua
					DispatchMessageForLua(queueItem);

//					bool processMsg = false;

//                    // Get Cmd Config Type
//                    // 0: 只在Lua执行
//                    // 1: 只在C#执行
//                    // 2: Lua后C#
//                    // 3: 在C#后Lua
//                    int cmdConfigType = _LuaSystem.GetCmdConfigType(queueItem.CMD);

//                    // 修改为先执行C#的分发
//                    // for C#
//                    bool showMessage = false;
//#if UNITY_STANDALONE || UNITY_EDITOR
//                    showMessage = true;
//#endif

//                    if (0 == cmdConfigType)
//                    {
//                        processMsg = DispatchMessageForLua(queueItem);
//                    }
//                    else if (1 == cmdConfigType)
//                    {
//                        processMsg = DispatchMessageForCSharp(queueItem, showMessage);
//                    }
//                    else if (2 == cmdConfigType)
//                    {
//                        bool processMsgLua = DispatchMessageForLua(queueItem);
//                        bool processMsgCSharp = DispatchMessageForCSharp(queueItem, showMessage);

//                        processMsg = (processMsgCSharp || processMsgLua);
//                    }
//                    else if (3 == cmdConfigType)
//                    {
//                        bool processMsgCSharp = DispatchMessageForCSharp(queueItem, showMessage);
//                        bool processMsgLua = DispatchMessageForLua(queueItem);
//                        processMsg = (processMsgCSharp || processMsgLua);
//                    }

                    TSArrayPool<byte>.Release(queueItem.MsgData.Data);
				}
			}
			
			/*lock(_ErrorQueue) 
			{
				for(int i = 0; i < _ErrorQueue.Count; ++i)
				{
					QueueItem errorItem = _ErrorQueue.Dequeue();
					SocketCloseItem socketCloseItem = errorItem.Model as SocketCloseItem;
					socketCloseItem.SocketListener.OnSocketClosed(socketCloseItem.Socket, socketCloseItem.FromRemote);
				}
			}*/
			
			//在这里发送下缓存消息
			NetworkKit.DefaultSocketChannel?.Socket?.Update();
		}

		#region ###Private###
		
		private bool DispatchMessageForLua(QueueItem queueItem)
		{
			if (false == _Inited)
			{
				return false;
			}
			
			_MsgContentLength = queueItem.MsgData.Length;
			ResizeMsgContent(_MsgContentLength);
			Array.Copy(queueItem.MsgData.Data, queueItem.MsgData.StartIndex, _MsgContect, 0, _MsgContentLength);

			var controlFlag = queueItem.MsgData.ControlFlag;
			var compressFlag = (controlFlag & 1) > 0;
			var iosMj = (controlFlag & 2) > 0;

			if (true == iosMj)
			{
				_MsgMJTempLength = _MsgContentLength - 256;
				ResizeMsgMJ(_MsgMJTempLength);
				Array.Copy(_MsgContect, 256, _MsgMJTemp, 0, _MsgMJTempLength);
				_MsgContentLength = _MsgMJTempLength;
				Array.Copy(_MsgMJTemp, 0, _MsgContect, 0, _MsgMJTempLength);
			}
			
			if (true == compressFlag)
			{
				//cache buff;
				var decodeBuff = SocketProtocol.I.DecodeCompress((short) queueItem.CMD, _MsgContect, _MsgContentLength, out int decodeLen);

				if (null != decodeBuff)
				{
					_MsgContentLength = decodeLen;
					ResizeMsgContent(_MsgContentLength);
					Array.Copy(decodeBuff, 0, _MsgContect, 0, _MsgContentLength);
				}
			}

			DispatchNetMessage(queueItem.CMD, _MsgContect, _MsgContentLength);
			
			return true;
		}
		public void DispatchNetMessage(int cmd, byte[] cacheBuff, int len)
		{

            //luaOnRecvMessage?.Invoke(cmd, cacheBuff);
            XLuaManager.Instance.DispatchNetMessage(cmd, cacheBuff, len);
        }

        private bool DispatchMessageForCSharp(QueueItem queueItem, bool showMessage)
		{
			//Type type = ProtocolTypeMatch.GetType((ProtocolEnum)queueItem.CMD);
			//if (null == type)
			//{
			//	return false;
			//}
			
			//_MsgContentLength = queueItem.MsgData.Length;
			//ResizeMsgContent(_MsgContentLength);
			//Array.Copy(queueItem.MsgData.Data, queueItem.MsgData.StartIndex, _MsgContect, 0, _MsgContentLength);
	        
			//var controlFlag = queueItem.MsgData.ControlFlag;
			//var compressFlag = (controlFlag & 1) > 0;
			//var iosMj = (controlFlag & 2) > 0;
			
			//if (true == iosMj)
			//{
			//	_MsgMJTempLength = _MsgContentLength - 256;
			//	ResizeMsgMJ(_MsgMJTempLength);
			//	Array.Copy(_MsgContect, 256, _MsgMJTemp, 0, _MsgMJTempLength);
			//	_MsgContentLength = _MsgMJTempLength;
			//	Array.Copy(_MsgMJTemp, 0, _MsgContect, 0, _MsgMJTempLength);
			//}
	        
			//if (true == compressFlag)
			//{
			//	//cache buff;
			//	var decodeBuff = SocketProtocol.I.DecodeCompress((short) queueItem.CMD, _MsgContect, _MsgContentLength, out int decodeLen);

			//	if (null != decodeBuff)
			//	{
			//		_MsgContentLength = decodeLen;
			//		ResizeMsgContent(_MsgContentLength);
			//		Array.Copy(decodeBuff, 0, _MsgContect, 0, _MsgContentLength);
			//	}
			//}
	        
			//var msgObj = SocketProtocol.I.DecodeMessage(type, _MsgContect, _MsgContentLength, controlFlag) as IMessage;
			
			//NetworkKit.HandleCSharpMessage(queueItem.CMD, msgObj);

			return true;
		}

		private void ResizeMsgContent(int newSize)
		{
			if (null == _MsgContect)
			{
				return;
			}

			if (newSize <= _MsgContect.Length)
			{
				return;
			}

			while (_MsgContect.Length < newSize)
			{
				Array.Resize(ref _MsgContect, _MsgContect.Length + _MsgInitSize);
			}
		}

		private void ResizeMsgMJ(int newSize)
		{
			if (null == _MsgMJTemp)
			{
				return;
			}

			if (newSize <= _MsgMJTemp.Length)
			{
				return;
			}

			while (_MsgMJTemp.Length < newSize)
			{
				Array.Resize(ref _MsgMJTemp, _MsgMJTemp.Length + _MsgInitSize);
			}
		}

		#endregion
	}
}

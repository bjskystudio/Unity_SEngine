using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    /// <summary>
    /// 简单事件管理器
    /// </summary>
    public class EventManager : MonoSingleton<EventManager>
    {

        private int eventIdFirst = 1000000;
        private Dictionary<int, Action<object[]>> eventDict;
        protected override void Init()
        {
            eventDict = new Dictionary<int, Action<object[]>>();
        }

        public void DispatchEvent(int eventId, params object[] param)
        {
            if (eventDict.ContainsKey(eventId))
            {
                eventDict[eventId](param);
            }
        }
        public void AddListener(int eventId, Action<object[]> cb)
        {
            if (eventDict.ContainsKey(eventId))
            {
                eventDict[eventId] += cb;
            }
            else
            {
                eventDict[eventId] = cb;
            }

        }
        public void RemoveListener(int eventId, Action<object[]> cb)
        {
            if (eventDict.ContainsKey(eventId))
            {
                eventDict[eventId] -= cb;
            }

        }

        public static int NewEventId()
        {
            return EventManager.Instance.eventIdFirst++;
        }
        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
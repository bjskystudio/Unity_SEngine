// ========================================================
// Copyright: Tiny-joy Software Chengdu LLC 
// Author: arthuryi 
// CreateTime: 2022/05/17 18:05:15
// ========================================================

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Game.Network
{
    public class HTTPResponseMT
    {
        private int _StatusCode = -1;
        private string _ReceiveContent = string.Empty;
        private string _Error = string.Empty;

        public int StatusCode
        {
            get => _StatusCode;
            set => _StatusCode = value;
        }

        public string ReceiveContent
        {
            get => _ReceiveContent;
            set => _ReceiveContent = value;
        }
        
        public string Error
        {
            get => _Error;
            set => _Error = value;
        }
        
        
    }
}
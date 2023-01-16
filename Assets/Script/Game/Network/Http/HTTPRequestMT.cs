// ========================================================
// Copyright: Tiny-joy Software Chengdu LLC 
// Author: arthuryi 
// CreateTime: 2022/05/17 17:16:26
// ========================================================

using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

//######################回调在主线程跑的HTTP请求类###########################
//使用UnityWebRequest来进行http请求, 使用协程让回到在主线程进行方便UI操作;
//目前post只接受application/json类型的Content-Type 后面有新数据再改接口;

namespace Game.Network
{

    public class HTTPRequestMT
    {
        private string _Url = string.Empty;
        private Action<HTTPResponseMT> _CallBack;
        private StringBuilder _PostBuilder;
        private byte[] _PutData;
        private Method _Method = Method.Get;
        
        private enum Method
        {
            Get = 0,
            Post,
            Put,
            Head,
        }

        public HTTPRequestMT(string url, int method, Action<HTTPResponseMT> callBack)
        {
            _Url = url ?? string.Empty;
            _Method = (Method) method;
            _CallBack = callBack;
        }
        
        public void Send()
        {
            switch (_Method)
            {
                case Method.Get:
                    {
                        Launcher.Instance.StartCoroutine(GetC(_Url, _CallBack));

                        break;
                    }
                case Method.Post:
                    {
                        Launcher.Instance.StartCoroutine(PostC(_Url, _CallBack, _PostBuilder));

                        break;
                    }
                case Method.Put:
                    {
                        Launcher.Instance.StartCoroutine(PutC(_Url, _CallBack, _PutData));

                        break;
                    }
                case Method.Head:
                    {
                        Launcher.Instance.StartCoroutine(HeadC(_Url, _CallBack));

                        break;
                    }
            }
        }
        
        public void SetPostData(string data)
        {
            if (true == string.IsNullOrEmpty(data))
            {
                return;
            }
            
            if (null == _PostBuilder)
            {
                _PostBuilder = new StringBuilder(data.Length);
            }

            if (_PostBuilder.Length > 0)
            {
                _PostBuilder.Append("&");
            }

            _PostBuilder.Append(data);
        }

        public void SetPostData(string key, string value)
        {
            if (true == string.IsNullOrEmpty(key)
            || true == string.IsNullOrEmpty(value)
            )
            {
                return;
            }
            
            if (null == _PostBuilder)
            {
                _PostBuilder = new StringBuilder();
            }

            if (_PostBuilder.Length > 0)
            {
                _PostBuilder.Append("&");
            }

            _PostBuilder.Append(key).Append("=").Append(value);
        }

        public void SetPutData(string data)
        {
            if (true == string.IsNullOrEmpty(data))
            {
                return;
            }

            _PutData = Encoding.UTF8.GetBytes(data);
        }

        public void SetPutData(byte[] data)
        {
            _PutData = data;
        }

        private IEnumerator GetC(string url, Action<HTTPResponseMT> callBack)
        {
            if (true == string.IsNullOrEmpty(url)
                || null == callBack
            )
            {
                yield break;
            }

            using (var request = UnityWebRequest.Get(url))
            {
                if (null == request)
                {
                    callBack.Invoke(null);
                    
                    yield break;
                }

                yield return request.SendWebRequest();
                
                var response = new HTTPResponseMT();
                
                response.StatusCode = (int)request.result;

                if (UnityWebRequest.Result.ProtocolError == request.result
                    || UnityWebRequest.Result.ConnectionError == request.result
                    || UnityWebRequest.Result.DataProcessingError == request.result
                )
                {
                    response.Error = request.error;
                }
                else
                {
                    response.ReceiveContent = request.downloadHandler.text ?? string.Empty;
                }
                
                callBack.Invoke(response);
            }
        }

        private IEnumerator PostC(string url, Action<HTTPResponseMT> callBack, StringBuilder postData)
        {
            if (true == string.IsNullOrEmpty(url)
            || null == callBack
            || null == postData
            || postData.Length <= 0
            )
            {
                yield break;
            }

            using (var request = new UnityWebRequest(url, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(_PostBuilder.ToString()));
                request.downloadHandler = new DownloadHandlerBuffer();
                request.uploadHandler.contentType = "application/json";

                yield return request.SendWebRequest();
                
                var response = new HTTPResponseMT();
                
                response.StatusCode = (int)request.result;

                if (UnityWebRequest.Result.ProtocolError == request.result
                    || UnityWebRequest.Result.ConnectionError == request.result
                    || UnityWebRequest.Result.DataProcessingError == request.result
                )
                {
                    response.Error = request.error;
                }
                else
                {
                    response.ReceiveContent = request.downloadHandler.text ?? string.Empty;
                }
                
                callBack.Invoke(response);
            }
        }

        private IEnumerator PutC(string url, Action<HTTPResponseMT> callBack, byte[] data)
        {
            if (true == string.IsNullOrEmpty(url)
                || null == callBack
                || null == data
                || data.Length <= 0
            )
            {
                yield break;
            }

            using (var request = UnityWebRequest.Put(url, data))
            {
                if (null == request)
                {
                    callBack.Invoke(null);
                    
                    yield break;
                }

                yield return request.SendWebRequest();
                
                var response = new HTTPResponseMT();
                
                response.StatusCode = (int)request.result;

                if (UnityWebRequest.Result.ProtocolError == request.result
                    || UnityWebRequest.Result.ConnectionError == request.result
                    || UnityWebRequest.Result.DataProcessingError == request.result
                )
                {
                    response.Error = request.error;
                }
                else
                {
                    response.ReceiveContent = request.downloadHandler.text ?? string.Empty;
                }
                
                callBack.Invoke(response);
            }
        }
        
        private IEnumerator HeadC(string url, Action<HTTPResponseMT> callBack)
        {
            if (true == string.IsNullOrEmpty(url)
                || null == callBack
            )
            {
                yield break;
            }

            using (var request = UnityWebRequest.Head(url))
            {
                if (null == request)
                {
                    callBack.Invoke(null);
                    
                    yield break;
                }

                yield return request.SendWebRequest();
                
                var response = new HTTPResponseMT();
                
                response.StatusCode = (int)request.result;

                if (UnityWebRequest.Result.ProtocolError == request.result
                    || UnityWebRequest.Result.ConnectionError == request.result
                    || UnityWebRequest.Result.DataProcessingError == request.result
                )
                {
                    response.Error = request.error;
                }
                else
                {
                    var contentLen = long.Parse(request.GetResponseHeader("Content-Length"));
                    response.ReceiveContent = contentLen + string.Empty;
                }
                
                callBack.Invoke(response);
            }
        }
    }
}
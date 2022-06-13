using Code.RESTClient.Attributes;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.RESTClient
{
    public abstract class Request<TSendData, TRecvData>
    {
        private static readonly string[] UrlArgsMethods = new string[] { UnityWebRequest.kHttpVerbGET, UnityWebRequest.kHttpVerbHEAD, UnityWebRequest.kHttpVerbDELETE };

        protected virtual string ContentType => "application/json";
        protected virtual string Charset => "utf-8";
        public async Task<Response<TRecvData>> Send()
        {
            var action = GetURL();
            var url = RestClient.Instance.Host + action;
            var method = GetMethod();
            var req = new UnityWebRequest(url, method);

            if (UrlArgsMethods.Contains(method))
            {
                req.url += GetDataAsArgsString();
            }
            else
            {
                req.uploadHandler = new UploadHandlerRaw(GetDataAsBytes());
                req.SetRequestHeader("Content-Type", $"{ContentType}; charset={Charset}");
            }

            req.downloadHandler = new DownloadHandlerBuffer();

            RestClient.Instance.Log("[Request]", method + ":", action);
            
            DateTime timeStart = DateTime.Now;

            var reqAsyncOperation =  req.SendWebRequest();

            //------------ wait response ------------------
            while (!reqAsyncOperation.isDone)
            {
                await Task.Delay(1); // one ms delay
            }

            var totalMs = Mathf.RoundToInt((float)(DateTime.Now - timeStart).TotalMilliseconds);

            bool hasError = req.isNetworkError || req.isHttpError;

            if (hasError)
            {
                RestClient.Instance.Log("[Error]", $"total ms ({totalMs}):", req.error);
                return new Response<TRecvData>()
                {
                    Error = new HttpError()
                    {
                        Code = req.responseCode,
                        Message = req.error
                    },
                    
                    Data = default,
                    JSONContent = "",
                    UnityWebRequest = req
                };
            }
            else
            {
                string responseText = req.downloadHandler.text;

                RestClient.Instance.Log("[Response]", $"total ms ({totalMs}):\n", responseText);

                var recvData = JsonConvert.DeserializeObject<TRecvData>(responseText, RestClient.Instance.SerializerSettings);
                return new Response<TRecvData>()
                {
                    Error = null,

                    Data = recvData,
                    JSONContent = responseText,
                    UnityWebRequest = req
                };
            }
        }

        public string GetURL()
        {
            var attributes = GetType().GetCustomAttributes(typeof(UrlAttribute), false) as UrlAttribute[];
            if (attributes.Length > 0)
                return attributes[0].url;
            else
                throw new Exception($"{GetType()} has no Url attribute");
        }

        public string GetMethod()
        {
            var attributes = GetType().GetCustomAttributes(typeof(HttpMethodAttribute), false) as HttpMethodAttribute[];
            if (attributes.Length > 0)
                return attributes[0].method;
            else
                throw new Exception($"{GetType()} has no HttpMethod attribute");
        }

        public abstract TSendData GetData();

        private string GetDataAsArgsString()
        {
            var fields = GetData().GetType().GetFields();
            return "?" + string.Join("&", fields.Select(field => $"{field.Name}={field.GetValue(this)}&"));
        }

        private byte[] GetDataAsBytes()
        {
            var json = JsonConvert.SerializeObject(GetData(), RestClient.Instance.SerializerSettings);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
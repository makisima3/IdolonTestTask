using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Code.Util;

namespace Code.RESTClient
{
    public class RestClient : Singleton<RestClient>
    {
        [SerializeField]
        private bool useTestHost = false;
        [SerializeField]
        private bool logInfo = false;

        [SerializeField]
        private string hostname = "http://example.com";
        [SerializeField]
        private string testHostname = "http://127.0.0.1:5000";


        public string Host => useTestHost ? testHostname : hostname;

        public JsonSerializerSettings SerializerSettings => new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public void Log(string separator = " ", params object[] msg)
        {
            if (logInfo)
            {
                Debug.Log(string.Join(separator, msg));
            }
        }
    }
}
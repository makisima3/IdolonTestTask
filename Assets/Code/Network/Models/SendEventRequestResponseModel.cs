using Newtonsoft.Json;

namespace Code.Network.Models
{
    public class SendEventRequestResponseModel
    {
        [JsonProperty("error")]
        public bool Error { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
using Newtonsoft.Json;

namespace Code.Models
{
    public class EventModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
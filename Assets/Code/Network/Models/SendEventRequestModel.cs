using Code.Models;
using Newtonsoft.Json;

namespace Code.Network.Models
{
    public class SendEventRequestModel
    {
        [JsonProperty("events")]
        public EventModel[] EventModels { get; set; }
    }
}
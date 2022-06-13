using System.Collections.Generic;
using System.Linq;
using Code.Models;
using Code.Network.Models;
using Code.RESTClient;
using Code.RESTClient.Attributes;

namespace Code.Network
{
    [Url("/send-event"),HttpMethod(MethodType.POST)]
    public class SendEventRequest : Request<SendEventRequestModel, SendEventRequestResponseModel>
    {
        private EventModel[] _models;

        public SendEventRequest(IEnumerable<EventModel> models)
        {
            _models= models.ToArray();
        }

        public override SendEventRequestModel GetData() => new SendEventRequestModel()
        {
            EventModels = _models
        };
    }
}
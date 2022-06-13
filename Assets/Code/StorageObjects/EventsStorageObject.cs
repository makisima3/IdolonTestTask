using System.Collections.Generic;
using Code.Models;
using PersistentStorage;

namespace Code.StorageObjects
{
    public class EventsStorageObject : PlainStorageObject<List<EventModel>>
    {
        
        public EventsStorageObject() : base(new List<EventModel>())
        {
        }

        public override string PrefKey => nameof(EventsStorageObject);
    }
}
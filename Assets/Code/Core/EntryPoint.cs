using System.Collections.Generic;
using System.Linq;
using Code.Models;
using Code.StorageObjects;
using UnityEngine;

namespace Code.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private EventService eventService;
        [SerializeField] private EventServicePresenter eventServicePresenter;
        [SerializeField] private EventServiceController eventServiceController;

        private EventsStorageObject _eventsStorageObject;
        
        private void Awake()
        {
            _eventsStorageObject = PersistentStorage.PersistentStorage
                .Load<EventsStorageObject, List<EventModel>>(new EventsStorageObject());
            
            eventService.Initialize(_eventsStorageObject.Data);
            eventService.OnEventsUpdate.AddListener(OnEventsUpdate);
            
            eventServicePresenter.Initialize(_eventsStorageObject.Data);
            eventServiceController.Initialize();
            
            eventService.StartService();
        }

        private void OnEventsUpdate(EventModel[] eventModels)
        {
            _eventsStorageObject.Data = eventModels.ToList();

            PersistentStorage.PersistentStorage.Save<EventsStorageObject, List<EventModel>>(_eventsStorageObject);
        }
    }
}
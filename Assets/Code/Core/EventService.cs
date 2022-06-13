using System.Collections.Generic;
using System.Linq;
using Code.Models;
using Code.Network;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Core
{
    public class EventService : MonoBehaviour
    {
        [SerializeField] private float cooldownBeforeSend = 3f;

        private List<EventModel> _events;
        private float _timer;
        private bool _isWorking;
        private bool _isSending;

        public UnityEvent<EventModel[]> OnEventsUpdate { get; private set; }
        public UnityEvent<bool> OnWorkingStateChange { get; private set; }
        public UnityEvent<bool> OnSendingStateChange { get; private set; }
        public UnityEvent<float> OnTimerTick { get; private set; }


        public void Initialize(IEnumerable<EventModel> eventModels)
        {
            OnEventsUpdate = new UnityEvent<EventModel[]>();
            OnTimerTick = new UnityEvent<float>();
            OnWorkingStateChange = new UnityEvent<bool>();
            OnSendingStateChange = new UnityEvent<bool>();
            _isWorking = false;
            _isSending = false;

            _events = new List<EventModel>(eventModels);
        }

        public void TrackEvent(string type, string data) => TrackEvent(new EventModel() { Type = type, Data = data });

        public void TrackEvent(EventModel model)
        {
            _events.Add(model);
            
            OnEventsUpdate.Invoke(_events.ToArray());
        }

        public void StartService()
        {
            if(_isWorking)
                return;
            
            _timer = 0f; //for immediate sending 
            _isWorking = true;

            OnWorkingStateChange.Invoke(_isWorking);
        }

        public void StopService()
        {
            if(!_isWorking)
                return;
            
            _isWorking = false;

            OnWorkingStateChange.Invoke(_isWorking);
        }


        private void Update()
        {
            if (!_isWorking || _isSending) 
                return;
            
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                SendEvent();
                _timer = cooldownBeforeSend;
            }

            OnTimerTick.Invoke(_timer);
        }

        private async void SendEvent()
        {
            if(_isSending || _events.Count <= 0)
                return;
            
            _isSending = true;
            OnSendingStateChange.Invoke(_isSending);
            var eventsForSending = new List<EventModel>(_events);
            var response = await new SendEventRequest(eventsForSending).Send();
            if (!response.HasError)
            {
                _events = _events
                    .Where(e => !eventsForSending.Contains(e))
                    .ToList();

                OnEventsUpdate.Invoke(_events.ToArray());
            }

            _isSending = false;
            OnSendingStateChange.Invoke(_isSending);
        }
    }
}
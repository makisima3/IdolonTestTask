using System.Collections.Generic;
using Code.Models;
using TMPro;
using UnityEngine;

namespace Code.Core
{
    public class EventServicePresenter : MonoBehaviour
    {
        [Header("Service")]
        [SerializeField] private EventService eventService;

        [Header("Texts")]
        [SerializeField] private TMP_Text eventsQueue;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text workingStateText;
        [SerializeField] private TMP_Text sendingStateText;
        
        public void Initialize(IEnumerable<EventModel> eventModels)
        {
            eventService.OnTimerTick.AddListener(UpdateTimer);
            eventService.OnEventsUpdate.AddListener(UpdateEvents);
            eventService.OnWorkingStateChange.AddListener(UpdateWorkingState);
            eventService.OnSendingStateChange.AddListener(UpdateSendingState);
            
            UpdateEvents(eventModels);
        }

        private void UpdateTimer(float time)
        {
            timerText.text = $"Seconds to nextSend: {time:F1}";
        }

        private void UpdateWorkingState(bool value)
        {
            workingStateText.text = value ? "Working: On" : "Working: Off";
        }
        
        private void UpdateSendingState(bool value)
        {
            sendingStateText.text = value ? "Sending" : "Nothing to send";
        }

        private void UpdateEvents(IEnumerable<EventModel> eventModels)
        {
            eventsQueue.text = "Events\n";

            foreach (var eventModel in eventModels)
            {
                eventsQueue.text += $"Type: {eventModel.Type}||Data: {eventModel.Data}\n";
            }
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core
{
    public class EventServiceController : MonoBehaviour
    {
        [Header("Service")]
        [SerializeField] private EventService eventService;
        
        [Header("Buttons")]
        [SerializeField] private Button startServiceButton;
        [SerializeField] private Button stopServiceButton;
        [SerializeField] private Button sendLevelStartEventButton;
        [SerializeField] private Button sendRewardEventButton;
        [SerializeField] private Button sendLevelEndEventButton;
        [SerializeField] private Button sendCustomEventButton;

        [Header("Input")] 
        [SerializeField] private TMP_InputField typeInput;
        [SerializeField] private TMP_InputField dataInput;
        
        public void Initialize()
        {
            startServiceButton.onClick.AddListener(eventService.StartService);
            stopServiceButton.onClick.AddListener(eventService.StopService);
            sendLevelStartEventButton.onClick.AddListener(() => SendEvent("LevelStart","1"));
            sendRewardEventButton.onClick.AddListener(() => SendEvent("Reward","Coins:10"));
            sendLevelEndEventButton.onClick.AddListener(() => SendEvent("LevelEnd","1"));
            sendCustomEventButton.onClick.AddListener(() => SendEvent(typeInput.text, dataInput.text));
        }

        private void SendEvent(string type, string data)
        {
            if(string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(data))
                return;
            
            eventService.TrackEvent(type,data);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;

namespace Assets.UIElements.Scripts
{
    internal class CountDownTimer : MonoBehaviour
    {
        class TimerInfo
        {
            public string Time { get; set; }

            public override string ToString()
            {
                return Time;
            }
        
        }
        private TimerInfo TimerInfoInstance = new TimerInfo() { Time = "0" };
        private DateTime TimerExpires = DateTime.MinValue;
        public UnityEvent OnTimerExpired;

        [field: SerializeField] private LocalizeStringEvent LocalizeStringEvent {  get; set; }
        [field: SerializeField] private TextMeshProUGUI TimerDisplay {  get; set; }
        public TimeSpan Timer => TimerExpires - DateTime.Now;
        public bool TimerActive { get; set; } = false;

        private void Reset()
        {
            LocalizeStringEvent = GetComponent<LocalizeStringEvent>();
            TimerDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            LocalizeStringEvent.StringReference.Arguments = new[] { TimerInfoInstance };
        }
        public void StartTimer(float duration)
        {
            StartTimer(TimeSpan.FromSeconds(duration));
        }

        public void StartTimer(double duration)
        {
            StartTimer(TimeSpan.FromSeconds(duration));
        }

        public void StartTimer(TimeSpan duration)
        {
            TimerExpires = DateTime.Now + duration;
            TimerActive = true;
        }

        public void Update()
        {
            if (!TimerActive)
            {
                return;
            }
            var newString = Timer.ToString("%s");
            if (TimerInfoInstance.Time != newString)
            {
                TimerInfoInstance.Time = newString;
                LocalizeStringEvent.RefreshString();
            }
            if (Timer <= TimeSpan.Zero)
            {
                OnTimerExpired?.Invoke();
                TimerActive = false;
            }
        }
    }
}

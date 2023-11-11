using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Utility
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager Current;
        public readonly List<Timer> Timers = new();

        private void Awake()
        {
            Current = this;
        }

        private void Update()
        {
            for (var i = Timers.Count - 1; i >= 0; i--)
            {
                if (Timers[i].IsExpired)
                {
                    Timers[i].Expire();
                    Timers.RemoveAt(i);
                }
            }
        }
    }

    public class Timer
    {
        private float timeout;

        public void SetTimeout(float duration)
        {
            timeout = Time.time + duration;

            if (TimerManager.Current != null && !TimerManager.Current.Timers.Contains(this))
                TimerManager.Current.Timers.Add(this);
        }

        public event Action Expired;

        public void Expire()
        {
            Expired?.Invoke();
            Expired = null;
        }

        public bool IsExpired => Time.time > timeout;
    }
}
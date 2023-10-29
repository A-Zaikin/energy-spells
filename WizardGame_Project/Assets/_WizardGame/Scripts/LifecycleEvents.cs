using System;
using UnityEngine;

namespace WizardGame
{
    public class LifecycleEvents : MonoBehaviour
    {
        public event Action OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
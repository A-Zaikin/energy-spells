using System;
using UnityEngine;

namespace WizardGame
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float max;

        private float value;

        public event Action OnDeath;

        public float Value
        {
            get => value;
            set
            {
                var newValue = Mathf.Clamp(value, 0, max);
                if (this.value == newValue)
                    return;

                this.value = newValue;

                if (this.value == 0)
                    OnDeath?.Invoke();
            }
        }

        public float Max => max;

        private void Awake()
        {
            value = max;
        }
    }
}
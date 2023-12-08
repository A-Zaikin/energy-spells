using System;
using UnityEngine;

namespace WizardGame
{
    public class ManaContainer : MonoBehaviour
    {
        [SerializeField] private float max;
        [SerializeField] private float regenerationSpeed;

        private float value;

        public float Value
        {
            get => value;
            set
            {
                var newValue = Mathf.Clamp(value, 0, max);
                this.value = newValue;
            }
        }

        public float Max => max;

        private void Awake()
        {
            value = max;
        }

        private void Update()
        {
            Value += regenerationSpeed * Time.deltaTime;
        }
    }
}
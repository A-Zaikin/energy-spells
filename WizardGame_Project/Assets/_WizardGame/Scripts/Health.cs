using System;
using UnityEngine;

namespace WizardGame
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float max;

        private float value;

        public float Value
        {
            get => value;
            set
            {
                this.value = Mathf.Clamp(value, 0, max);

                if (this.value == 0)
                    Destroy(gameObject);
            }
        }

        private void Awake()
        {
            value = max;
        }
    }
}
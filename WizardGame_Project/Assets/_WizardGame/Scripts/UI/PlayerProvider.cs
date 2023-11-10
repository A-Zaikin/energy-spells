using System;
using UnityEngine;

namespace WizardGame
{
    public class PlayerProvider : MonoBehaviour
    {
        public static Health Health { get; private set; }

        [SerializeField] private Health health;

        private void Awake()
        {
            Health = health;
        }
    }
}

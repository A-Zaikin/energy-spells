using System;
using UnityEngine;

namespace WizardGame
{
    public class PlayerProvider : MonoBehaviour
    {
        public static Health Health { get; private set; }

        public static ManaContainer Mana { get; private set; }

        [SerializeField] private Health health;
        [SerializeField] private ManaContainer mana;

        private void Awake()
        {
            Health = health;
            Mana = mana;
        }
    }
}

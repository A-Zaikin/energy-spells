using System;
using UnityEngine;

namespace WizardGame
{
    public class Target : MonoBehaviour
    {
        public static Target Current;

        private void Awake()
        {
            Current = this;
        }
    }
}
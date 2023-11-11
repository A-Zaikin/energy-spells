using System;
using UnityEngine;

namespace WizardGame
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Health health;

        private void OnEnable()
        {
            if (health != null)
                health.OnDeath += Death;
        }

        private void OnDisable()
        {
            if (health != null)
                health.OnDeath -= Death;
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}

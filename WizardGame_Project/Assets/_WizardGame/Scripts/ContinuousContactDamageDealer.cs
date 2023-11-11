using System;
using UnityEngine;

namespace WizardGame
{
    public class ContinuousContactDamageDealer : MonoBehaviour
    {
        [SerializeField] private ParameterContainer parameters;
        [SerializeField] private TeamContainer team;

        private float timeSinceLastHit;

        private void OnCollisionEnter(Collision collision)
        {
            DealDamage(collision.gameObject);
        }

        private void OnCollisionStay(Collision collision)
        {
            DealDamage(collision.gameObject);
        }

        private void DealDamage(GameObject gameObject)
        {
            if (team != null &&
                parameters != null &&
                parameters.Get(ParameterType.Damage, out var damage) &&
                parameters.Get(ParameterType.FireRate, out var fireRate) &&
                timeSinceLastHit > 1 / fireRate &&
                gameObject.TryGetComponent<Damageable>(out var damageable))
            {
                if (damageable.ReceiveDamage(damage, team.Value))
                    timeSinceLastHit = 0;
            }
        }

        private void Update()
        {
            timeSinceLastHit += Time.deltaTime;
        }
    }
}
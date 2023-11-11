using System;
using UnityEngine;

namespace WizardGame
{
    public class ContinuousContactDamageDealer : MonoBehaviour
    {
        [SerializeField] private ParameterContainer parameters;
        [SerializeField] private TeamContainer team;

        private float timeSinceLastHit;

        private void OnCollisionStay(Collision collision)
        {
            if (team != null &&
                parameters != null &&
                parameters.Get(ParameterType.Damage, out var damage) &&
                parameters.Get(ParameterType.FireRate, out var fireRate) &&
                timeSinceLastHit > 1 / fireRate &&
                collision.gameObject.TryGetComponent<Damageable>(out var damageable))
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
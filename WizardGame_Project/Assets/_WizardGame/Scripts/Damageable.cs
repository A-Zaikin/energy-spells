using System;
using UnityEngine;
using WizardGame.Data;

namespace WizardGame
{
    public class Damageable : MonoBehaviour
    {
        public static event Action<DamageReceivedArgs> OnDamageReceived;
        
        [field: SerializeField] public Team Team { get; private set; }
        [SerializeField] private Health health;

        public bool ReceiveDamage(float damage, Team incomingTeam)
        {
            if (health == null || incomingTeam == Team)
                return false;

            health.Value -= damage;

            OnDamageReceived?.Invoke(new DamageReceivedArgs(damage, transform.position));

            return true;
        }
    }

    public struct DamageReceivedArgs
    {
        public float Damage;
        public Vector3 Position;

        public DamageReceivedArgs(float damage, Vector3 position)
        {
            Damage = damage;
            Position = position;
        }
    }
}
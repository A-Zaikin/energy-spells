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
            if (health == null || incomingTeam == null || incomingTeam == Team)
                return false;

            health.Value -= damage;

            OnDamageReceived?.Invoke(new DamageReceivedArgs(damage, transform.position, Team));

            return true;
        }
    }

    public struct DamageReceivedArgs
    {
        public float Damage;
        public Vector3 Position;
        public Team Team;

        public DamageReceivedArgs(float damage, Vector3 position, Team team)
        {
            Damage = damage;
            Position = position;
            Team = team;
        }
    }
}
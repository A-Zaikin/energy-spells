using UnityEngine;
using WizardGame.Data;

namespace WizardGame
{
    public class Damageable : MonoBehaviour
    {
        [field: SerializeField] public Team Team { get; private set; }
        [SerializeField] private Health health;

        public bool ReceiveDamage(float damage, Team incomingTeam)
        {
            if (health == null || incomingTeam == Team)
                return false;

            health.Value -= damage;
            return true;
        }
    }
}
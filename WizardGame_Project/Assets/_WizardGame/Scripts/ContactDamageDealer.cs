using UnityEngine;

namespace WizardGame
{
    public class ContactDamageDealer : MonoBehaviour
    {
        [SerializeField] private ParameterContainer parameters;
        [SerializeField] private TeamContainer team;

        private void OnCollisionEnter(Collision collision)
        {
            if (team != null &&
                parameters != null &&
                parameters.Get(ParameterType.Damage, out var damage) &&
                collision.gameObject.TryGetComponent<Damageable>(out var damageable))
            {
                if (parameters.Get(ParameterType.Intensity, out var intensity))
                    damage *= intensity;

                damageable.ReceiveDamage(damage, team.Value);
            }
        }
    }
}
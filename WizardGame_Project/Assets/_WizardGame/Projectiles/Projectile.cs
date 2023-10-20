using UnityEngine;
using UnityEngine.Animations;
using WizardGame.Data;

namespace WizardGame
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject trailPrefab;
        [SerializeField] private Team team;
        [SerializeField] private float damage;

        private PositionConstraint trailConstraint;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Damageable>(out var damageable))
                damageable.ReceiveDamage(damage, team);

            Destroy(gameObject);
        }

        private void Awake()
        {
            if (trailPrefab == null)
                return;

            var trail = Instantiate(trailPrefab, transform.position, transform.rotation);

            if (trail.TryGetComponent(out trailConstraint))
                trailConstraint.AddSource(new ConstraintSource { sourceTransform = transform, weight = 1 });
        }

        private void OnDestroy()
        {
            if (trailConstraint != null)
                trailConstraint.enabled = false;
        }
    }
}
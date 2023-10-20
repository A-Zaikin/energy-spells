using System;
using UnityEngine;
using UnityEngine.Animations;

namespace WizardGame
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject trailPrefab;

        private PositionConstraint trailConstraint;

        private void OnCollisionEnter(Collision other)
        {
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace WizardGame
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParameterContainer parameters;
        [SerializeField] private GameObject trailPrefab;
        [SerializeField] private Rigidbody body;

        private PositionConstraint trailConstraint;

        private int bounceCount;
        private float life;

        private void OnCollisionEnter(Collision collision)
        {
            if (parameters != null && parameters.Get(ParameterType.Bounces, out var bounces) && bounceCount < bounces)
            {
                bounceCount++;

                if (body != null && collision.contactCount > 0)
                {
                    var contact = collision.GetContact(0);
                    body.velocity = Vector3.Reflect(-collision.relativeVelocity, contact.normal);
                }

                return;
            }

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

        private void FixedUpdate()
        {
            if (body != null)
            {
                if (parameters != null && parameters.Get(ParameterType.Gravity, out var gravity))
                    body.velocity += Vector3.down * gravity;

                if (parameters != null && parameters.Get(ParameterType.AccelerationMultiplier, out var accelerationMultiplier))
                    body.velocity *= accelerationMultiplier;
            }
        }

        private void Update()
        {
            if (parameters != null && parameters.Get(ParameterType.Lifetime, out var lifetime) &&
                life > lifetime)
            {
                Destroy(gameObject);
                return;
            }

            life += Time.deltaTime;
        }
    }
}
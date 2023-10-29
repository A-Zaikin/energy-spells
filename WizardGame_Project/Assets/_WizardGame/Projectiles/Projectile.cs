using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using WizardGame.Data;

namespace WizardGame
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject trailPrefab;
        [SerializeField] private Team team;
        [SerializeField] private Rigidbody body;

        private readonly Dictionary<WeaponParameterType, float> parameters = new();
        private PositionConstraint trailConstraint;

        private int bounceCount;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Damageable>(out var damageable) &&
                parameters.TryGetValue(WeaponParameterType.Damage, out var damage))
            {
                damageable.ReceiveDamage(damage, team);
            }

            if (parameters.TryGetValue(WeaponParameterType.Bounces, out var bounces) && bounceCount < bounces)
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
                if (parameters.TryGetValue(WeaponParameterType.Gravity, out var gravity))
                    body.velocity += Vector3.down * gravity;

                if (parameters.TryGetValue(WeaponParameterType.AccelerationMultiplier, out var accelerationMultiplier))
                    body.velocity *= accelerationMultiplier;
            }
        }

        public void ApplyParameter(WeaponParameterType type, float value)
        {
            parameters[type] = value;
        }
    }
}
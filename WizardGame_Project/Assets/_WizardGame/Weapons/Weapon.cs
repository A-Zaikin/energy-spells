using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame
{
    public class Weapon : MonoBehaviour
    {
        public static Weapon Current { get; private set; }

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private float startingImpulse;

        private float timeSinceLastShot;

        public Dictionary<WeaponParameterType, float> Parameters { get; } = new();

        private void Awake()
        {
            Current = this;
        }

        private void Update()
        {
            if (projectilePrefab == null || firePoint == null)
                return;

            if (InputManager.GetMouseButton(0) &&
                Parameters.TryGetValue(WeaponParameterType.FireRate, out var fireRate) &&
                timeSinceLastShot > 1 / fireRate)
            {
                var projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);

                if (projectileObject.TryGetComponent<Rigidbody>(out var body))
                    body.AddForce(startingImpulse * (transform.rotation * Vector3.forward), ForceMode.Impulse);

                if (projectileObject.TryGetComponent<Projectile>(out var projectile))
                {
                    foreach (var (type, value) in Parameters)
                        projectile.ApplyParameter(type, value);
                }

                timeSinceLastShot = 0;
            }

            timeSinceLastShot += Time.deltaTime;
        }
    }
}
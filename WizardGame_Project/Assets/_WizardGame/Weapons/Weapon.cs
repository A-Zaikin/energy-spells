using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Extensions;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class Weapon : MonoBehaviour
    {
        public static Weapon Current { get; private set; }

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject firePoint;

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
                var angle = Vector2.SignedAngle(Vector2.right, transform.forward.Xz());

                if (Parameters.TryGetValue(WeaponParameterType.Spread, out var spread))
                    angle += Random.Range(-spread, spread);

                var angleInRadians = angle * Mathf.Deg2Rad;

                var direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
                var rotation = Quaternion.FromToRotation(Vector3.forward, direction.AsXz());

                var projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, rotation);

                if (projectileObject.TryGetComponent<Rigidbody>(out var body) &&
                    Parameters.TryGetValue(WeaponParameterType.Speed, out var speed))
                {
                    body.AddForce(speed * (rotation * Vector3.forward), ForceMode.Impulse);
                }

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
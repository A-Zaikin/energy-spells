using UnityEngine;

namespace WizardGame
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private float startingImpulse;

        private void Update()
        {
            if (projectilePrefab == null || firePoint == null)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                var projectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
                if (projectile.TryGetComponent<Rigidbody>(out var body))
                {
                    body.AddForce(startingImpulse * (transform.rotation * Vector3.forward), ForceMode.Impulse);
                }
            }
        }
    }
}
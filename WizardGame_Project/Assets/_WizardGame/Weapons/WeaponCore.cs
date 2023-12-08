using UnityEngine;
using WizardGame.Data;
using WizardGame.Utility;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class WeaponCore : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private ParameterContainer parameters;
        [SerializeField] private Team team;

        private float timeSinceLastShot;
        private float pelletCountAdditive;
        private ManaContainer manaContainer;

        public bool CanShoot { get; set; }

        public void Setup(ManaContainer manaContainer)
        {
            this.manaContainer = manaContainer;
        }

        private void Update()
        {
            if (firePoint == null)
                return;

            var hasManaCost = parameters.Get(ParameterType.ManaCost, out var manaCost);

            var hasEnoughMana = manaContainer == null ||
                !hasManaCost ||
                manaContainer.Value > manaCost;

            if (CanShoot &&
                parameters.Get(ParameterType.FireRate, out var fireRate) &&
                timeSinceLastShot > 1 / fireRate &&
                hasEnoughMana)
            {
                var weaponAngle = Vector2.SignedAngle(Vector2.right, transform.forward.Xz());

                if (parameters.Get(ParameterType.PelletCount, out var pellets) &&
                    parameters.Get(ParameterType.PelletSpread, out var pelletSpread))
                {
                    pelletCountAdditive += pellets;
                    var pelletCount = Mathf.FloorToInt(pelletCountAdditive);
                    pelletCountAdditive -= pelletCount;

                    if (pelletCount % 2 == 0)
                    {
                        for (var i = 0; i < pelletCount; i++)
                        {
                            // ReSharper disable once PossibleLossOfFraction
                            var offset = pelletSpread / 2 + (pelletCount / 2 - 1) * pelletSpread;
                            var angle = weaponAngle - offset + pelletSpread * i;

                            ShootPellet(angle);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < pelletCount; i++)
                        {
                            // ReSharper disable once PossibleLossOfFraction
                            var offset = pelletCount / 2 * pelletSpread;
                            var angle = weaponAngle - offset + pelletSpread * i;

                            ShootPellet(angle);
                        }
                    }
                }

                timeSinceLastShot = 0;

                if (manaContainer != null && hasManaCost)
                {
                    if (parameters.Get(ParameterType.Intensity, out var intensity))
                        manaCost *= intensity;

                    manaContainer.Value -= manaCost;
                }
            }

            timeSinceLastShot += Time.deltaTime;
        }

        private void ShootPellet(float angle)
        {
            if (parameters.Get(ParameterType.RandomSpread, out var spread))
                angle += Random.Range(-spread, spread);

            var direction = VectorHelper.CreateFromAngle(angle);
            var rotation = Quaternion.FromToRotation(Vector3.forward, direction.AsXz());

            var projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, rotation);

            if (projectileObject.TryGetComponent<Rigidbody>(out var body) &&
                parameters.Get(ParameterType.Speed, out var speed))
            {
                body.AddForce(speed * (rotation * Vector3.forward), ForceMode.Impulse);
            }

            if (projectileObject.TryGetComponent<ParameterContainer>(out var container))
                container.Setup(parameters);

            if (projectileObject.TryGetComponent<TeamContainer>(out var teamContainer))
                teamContainer.Setup(team);
        }
    }
}
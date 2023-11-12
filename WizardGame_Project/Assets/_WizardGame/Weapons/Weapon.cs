using System.Collections.Generic;
using UnityEngine;
using WizardGame.Data;
using WizardGame.Utility;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class Weapon : MonoBehaviour
    {
        public static Weapon Current { get; private set; }

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private Team team;

        private float timeSinceLastShot;
        private float pelletCountAdditive;

        public Dictionary<ParameterType, Modifiable> Parameters { get; } = new();

        public OrderedContainer<WeaponMod> Mods { get; } = new(5);

        private readonly List<Modification> modifications = new();

        public void Setup(WeaponData data)
        {
            foreach (var (type, value) in data.StartingParameters)
            {
                Parameters[type] = new Modifiable(value);
            }
        }

        private void Awake()
        {
            Current = this;
        }

        private void OnEnable()
        {
            Mods.Observe += ObserveMods;
        }

        private void OnDisable()
        {
            Mods.Observe -= ObserveMods;
        }

        private void ObserveMods(OrderedContainer<WeaponMod> mods)
        {
            foreach (var modification in modifications)
                modification.Dispose();

            foreach (var mod in mods)
            {
                if (mod == null)
                    continue;

                foreach (var modifier in mod.Modifiers)
                {
                    if (Parameters.TryGetValue(modifier.Parameter, out var parameter))
                        modifications.Add(parameter.AddModifier(modifier.Value));
                }
            }
        }

        private void Update()
        {
            if (projectilePrefab == null || firePoint == null)
                return;

            if (InputManager.GetMouseButton(0) &&
                Parameters.TryGetValue(ParameterType.FireRate, out var fireRate) &&
                timeSinceLastShot > 1 / fireRate)
            {
                var weaponAngle = Vector2.SignedAngle(Vector2.right, transform.forward.Xz());

                if (Parameters.TryGetValue(ParameterType.PelletCount, out var pellets) &&
                    Parameters.TryGetValue(ParameterType.PelletSpread, out var pelletSpread))
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
            }

            timeSinceLastShot += Time.deltaTime;
        }

        private void ShootPellet(float angle)
        {
            if (Parameters.TryGetValue(ParameterType.RandomSpread, out var spread))
                angle += Random.Range(-spread, spread);

            var direction = VectorHelper.CreateFromAngle(angle);
            var rotation = Quaternion.FromToRotation(Vector3.forward, direction.AsXz());

            var projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, rotation);

            if (projectileObject.TryGetComponent<Rigidbody>(out var body) &&
                Parameters.TryGetValue(ParameterType.Speed, out var speed))
            {
                body.AddForce(speed * (rotation * Vector3.forward), ForceMode.Impulse);
            }

            if (projectileObject.TryGetComponent<ParameterContainer>(out var parameters))
                parameters.Setup(Parameters);

            if (projectileObject.TryGetComponent<TeamContainer>(out var teamContainer))
                teamContainer.Setup(team);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Data;
using WizardGame.Utility;

namespace WizardGame
{
    public class WeaponMovement : MonoBehaviour
    {
        [SerializeField] private Transform character;
        [SerializeField] private Transform aim;
        [SerializeField] private float offset;
        [SerializeField] private float height;
        [SerializeField, Range(0, 1)] private float interpolation;

        private IReadOnlyList<WeaponData> weaponsData;
        private readonly Dictionary<WeaponData, GameObject> weaponModels = new();

        public void Setup(IReadOnlyList<WeaponData> weaponsData)
        {
            this.weaponsData = weaponsData;

            foreach (var data in weaponsData)
                weaponModels[data] = Instantiate(data.Model, transform);
        }

        private void Update()
        {
            if (weaponModels == null)
                return;

            foreach (var model in weaponModels.Values)
            {
                var characterPosition = character.position.Xz();
                var aimPosition = aim.position;
                var aimDirection = (aimPosition.Xz() - characterPosition).normalized;
                var right = Vector2.Perpendicular(aimDirection);
                var desiredPosition = (characterPosition + right * offset).AsXz().WithY(height);
                var modelPosition = model.transform.position;
                var desiredRotation = Quaternion.LookRotation(aimPosition - modelPosition);

                modelPosition = Vector3.Lerp(modelPosition, desiredPosition, interpolation);
                model.transform.position = modelPosition;
                model.transform.rotation = Quaternion.Lerp(model.transform.rotation, desiredRotation, interpolation);
            }
        }
    }
}
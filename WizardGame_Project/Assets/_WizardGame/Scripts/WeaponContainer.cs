using System.Collections.Generic;
using UnityEngine;
using WizardGame.Data;

namespace WizardGame
{
    public class WeaponContainer : MonoBehaviour
    {
        [SerializeField] private WeaponMovement weaponMovement;
        [SerializeField] private List<WeaponData> weapons;

        private void Awake()
        {
            if (weaponMovement != null)
                weaponMovement.Setup(weapons);
        }
    }
}

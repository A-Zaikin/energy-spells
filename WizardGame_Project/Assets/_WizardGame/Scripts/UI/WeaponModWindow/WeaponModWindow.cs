using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame.UI
{
    public class WeaponModWindow : MonoBehaviour
    {
        [SerializeField] private Transform inventoryModContainer;
        [SerializeField] private Transform weaponModContainer;

        private void OnEnable()
        {
            if (Inventory.Current != null)
                Inventory.Current.WeaponMods.Observe += ObserveInventory;

            if (Weapon.Current != null)
                Weapon.Current.Mods.Observe += ObserveWeaponMods;
        }

        private void OnDisable()
        {
            if (Inventory.Current != null)
                Inventory.Current.WeaponMods.Observe -= ObserveInventory;
        }

        private void ObserveInventory(ObservableList<WeaponMod> mods)
        {
            UIHelper.SetupWidgets(inventoryModContainer, mods);
        }

        private void ObserveWeaponMods(OrderedContainer<WeaponMod> mods)
        {
            UIHelper.SetupWidgets(weaponModContainer, Weapon.Current.Mods);
        }
    }
}
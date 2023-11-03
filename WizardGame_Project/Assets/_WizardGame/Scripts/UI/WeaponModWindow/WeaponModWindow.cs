using System;
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame.UI
{
    public class WeaponModWindow : MonoBehaviour
    {
        [SerializeField] private Transform inventoryModContainer;

        private void OnEnable()
        {
            if (Inventory.Current != null)
                Inventory.Current.WeaponMods.Observe += Observe;
        }

        private void OnDisable()
        {
            if (Inventory.Current != null)
                Inventory.Current.WeaponMods.Observe -= Observe;
        }

        private void Observe(ObservableList<WeaponMod> mods)
        {
            UIHelper.SetupWidgets(inventoryModContainer, mods);
        }
    }
}
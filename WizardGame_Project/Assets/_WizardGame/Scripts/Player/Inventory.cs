using System.Collections.Generic;
using UnityEngine;

namespace WizardGame
{
    public class Inventory : MonoBehaviour
    {
        public List<WeaponMod> WeaponMods { get; } = new();

        public void AddMod(WeaponMod mod)
        {
            WeaponMods.Add(mod);
            foreach (var modifier in mod.Modifiers)
            {
                if (Weapon.Current.Parameters.TryGetValue(modifier.Parameter, out var parameter))
                    parameter.AddModifier(modifier.Value);
            }
        }
    }
}
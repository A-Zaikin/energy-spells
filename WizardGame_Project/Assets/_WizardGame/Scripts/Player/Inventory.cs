using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Current;
        public ObservableList<WeaponMod> WeaponMods { get; } = new();

        public void AddMod(WeaponMod mod)
        {
            WeaponMods.Add(mod);
            foreach (var modifier in mod.Modifiers)
            {
                if (Weapon.Current.Parameters.TryGetValue(modifier.Parameter, out var parameter))
                    parameter.AddModifier(modifier.Value);
            }
        }

        private void Awake()
        {
            Current = this;
        }
    }
}
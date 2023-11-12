using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Current;

        public ObservableList<WeaponMod> WeaponMods { get; } = new();

        private void Awake()
        {
            Current = this;

            for (int i = 0, count = 30; i < count; i++)
            {
                var mod = new WeaponMod(Quality.Rare);
                WeaponMods.Add(mod);
            }
        }
    }
}
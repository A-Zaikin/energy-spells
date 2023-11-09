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
        }
    }
}
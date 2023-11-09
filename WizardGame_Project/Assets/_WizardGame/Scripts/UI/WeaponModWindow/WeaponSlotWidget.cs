using UnityEngine;
using UnityEngine.EventSystems;

namespace WizardGame.UI
{
    public class WeaponSlotWidget : OrderedWidget<WeaponMod>, IDropHandler
    {
        [SerializeField] private InventoryModWidget modWidget;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryModWidget>(out var widget))
            {
                var mod = widget.Mod;

                if (Inventory.Current.WeaponMods.Contains(mod) &&
                    Weapon.Current.Mods.EmptyAt(Index))
                {
                    Inventory.Current.WeaponMods.Remove(mod);
                    Weapon.Current.Mods[Index] = mod;
                }
                else if (Weapon.Current.Mods.Contains(mod) &&
                         Weapon.Current.Mods.EmptyAt(Index))
                {
                    Weapon.Current.Mods.Remove(mod);
                    Weapon.Current.Mods[Index] = mod;
                }
            }
        }

        public override void Setup(WeaponMod mod)
        {
            if (modWidget != null)
            {
                modWidget.gameObject.SetActive(mod != null);

                if (mod != null)
                    modWidget.Setup(mod);
            }
        }
    }
}

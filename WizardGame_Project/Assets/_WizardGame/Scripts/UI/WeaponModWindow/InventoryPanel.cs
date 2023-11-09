using UnityEngine;
using UnityEngine.EventSystems;

namespace WizardGame.UI
{
    public class InventoryPanel : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryModWidget>(out var widget))
            {
                var mod = widget.Mod;

                if (Weapon.Current.Mods.Contains(mod))
                {
                    Weapon.Current.Mods.Remove(mod);
                    Inventory.Current.WeaponMods.Add(mod);
                }
            }
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace WizardGame.UI
{
    public class WeaponSlotWidget : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryModWidget>(out var widget))
            {
                Inventory.Current.WeaponMods.Remove(widget.Mod);
            }
        }
    }
}

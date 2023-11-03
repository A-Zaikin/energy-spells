using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WizardGame.UI
{
    public class InventoryModWidget : Widget<WeaponMod>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image image;

        public WeaponMod Mod { get; private set; }

        private Vector3 startPosition;

        public override void Setup(WeaponMod mod)
        {
            Mod = mod;

            if (text != null)
            {
                text.text = "";

                foreach (var modifier in mod.Modifiers)
                    text.text += $"{modifier.Parameter}: {modifier.Value:0.##}\n";
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (image != null)
                image.raycastTarget = false;

            startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (image != null)
                image.raycastTarget = true;

            transform.position = startPosition;
        }
    }
}
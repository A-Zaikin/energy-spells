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
        [SerializeField] private Canvas canvas;

        public WeaponMod Mod { get; private set; }

        private Vector3 startPosition;

        public override void Setup(WeaponMod mod)
        {
            Mod = mod;

            if (image != null)
                image.raycastTarget = true;

            if (canvas != null)
                canvas.sortingOrder = 1;

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

            if (canvas != null)
                canvas.sortingOrder = 2;

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

            if (canvas != null)
                canvas.sortingOrder = 1;

            transform.position = startPosition;
        }
    }
}
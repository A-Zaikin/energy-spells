using System;
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
                canvas.overrideSorting = false;

            if (text != null)
            {
                text.text = "";

                foreach (var modifier in mod.Modifiers)
                    text.text += $"{ParamToString(modifier.Parameter)}: {modifier.Value:0.##}\n";
            }
        }

        private static string ParamToString(ParameterType type)
        {
            return type switch
            {
                ParameterType.Damage => "Damage",
                ParameterType.FireRate => "Fire rate",
                ParameterType.RandomSpread => "Spread",
                ParameterType.Speed => "Speed",
                ParameterType.AccelerationMultiplier => "Acceleration",
                ParameterType.Gravity => "Gravity",
                ParameterType.Bounces => "Bounces",
                ParameterType.PelletCount => "Pellets",
                ParameterType.PelletSpread => "Pellet spread",
                ParameterType.Lifetime => "Lifetime",
                ParameterType.ManaCost => "Mana cost",
                ParameterType.Intensity => "Intensity",
                _ => string.Empty,
            };
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (image != null)
                image.raycastTarget = false;

            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = 1;
            }

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
                canvas.overrideSorting = false;

            transform.position = startPosition;
        }
    }
}
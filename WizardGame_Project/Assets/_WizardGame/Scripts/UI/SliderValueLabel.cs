using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WizardGame.UI
{
    public class SliderValueLabel : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;

        private void OnEnable()
        {
            if (slider == null)
                return;

            OnValueChanged(slider.value);
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            if (slider != null)
                slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float newValue)
        {
            if (text != null)
                text.text = newValue.ToString("0.00", CultureInfo.InvariantCulture);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace WizardGame.UI
{
    public class SliderSetsWeaponParameter : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TestWindow window;
        [SerializeField] private ParameterType type;

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
            if (window != null)
                TestWindow.SetWeaponParameter(type, newValue);
        }
    }
}
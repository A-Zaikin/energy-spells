using TMPro;
using UnityEngine;

namespace WizardGame.UI
{
    public class InputFieldSetsWeaponParameter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TestWindow window;
        [SerializeField] private WeaponParameterType type;

        private void OnEnable()
        {
            if (inputField == null)
                return;

            OnValueChanged(inputField.text);
            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            if (inputField != null)
                inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string newValue)
        {
            if (window != null && float.TryParse(newValue, out var value))
                TestWindow.SetWeaponParameter(type, value);
        }
    }
}
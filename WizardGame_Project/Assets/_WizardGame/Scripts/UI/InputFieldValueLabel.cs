using System.Globalization;
using TMPro;
using UnityEngine;

namespace WizardGame.UI
{
    public class InputFieldValueLabel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TextMeshProUGUI text;

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
            if (text != null && float.TryParse(newValue, out var value))
                text.text = value.ToString("0.00", CultureInfo.InvariantCulture);
        }
    }
}
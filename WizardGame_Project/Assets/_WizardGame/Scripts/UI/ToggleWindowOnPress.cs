using UnityEngine;

namespace WizardGame.UI
{
    public class ToggleWindowOnPress : MonoBehaviour
    {
        [SerializeField] private KeyCode key;
        [SerializeField] private GameObject window;

        private void Update()
        {
            if (window != null && Input.GetKeyDown(key))
                window.SetActive(!window.activeSelf);
        }
    }
}


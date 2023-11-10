using UnityEngine;

namespace WizardGame.UI
{
    public class TestWindow : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
                Time.timeScale = canvas.gameObject.activeSelf ? 0 : 1;
                InputManager.Blocked = canvas.gameObject.activeSelf;
            }
        }

        public static void SetWeaponParameter(ParameterType type, float value)
        {
            Weapon.Current.Parameters[type] = new Modifiable(value);
        }
    }
}

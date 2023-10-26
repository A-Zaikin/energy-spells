using System;
using UnityEngine;

namespace WizardGame
{
    public class InputManager : MonoBehaviour
    {
        public static bool Blocked;

        public static bool GetMouseButtonDown(int button)
        {
            return !Blocked && Input.GetMouseButtonDown(button);
        }

        public static bool GetMouseButton(int button)
        {
            return !Blocked && Input.GetMouseButton(button);
        }

        public static bool GetKeyDown(KeyCode key)
        {
            return !Blocked && Input.GetKeyDown(key);
        }

        public static Vector3 MousePosition { get; private set; }

        private void Update()
        {
            if (!Blocked)
                MousePosition = Input.mousePosition;
        }
    }
}
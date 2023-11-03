using UnityEngine;
using WizardGame.Utility;

namespace WizardGame.UnityUtils
{
    public class MoveToMousePosition : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float height;

        private void Update()
        {
            if (camera == null)
                return;

            var mousePosition = camera.ScreenToWorldPoint(InputManager.MousePosition.WithZ(camera.nearClipPlane));
            var cameraPosition = camera.transform.position;

            var mouseDirection = mousePosition - cameraPosition;

            var lengthToHeight = mouseDirection.magnitude / -mouseDirection.y;
            var length = lengthToHeight * cameraPosition.y;

            transform.position = (cameraPosition + mouseDirection.WithLength(length)).WithY(height);
        }
    }
}

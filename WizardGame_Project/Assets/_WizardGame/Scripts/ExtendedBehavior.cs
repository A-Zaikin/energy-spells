using UnityEngine;

namespace WizardGame
{
    public class ExtendedBehaviour : MonoBehaviour
    {
        protected void Spawn<T>(T prefab, Vector3 position) where T : Object
        {
            if (prefab != null && this != null && gameObject.scene.isLoaded)
                Instantiate(prefab, position, Quaternion.identity);
        }

        protected bool Spawn<T>(T prefab, Vector3 position, out T result) where T : Object
        {
            if (prefab != null && this != null && gameObject.scene.isLoaded)
            {
                result = Instantiate(prefab, position, Quaternion.identity);
                return true;
            }

            result = null;
            return false;
        }

        protected bool Spawn<T>(T prefab, out T result) where T : Object
        {
            if (prefab != null && this != null && gameObject.scene.isLoaded)
            {
                result = Instantiate(prefab);
                return true;
            }

            result = null;
            return false;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardGame
{
    public class Spawner
    {
        public static bool Spawn<T>(T prefab, out T result) where T : Object
        {
            var scene = SceneManager.GetActiveScene();
            if (prefab != null && scene.isLoaded)
            {
                result = Object.Instantiate(prefab);
                return true;
            }

            result = null;
            return false;
        }
    }
}
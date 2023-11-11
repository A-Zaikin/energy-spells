using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardGame
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Health health;

        private void OnEnable()
        {
            if (health != null)
                health.OnDeath += Death;
        }

        private void OnDisable()
        {
            if (health != null)
                health.OnDeath -= Death;
        }

        private void Death()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
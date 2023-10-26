using UnityEngine;

namespace WizardGame.Effects
{
    public class PlayVfxOnDisable : MonoBehaviour
    {
        [SerializeField] private GameObject effect;

        private void OnDisable()
        {
            if (effect != null)
                Instantiate(effect, transform.position, Quaternion.identity);
        }
    }
}
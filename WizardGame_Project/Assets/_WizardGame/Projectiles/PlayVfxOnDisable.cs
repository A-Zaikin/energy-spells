using UnityEngine;

namespace WizardGame.Effects
{
    public class PlayVfxOnDisable : ExtendedBehaviour
    {
        [SerializeField] private GameObject effect;

        private void OnDisable()
        {
            Spawn(effect, transform.position);
        }
    }
}
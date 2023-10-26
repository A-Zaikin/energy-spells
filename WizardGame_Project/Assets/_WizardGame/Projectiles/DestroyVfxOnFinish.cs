using UnityEngine;

namespace WizardGame.Effects
{
    public class DestroyVfxOnFinish : MonoBehaviour
    {
        private ParticleSystem[] particleSystems;

        private void Awake()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>();
        }

        private void Update()
        {
            if (particleSystems == null)
                return;

            for (int i = 0, count = particleSystems.Length; i < count; i++)
            {
                if (particleSystems[i].IsAlive())
                    return;
            }

            Destroy(gameObject);
        }
    }
}
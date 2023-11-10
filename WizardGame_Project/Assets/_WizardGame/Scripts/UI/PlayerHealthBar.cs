using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform value;

        private void Update()
        {
            if (value != null &&
                PlayerProvider.Health != null)
            {
                var relativeHealth = PlayerProvider.Health.Value / PlayerProvider.Health.Max;
                value.localScale = value.localScale.WithX(relativeHealth);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
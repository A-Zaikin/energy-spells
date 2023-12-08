using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class PlayerManaBar : MonoBehaviour
    {
        [SerializeField] private RectTransform value;

        private void Update()
        {
            if (value != null &&
                PlayerProvider.Mana != null)
            {
                var relativeMana = PlayerProvider.Mana.Value / PlayerProvider.Mana.Max;
                value.localScale = value.localScale.WithX(relativeMana);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
using System.Globalization;
using TMPro;
using UnityEngine;

namespace WizardGame.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI damageTextPrefab;
        [SerializeField] private Camera currentCamera;

        private void OnEnable()
        {
            Damageable.OnDamageReceived += OnDamageReceived;
        }

        private void OnDisable()
        {
            Damageable.OnDamageReceived -= OnDamageReceived;
        }

        private void OnDamageReceived(DamageReceivedArgs args)
        {
            if (currentCamera == null || canvas == null || damageTextPrefab == null)
                return;

            // var screenPosition = currentCamera.WorldToScreenPoint(args.Position);
            // var damageText = Instantiate(damageTextPrefab, screenPosition, Quaternion.identity);
            var damageText = Instantiate(damageTextPrefab, args.Position, currentCamera.transform.rotation);
            damageText.transform.parent = canvas.transform;
            damageText.text = args.Damage.ToString(CultureInfo.InvariantCulture);
        }
    }
}
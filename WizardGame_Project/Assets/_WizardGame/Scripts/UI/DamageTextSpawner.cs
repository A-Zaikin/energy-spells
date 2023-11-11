using System.Globalization;
using TMPro;
using UnityEngine;
using WizardGame.Data;

namespace WizardGame.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI damageTextPrefab;
        [SerializeField] private Camera currentCamera;
        [SerializeField] private Team playerTeam;

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
            damageText.text = Mathf.RoundToInt(args.Damage).ToString();

            if (playerTeam != null && args.Team == playerTeam)
                damageText.color = Color.red;
        }
    }
}
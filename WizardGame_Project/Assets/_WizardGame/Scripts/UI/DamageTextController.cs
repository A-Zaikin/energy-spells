using UnityEngine;
using UnityEngine.Serialization;
using WizardGame.Extensions;

namespace WizardGame.UI
{
    public class DamageTextController : MonoBehaviour
    {
        [SerializeField] private float lifetime;
        [SerializeField] private float initialSpeed;
        [SerializeField] private float maxAngle;
        
        [SerializeField] private AnimationCurve sizeOverLifetime;
        [SerializeField] private AnimationCurve alphaOverLifetime;
        [SerializeField] private AnimationCurve velocityOverLifetime;
        [SerializeField] private CanvasGroup canvasGroup;

        private Vector2 initialVelocity;
        private Vector2 currentVelocity;
        private float life;

        private void Awake()
        {
            var angle = (90 + Random.Range(-maxAngle, maxAngle)) * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            initialVelocity = initialSpeed * direction;
            currentVelocity = initialVelocity;
        }

        private void Update()
        {
            transform.position += transform.rotation * currentVelocity.AsXy() * Time.deltaTime;

            if (velocityOverLifetime != null)
                currentVelocity = velocityOverLifetime.Evaluate(life / lifetime) * initialVelocity;

            if (sizeOverLifetime != null)
                transform.localScale = VectorHelper.Create(sizeOverLifetime.Evaluate(life / lifetime));

            if (canvasGroup != null)
                canvasGroup.alpha = alphaOverLifetime.Evaluate(life / lifetime);

            life += Time.deltaTime;
            if (life > lifetime)
                Destroy(gameObject);
        }
    }
}
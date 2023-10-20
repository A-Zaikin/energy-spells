using System;
using UnityEngine;
using WizardGame.Extensions;

namespace WizardGame.UI
{
    public class DamageTextController : MonoBehaviour
    {
        [SerializeField] private float lifetime;
        [SerializeField] private Vector2 initialVelocity;
        [SerializeField] private float gravity;
        [SerializeField] private AnimationCurve sizeOverLifetime;

        private Vector2 currentVelocity;
        private float life;

        private void Awake()
        {
            currentVelocity = initialVelocity;
        }

        private void Update()
        {
            transform.position += currentVelocity.AsXy();
            currentVelocity += gravity * Time.deltaTime * Vector2.down;

            transform.localScale = VectorHelper.Create(sizeOverLifetime.Evaluate(life / lifetime));

            life += Time.deltaTime;
            if (life > lifetime)
                Destroy(gameObject);
        }
    }
}
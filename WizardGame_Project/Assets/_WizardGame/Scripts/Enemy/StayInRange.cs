using Unity.Mathematics;
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class StayInRange : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        [SerializeField] private ParameterContainer parameters;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;

        private void FixedUpdate()
        {
            body.velocity = Vector3.zero;

            if (body == null ||
                Target.Current == null ||
                parameters == null ||
                !parameters.Get(ParameterType.Speed, out var speed))
            {
                return;
            }

            var movement = (Target.Current.transform.position - body.position).OnlyXz();
            body.rotation = Quaternion.LookRotation(movement);

            if (movement.magnitude > minDistance && movement.magnitude < maxDistance)
                return;

            if (movement.magnitude < minDistance)
                speed = -speed;

            body.velocity = speed * movement.normalized;
        }
    }
}
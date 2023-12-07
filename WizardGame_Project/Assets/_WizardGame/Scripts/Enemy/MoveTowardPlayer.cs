using System;
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class MoveTowardPlayer : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        [SerializeField] private ParameterContainer parameters;

        private void FixedUpdate()
        {
            if (body != null &&
                Target.Current != null &&
                parameters != null &&
                parameters.Get(ParameterType.Speed, out var speed))
            {
                var direction = (Target.Current.transform.position - body.position).OnlyXz().normalized;
                body.rotation = Quaternion.LookRotation(direction);
                body.velocity = speed * direction;
            }
        }
    }
}
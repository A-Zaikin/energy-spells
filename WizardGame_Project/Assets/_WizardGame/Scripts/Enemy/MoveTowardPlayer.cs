using System;
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class MoveTowardPlayer : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        [SerializeField] private float speed;
        
        private void FixedUpdate()
        {
            if (body != null && Target.Current != null)
            {
                var direction = (Target.Current.transform.position - body.position).OnlyXz().normalized;
                body.velocity = speed * direction;
            }
        }
    }
}
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class PlayerCharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField, Range(0, 1)] private float directionInterpolation;
        [SerializeField] private float acceleration;

        private Vector2 input;
        private bool hasInput;

        private Vector2 movement;

        private void Update()
        {
            input = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
                input.y += 1;
            if (Input.GetKey(KeyCode.S))
                input.y -= 1;
            if (Input.GetKey(KeyCode.A))
                input.x -= 1;
            if (Input.GetKey(KeyCode.D))
                input.x += 1;

            hasInput = input != Vector2.zero;

            if (hasInput)
                input.Normalize();

            animator.SetBool("walking", hasInput);
        }

        private void FixedUpdate()
        {
            if (!hasInput || Vector2.Dot(movement, input) < 0)
                movement = Vector2.zero;

            movement = Vector2.Lerp(movement, input, acceleration * Time.fixedDeltaTime);
            if (movement.magnitude > 1)
                movement.Normalize();

            if (hasInput)
            {
                transform.position += speed * Time.fixedDeltaTime * movement.AsXz();
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.LookRotation(movement.AsXz()),
                    directionInterpolation);
            }
        }
    }
}

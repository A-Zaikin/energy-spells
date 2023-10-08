using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace WizardGame.UnityUtils
{
    public class ConstraintWeightController : MonoBehaviour
    {
        [SerializeField] private float minBackReductionAngle;
        [SerializeField] private float maxBackReductionAngle;

        [SerializeField] private Transform character;
        [SerializeField] private Transform aim;
        [SerializeField] private Rig rig;

        private float originalWeight;

        private void Start()
        {
            originalWeight = rig.weight;
        }

        private void Update()
        {
            if (character == null || aim == null || rig == null)
                return;

            var aimDirection = aim.position - character.position;
            var backAngle = Vector3.Angle(-character.forward, aimDirection);
            var weightScale = Mathf.Clamp01((backAngle - minBackReductionAngle) / maxBackReductionAngle);

            rig.weight = originalWeight * weightScale;
        }
    }
}
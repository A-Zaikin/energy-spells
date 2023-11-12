using UnityEngine;
using WizardGame.Utility;

namespace WizardGame
{
    public class ShootInRange : MonoBehaviour
    {
        [SerializeField] private WeaponCore weapon;
        [SerializeField] private GameObject eyeBone;
        [SerializeField] private float range;

        private void FixedUpdate()
        {
            if (Target.Current == null)
                return;

            var movement = Target.Current.transform.position - transform.position;

            if (eyeBone != null)
                eyeBone.transform.rotation = Quaternion.LookRotation(movement);

            if (weapon != null)
                weapon.CanShoot = movement.magnitude < range;
        }
    }
}
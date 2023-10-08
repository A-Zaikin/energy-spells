using UnityEngine;

namespace WizardGame.Data
{
    [CreateAssetMenu(menuName = "Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public GameObject Model { get; private set; }
    }
}

using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace WizardGame.Data
{
    [CreateAssetMenu(menuName = "Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public GameObject Model { get; private set; }

        [SerializedDictionary("Parameter Type", "Value")]
        public SerializedDictionary<ParameterType, float> StartingParameters;
    }
}

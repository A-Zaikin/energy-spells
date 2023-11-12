using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace WizardGame.Data
{
    [CreateAssetMenu(menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }

        [SerializedDictionary("Parameter Type", "Value")]
        public SerializedDictionary<ParameterType, float> StartingParameters;
    }
}
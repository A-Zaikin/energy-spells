using UnityEngine;

namespace WizardGame
{
    public class Loot : MonoBehaviour
    {
        [field: SerializeField] public Quality Quality { get; private set; }
    }

    public enum Quality
    {
        Common,
        Uncommon,
        Rare
    }
}
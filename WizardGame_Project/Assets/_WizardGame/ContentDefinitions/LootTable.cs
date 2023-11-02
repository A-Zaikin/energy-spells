using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WizardGame.Data
{
    [CreateAssetMenu(menuName = "Data/LootTable")]
    public class LootTable : ScriptableObject
    {
        [field: SerializeField] public List<Entry> Entries { get; private set; }

        public GameObject GetRandom()
        {
            var totalWeight = Entries.Sum(entry => entry.Weight);
            var randomWeight = Random.Range(0, totalWeight);
            var currentWeight = 0f;
            foreach (var entry in Entries)
            {
                currentWeight += entry.Weight;

                if (randomWeight < currentWeight)
                    return entry.Prefab;
            }

            return null;
        }

        [Serializable]
        public struct Entry
        {
            public float Weight;
            public GameObject Prefab;
        }
    }
}
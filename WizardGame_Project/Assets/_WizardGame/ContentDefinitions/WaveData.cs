using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Data
{
    [CreateAssetMenu(menuName = "Data/Wave")]
    public class WaveData : ScriptableObject
    {
        [field: SerializeField] public List<Enemy> Enemies { get; private set; }
    }

    [Serializable]
    public struct Enemy
    {
        public EnemyData EnemyData;
        public int Count;
    }
}
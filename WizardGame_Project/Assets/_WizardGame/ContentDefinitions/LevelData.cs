using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Data
{
    [CreateAssetMenu(menuName = "Data/Level")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public List<WaveData> Waves { get; private set; }
    }
}
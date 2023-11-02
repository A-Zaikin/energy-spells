using System;
using UnityEngine;
using WizardGame.Data;

namespace WizardGame
{
    public class LootDropper : ExtendedBehaviour
    {
        [SerializeField] private LootTable lootTable;

        private void OnDestroy()
        {
            Spawn(lootTable.GetRandom(), transform.position);
        }
    }
}
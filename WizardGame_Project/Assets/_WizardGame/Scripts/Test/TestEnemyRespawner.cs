using System.Collections.Generic;
using UnityEngine;
using WizardGame.Extensions;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class TestEnemyRespawner : MonoBehaviour
    {
        [SerializeField] private LifecycleEvents enemyPrefab;
        [SerializeField] private float radius;
        [SerializeField] private List<LifecycleEvents> startingEnemies;

        private void Start()
        {
            foreach (var enemy in startingEnemies)
                enemy.OnDestroyed += SpawnNewEnemy;
        }

        private void SpawnNewEnemy()
        {
            var position = Random.insideUnitCircle * radius;

            var enemy = Instantiate(enemyPrefab);
            var enemyTransform = enemy.transform;
            enemyTransform.position = enemyTransform.position.WithXz(position);
            enemy.OnDestroyed += SpawnNewEnemy;
        }
    }
}
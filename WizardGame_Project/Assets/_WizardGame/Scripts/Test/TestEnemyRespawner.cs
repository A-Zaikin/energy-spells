using System.Collections.Generic;
using UnityEngine;
using WizardGame.Data;
using WizardGame.Utility;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class TestEnemyRespawner : ExtendedBehaviour
    {
        [SerializeField] private LifecycleEvents enemyPrefab;
        [SerializeField] private float radius;
        [SerializeField] private List<LifecycleEvents> startingEnemies;
        [SerializeField] private float damage;
        [SerializeField] private Team team;
        [SerializeField] private float attackSpeed;

        private void Start()
        {
            foreach (var enemy in startingEnemies)
                SetupEnemy(enemy);
        }

        private void SpawnNewEnemy()
        {
            var position = Random.insideUnitCircle * radius;

            if (Spawn(enemyPrefab, out var enemy))
            {
                var enemyTransform = enemy.transform;
                enemyTransform.position = enemyTransform.position.WithXz(position);

                SetupEnemy(enemy);
            }
        }

        private void SetupEnemy(LifecycleEvents enemy)
        {
            if (enemy.TryGetComponent<ParameterContainer>(out var parameters))
            {
                parameters.SetupWithValues(new()
                {
                    [ParameterType.Damage] = damage,
                    [ParameterType.FireRate] = attackSpeed
                });
            }

            if (enemy.TryGetComponent<TeamContainer>(out var teamContainer))
                teamContainer.Setup(team);

            enemy.OnDestroyed += SpawnNewEnemy;
        }
    }
}
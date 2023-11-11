using UnityEngine;
using WizardGame.Data;
using WizardGame.Utility;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int enemyCountAdditive;
        [SerializeField] private int startingEnemyCount;

        [SerializeField] private float startingDamage;
        [SerializeField] private float startingMovementSpeed;

        [SerializeField] private LifecycleEvents enemyPrefab;
        [SerializeField] private float radius;

        [SerializeField] private Team team;
        [SerializeField] private float restTimeBetweenWaves;

        private EventCounter whenAllDestroyed;

        private int waveCount;
        private float damage;
        private float movementSpeed;

        private readonly Timer restTimer = new();

        private void Start()
        {
            damage = startingDamage;
            movementSpeed = startingMovementSpeed;

            SpawnNewWave();
        }

        private void SpawnNewWave()
        {
            whenAllDestroyed = new EventCounter();
            whenAllDestroyed.OnCompleted += Rest;

            var enemyCount = startingEnemyCount + waveCount * enemyCountAdditive;

            for (var i = 0; i < enemyCount; i++)
            {
                SpawnEnemy();
            }

            damage *= 1.08f;
            movementSpeed *= 1.08f;

            waveCount++;
        }

        private void SpawnEnemy()
        {
            var position = Random.insideUnitCircle * radius;
            
            if (Spawner.Spawn(enemyPrefab, out var enemy))
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
                    [ParameterType.FireRate] = 3,
                    [ParameterType.Speed] = movementSpeed
                });
            }

            if (enemy.TryGetComponent<TeamContainer>(out var teamContainer))
                teamContainer.Setup(team);

            if (whenAllDestroyed != null)
                enemy.OnDestroyed += whenAllDestroyed.Subscribe();
        }

        private void Rest()
        {
            restTimer.SetTimeout(restTimeBetweenWaves);
            restTimer.Expired += SpawnNewWave;
        }
    }
}


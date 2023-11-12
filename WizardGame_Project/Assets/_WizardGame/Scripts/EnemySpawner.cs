using UnityEngine;
using UnityEngine.Serialization;
using WizardGame.Data;
using WizardGame.Utility;
using Random = UnityEngine.Random;

namespace WizardGame
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        
        [SerializeField] private float radius;

        [SerializeField] private Team team;
        [SerializeField] private float restTimeBetweenWaves;

        private EventCounter whenAllDestroyed;

        private int waveCount;

        private readonly Timer restTimer = new();

        private void Start()
        {
            SpawnNewWave();
        }

        private void SpawnNewWave()
        {
            if (level == null || waveCount < 0)
                return;

            whenAllDestroyed = new EventCounter();
            whenAllDestroyed.OnCompleted += Rest;

            waveCount %= level.Waves.Count;

            var wave = level.Waves[waveCount];
            foreach (var enemyType in wave.Enemies)
            {
                for (int i = 0, count = enemyType.Count; i < count; i++)
                {
                    SpawnEnemy(enemyType.EnemyData);
                }
            }

            waveCount++;
        }

        private void SpawnEnemy(EnemyData data)
        {
            var position = Random.insideUnitCircle * radius;
            
            if (Spawner.Spawn(data.Prefab, out var enemy))
            {
                var enemyTransform = enemy.transform;
                enemyTransform.position = enemyTransform.position.WithXz(position);

                SetupEnemy(enemy, data);
            }
        }

        private void SetupEnemy(GameObject enemy, EnemyData data)
        {
            if (enemy.TryGetComponent<ParameterContainer>(out var parameters))
                parameters.SetupWithValues(data.StartingParameters);

            if (enemy.TryGetComponent<TeamContainer>(out var teamContainer))
                teamContainer.Setup(team);

            if (whenAllDestroyed != null && enemy.TryGetComponent<LifecycleEvents>(out var lifecycleEvents))
                lifecycleEvents.OnDestroyed += whenAllDestroyed.Subscribe();
        }

        private void Rest()
        {
            restTimer.SetTimeout(restTimeBetweenWaves);
            restTimer.Expired += SpawnNewWave;
        }
    }
}


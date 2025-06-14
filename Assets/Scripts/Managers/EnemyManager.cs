using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class EnemyManager
    {
        [Inject] private readonly LevelConfigSO levelConfig;
        [Inject] private readonly TankEnemy tankPrefab;
        [Inject] private readonly SpawnPoint[] spawnPoints;
        [Inject] private readonly TankObjectPooling tankObjectPooling;
        
        private int maxCount;
        private int activeEnemies;
        private Coroutine spawnCoroutine;

        public event Action OnAllEnemiesDefeated;

        public void Initialize()
        {
            maxCount = levelConfig.EnemyCount;
            activeEnemies = 0;
        
            var gameObject = new GameObject("EnemyManager");
            var monoBehaviour = gameObject.AddComponent<CoroutineRunner>();
        
            spawnCoroutine = monoBehaviour.StartCoroutine(SpawnEnemyWorker());
        }

        private IEnumerator SpawnEnemyWorker()
        {
            while (maxCount > 0)
            {
                if (spawnPoints.All(sp => sp.Busy == true))
                {
                    yield return new WaitForSeconds(1);
                    continue;
                }

                maxCount--;
                activeEnemies++;

                var freeSpawnPoint = spawnPoints.First(sp => sp.Busy == false);
                freeSpawnPoint.Busy = true;

                var tank = tankObjectPooling.Item;
                tank.transform.position = freeSpawnPoint.transform.position;
                tank.transform.rotation = freeSpawnPoint.transform.rotation;

                tank.OnDeathEvent += () => HandleEnemyDeath(freeSpawnPoint);

                Debug.Log("Tank spawned");
            }
        }

        private void HandleEnemyDeath(SpawnPoint spawnPoint)
        {
            activeEnemies--;
            spawnPoint.Busy = false;

            if (activeEnemies <= 0 && maxCount <= 0)
            {
                OnAllEnemiesDefeated?.Invoke();
            }
        }

        private class CoroutineRunner : MonoBehaviour { }
    }
}
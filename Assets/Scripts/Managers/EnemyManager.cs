using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class EnemyManager : IDisposable
    {
        [Inject] private readonly LevelConfigSO levelConfig;
        [Inject] private readonly TankEnemy tankPrefab;
        [Inject] private readonly SpawnPoint[] spawnPoints;
        [Inject] private readonly TankObjectPooling tankObjectPooling;
        
        private int maxCount;
        private int activeEnemies;
        private Coroutine spawnCoroutine;
        private CoroutineRunner coroutineRunner;
        private readonly Dictionary<Tank, SpawnPoint> tankSpawnMap = new();

        public event Action OnAllEnemiesDefeated;

        public void Initialize()
        {
            maxCount = levelConfig.EnemyCount;
            activeEnemies = 0;
        
            var gameObject = new GameObject("EnemyManager");
            coroutineRunner = gameObject.AddComponent<CoroutineRunner>();
        
            spawnCoroutine = coroutineRunner.StartCoroutine(SpawnEnemyWorker());
        }

        private IEnumerator SpawnEnemyWorker()
        {
            while (maxCount > 0)
            {
                if (spawnPoints.All(sp => sp.Busy))
                {
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                maxCount--;
                activeEnemies++;

                var freeSpawnPoint = spawnPoints.First(sp => !sp.Busy);
                freeSpawnPoint.Busy = true;

                var tank = tankObjectPooling.Item;
                tank.transform.position = freeSpawnPoint.transform.position;
                tank.transform.rotation = freeSpawnPoint.transform.rotation;

                // Store the mapping for cleanup
                tankSpawnMap[tank] = freeSpawnPoint;
                tank.OnDeathEvent += HandleEnemyDeath;

                //Debug.Log("Tank spawned");
            }
        }

        private void HandleEnemyDeath(Tank tank)
        {
            if (tankSpawnMap.TryGetValue(tank, out var spawnPoint))
            {
                activeEnemies--;
                spawnPoint.Busy = false;
                
                // Clean up the mapping and unsubscribe
                tankSpawnMap.Remove(tank);
                tank.OnDeathEvent -= HandleEnemyDeath;

                if (activeEnemies <= 0 && maxCount <= 0)
                {
                    OnAllEnemiesDefeated?.Invoke();
                }
            }
        }

        public void StopSpawning()
        {
            if (spawnCoroutine != null && coroutineRunner != null)
            {
                coroutineRunner.StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }

        public void Dispose()
        {
            StopSpawning();
            
            // Clean up all tank subscriptions
            foreach (var tank in tankSpawnMap.Keys.ToList())
            {
                tank.OnDeathEvent -= HandleEnemyDeath;
            }
            tankSpawnMap.Clear();
            
            // Destroy the coroutine runner GameObject
            if (coroutineRunner != null)
            {
                UnityEngine.Object.Destroy(coroutineRunner.gameObject);
                coroutineRunner = null;
            }
        }

        private class CoroutineRunner : MonoBehaviour 
        {
            private void OnDestroy()
            {
                // Additional safety cleanup if GameObject is destroyed externally
            }
        }
    }
}
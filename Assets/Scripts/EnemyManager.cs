using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private LevelConfigSO levelConfig;
    [SerializeField] private Tank tankPrefab;
    [SerializeField] private SpawnPoint[] spawnPoints;

    private int _maxCount;

    private void Start()
    {
        _maxCount = levelConfig.EnemyCount;
        
        StartCoroutine(SpawnEnemyWorker());
    }

    private IEnumerator SpawnEnemyWorker()
    {
        while (_maxCount >= 0)
        {
            if (spawnPoints.All(sp => sp.Busy == true))
            {
                //Debug.Log("Spawn points are busy");
                
                yield return new WaitForSeconds(1);
                
                continue;
            }
            
            --_maxCount;
            
            var freeSpawnPoint = spawnPoints.First(sp => sp.Busy == false);
            freeSpawnPoint.Busy = true;
            
            var tank = TankObjectPooling.Instance.Item;
            
            tank.transform.position = freeSpawnPoint.transform.position;

            Debug.Log("Tank spawned");
        }
    }
}
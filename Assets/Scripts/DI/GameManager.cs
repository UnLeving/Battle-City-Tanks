using System;
using Managers;
using UnityEngine;

namespace DI
{
    public class GameManager
    {
        private readonly LevelConfigSO levelConfig;
        private readonly PlayerManager playerManager;
        private readonly EnemyManager enemyManager;
        private readonly HQService hqService;
        
        public event Action OnGameStart;
        public event Action OnGameEnd;
        public event Action OnPlayerDeath;
        public event Action OnHQDestroyed;
        
        public GameManager(LevelConfigSO levelConfig, PlayerManager playerManager, EnemyManager enemyManager, HQService hqService)
        {
            this.levelConfig = levelConfig;
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.hqService = hqService;
        }
        
        public void Initialize()
        {
            Debug.Log($"Initializing game with level: {levelConfig.Name}");
        
            playerManager.OnPlayerDeath += HandlePlayerDeath;
            enemyManager.OnAllEnemiesDefeated += HandleAllEnemiesDefeated;
            hqService.OnHQDestroyed += HandleHQDestroyed;
        
            OnGameStart?.Invoke();
        }

        private void HandlePlayerDeath()
        {
            Debug.Log("Player died - Game Over");
            
            OnPlayerDeath?.Invoke();
            OnGameEnd?.Invoke();
        }

        private void HandleAllEnemiesDefeated()
        {
            Debug.Log("All enemies defeated - Victory!");
            
            OnGameEnd?.Invoke();
        }
        
        private void HandleHQDestroyed()
        {
            Debug.Log("HQ destroyed - Game Over!");
            
            OnHQDestroyed?.Invoke();
            OnGameEnd?.Invoke();
        }
    }
}
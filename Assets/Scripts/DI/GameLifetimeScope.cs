using HQ;
using Managers;
using Player;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Configuration")]
        [SerializeField] private LevelConfigSO levelConfig;
        
        [Header("Prefabs")]
        [SerializeField] private TankPlayer playerTankPrefab;
        [SerializeField] private TankEnemy enemyTankPrefab;

        [Header("Scene References")]
        [SerializeField] private SpawnPoint playerSpawnPoint;
        [SerializeField] private SpawnPoint[] enemySpawnPoints;
        [SerializeField] private TankObjectPooling tankObjectPooling;
        [SerializeField] private HQView hqView;
        [SerializeField] private PlayerLivesView playerLivesView;
        [SerializeField] private GameOver gameOverView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigurations(builder);
            
            RegisterPrefabs(builder);
            
            RegisterSceneComponents(builder);

            RegisterServices(builder);
            
            RegisterModels(builder);
            
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
        
        private void RegisterConfigurations(IContainerBuilder builder)
        {
            builder.RegisterInstance(levelConfig);
        }
        
        private void RegisterPrefabs(IContainerBuilder builder)
        {
            builder.RegisterInstance(playerTankPrefab).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(enemyTankPrefab).AsImplementedInterfaces().AsSelf();
        }

        private void RegisterSceneComponents(IContainerBuilder builder)
        {
            // UI Components
            builder.RegisterComponent(hqView);
            builder.RegisterComponent(gameOverView);
            builder.RegisterComponent(playerLivesView);

            // Gameplay Components
            builder.RegisterComponent(playerSpawnPoint).WithParameter("playerSpawn");
            builder.RegisterComponent(enemySpawnPoints).WithParameter("enemySpawns");
            builder.RegisterComponent(tankObjectPooling);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            // Core services
            builder.Register<GameManager>(Lifetime.Singleton);
            builder.Register<HQService>(Lifetime.Singleton);
        
            // Player-related services
            builder.Register<PlayerManager>(Lifetime.Singleton);
            builder.Register<PlayerLivesService>(Lifetime.Singleton);
        
            // Enemy-related services
            builder.Register<EnemyManager>(Lifetime.Singleton);
        }

        private void RegisterModels(IContainerBuilder builder)
        {
            builder.Register<PlayerLivesModel>(Lifetime.Singleton).WithParameter(3);
        }
    }
}
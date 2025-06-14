using Managers;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfigSO levelConfig;
        [SerializeField] private TankPlayer playerTankPrefab;
        [SerializeField] private TankEnemy enemyTankPrefab;
        [SerializeField] private SpawnPoint playerSpawnPoint;
        [SerializeField] private SpawnPoint[] enemySpawnPoints;
        [SerializeField] private TankObjectPooling tankObjectPooling;
        [SerializeField] private HQView hqView;
        [SerializeField] private GameOver gameOverView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(levelConfig);
            builder.RegisterInstance(playerTankPrefab).WithParameter("playerTank");
            builder.RegisterInstance(enemyTankPrefab).WithParameter("enemyTank");
            
            builder.RegisterComponent(playerSpawnPoint).WithParameter("playerSpawn");
            builder.RegisterComponent(enemySpawnPoints).WithParameter("enemySpawns");
            builder.RegisterComponent(tankObjectPooling);
            builder.RegisterComponent(hqView);
            builder.RegisterComponent(gameOverView);

            builder.Register<GameManager>(Lifetime.Singleton);
            builder.Register<PlayerManager>(Lifetime.Singleton);
            builder.Register<EnemyManager>(Lifetime.Singleton);
            builder.Register<HQService>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}
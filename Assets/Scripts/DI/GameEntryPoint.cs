using HQ;
using Managers;
using VContainer.Unity;

namespace DI
{
    public class GameEntryPoint : IStartable
    {
        private readonly GameManager gameManager;
        private readonly PlayerManager playerManager;
        private readonly EnemyManager enemyManager;
        private readonly HQService hqService;
        
        public GameEntryPoint(GameManager gameManager, PlayerManager playerManager, EnemyManager enemyManager, HQService hqService)
        {
            this.gameManager = gameManager;
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.hqService = hqService;
        }
        
        public void Start()
        {
            gameManager.Initialize();
            playerManager.Initialize();
            enemyManager.Initialize();
            
            // HQService initializes automatically via constructor
        }
    }
}
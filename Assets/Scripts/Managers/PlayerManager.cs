using System;
using DI;
using Player;
using VContainer;
using Object = UnityEngine.Object;

namespace Managers
{
    public class PlayerManager
    {
        [Inject] private readonly TankPlayer tankPrefab;
        [Inject] private readonly SpawnPoint spawnPoint;
        [Inject] private readonly PlayerLivesService playerLivesService;
        
        private Tank currentTank;
    
        public event Action OnPlayerDeath;

        public void Initialize()
        {
            InitPlayerTank();
                
            Summon();
        
            currentTank.OnDeathEvent += HandleTankDeath;
        }

        private void InitPlayerTank()
        {
            currentTank = Object.Instantiate(tankPrefab);
        }
    
        private void Summon()
        {
            currentTank.transform.position = spawnPoint.transform.position;
            currentTank.transform.rotation = spawnPoint.transform.rotation;
            currentTank.gameObject.SetActive(true);
        }

        private void HandleTankDeath(Tank tank)
        {
            currentTank.gameObject.SetActive(false);
            
            playerLivesService.LoseLife();

            if (playerLivesService.CurrentLives <= 0)
            {
                OnPlayerDeath?.Invoke();
                
                return;
            }
            
            Summon();
        }
    }
}

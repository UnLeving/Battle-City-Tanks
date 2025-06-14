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

        private void HandleTankDeath()
        {
            OnPlayerDeath?.Invoke();
        
            // Respawn after delay
            Object.Destroy(currentTank.gameObject, 2f);
        }
    }
}

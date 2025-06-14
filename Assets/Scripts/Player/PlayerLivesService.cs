using System;

namespace Player
{
    public class PlayerLivesService
    {
        private readonly PlayerLivesModel _playerLives;

        public event Action<int> OnLivesChanged;

        public int CurrentLives => _playerLives.CurrentLives;

        public PlayerLivesService(PlayerLivesModel playerLives)
        {
            _playerLives = playerLives;
        }

        public void LoseLife()
        {
            _playerLives.LoseLife();
            
            OnLivesChanged?.Invoke(_playerLives.CurrentLives);
        }

        public void GainLife()
        {
            _playerLives.GainLife();
            
            OnLivesChanged?.Invoke(_playerLives.CurrentLives);
        }
    }
}
using Player;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI
{
    public class PlayerLivesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        private PlayerLivesService _playerLivesService;
        
        [Inject]
        public void Construct(PlayerLivesService playerLivesService)
        {
            _playerLivesService = playerLivesService;
            UpdateLivesDisplay(_playerLivesService.CurrentLives);
            
            _playerLivesService.OnLivesChanged += UpdateLivesDisplay;
        }

        private void OnDestroy()
        {
            if (_playerLivesService != null)
            {
                _playerLivesService.OnLivesChanged -= UpdateLivesDisplay;
            }
        }

        private void UpdateLivesDisplay(int currentLives)
        {
            if (valueText != null)
            {
                valueText.text = currentLives.ToString();
            }
        }
    }
}
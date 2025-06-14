using UnityEngine;
using DG.Tweening;
using DI;
using VContainer;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Transform startPositionTransform;
        [SerializeField] private Transform endPositionTransform;
        [SerializeField] private Transform viewTransform;
        [SerializeField] private FloatSO lerpDuration;
        
        private GameManager _gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        private void Start()
        {
            viewTransform.position = startPositionTransform.position;
            
            _gameManager.OnHQDestroyed += PlayAnimation;
        }
        
        private void OnDestroy()
        {
            _gameManager.OnHQDestroyed -= PlayAnimation;
        }

        private void PlayAnimation()
        {
            viewTransform.DOMoveY(endPositionTransform.position.y, lerpDuration.Value);
        }
    }
}
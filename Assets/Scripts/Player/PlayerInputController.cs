using DI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private float rotationOffset = -90f; // Adjust in Inspector if needed
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Tank tank;
        [SerializeField] private FloatSO moveSpeed;
        [SerializeField] private PlayerInput playerInput;

        private Vector2 _moveInput;

        private void OnEnable()
        {
            tank.OnHitEvent += TankOnOnHitEvent;
        }

        private void OnDisable()
        {
            tank.OnHitEvent -= TankOnOnHitEvent;
        }
    
        private void TankOnOnHitEvent()
        {
            
        }

        public void OnMove(InputValue input)
        {
            _moveInput = input.Get<Vector2>();
        }

        private void OnAttack(InputValue input)
        {
            tank.Cannon.Fire();
        }

        private void Update()
        {
            Vector3 movement = new Vector3(_moveInput.x, _moveInput.y, 0) * (moveSpeed.Value * Time.deltaTime);
            
            transform.position += movement;

            if (_moveInput == Vector2.zero) return;
            
            float angle = Mathf.Atan2(_moveInput.y, _moveInput.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
                
            ChangeRendererSpriteOnMove();
        }

        private void ChangeRendererSpriteOnMove()
        {
            spriteRenderer.sprite = spriteRenderer.sprite == tank.tankSO.tanks[0].f1
                ? tank.tankSO.tanks[0].f0
                : tank.tankSO.tanks[0].f1;
        }
    }
}
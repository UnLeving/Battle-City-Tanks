using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private float rotationOffset = -90f; // Adjust in Inspector if needed
        [SerializeField] private FloatSO moveSpeed;

        private Tank _tank;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _moveInput;

        private void Awake()
        {
            _tank = GetComponent<Tank>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void OnMove(InputValue input)
        {
            Vector2 rawInput = input.Get<Vector2>();
    
            // If no input, clear movement
            if (rawInput == Vector2.zero)
            {
                _moveInput = Vector2.zero;
                return;
            }
    
            // Check if there's new input on X axis
            if (Mathf.Abs(rawInput.x) > 0 && Mathf.Abs(_moveInput.x) == 0)
            {
                _moveInput = new Vector2(rawInput.x, 0);
            }
            // Check if there's new input on Y axis
            else if (Mathf.Abs(rawInput.y) > 0 && Mathf.Abs(_moveInput.y) == 0)
            {
                _moveInput = new Vector2(0, rawInput.y);
            }
            // If currently moving horizontally and vertical input is pressed
            else if (Mathf.Abs(_moveInput.x) > 0 && Mathf.Abs(rawInput.y) > 0)
            {
                _moveInput = new Vector2(0, rawInput.y);
            }
            // If currently moving vertically and horizontal input is pressed
            else if (Mathf.Abs(_moveInput.y) > 0 && Mathf.Abs(rawInput.x) > 0)
            {
                _moveInput = new Vector2(rawInput.x, 0);
            }
            // Update the current direction if still pressing the same axis
            else if (Mathf.Abs(_moveInput.x) > 0)
            {
                _moveInput = new Vector2(rawInput.x, 0);
            }
            else if (Mathf.Abs(_moveInput.y) > 0)
            {
                _moveInput = new Vector2(0, rawInput.y);
            }
        }

        private void OnAttack(InputValue input)
        {
            _tank.Cannon.Fire();
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
            _spriteRenderer.sprite = _spriteRenderer.sprite == _tank.tankSO.tanks[0].f1
                ? _tank.tankSO.tanks[0].f0
                : _tank.tankSO.tanks[0].f1;
        }
    }
}
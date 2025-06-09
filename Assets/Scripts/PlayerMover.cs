using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationOffset = -90f; // Adjust in Inspector if needed
    
    private Vector2 _moveInput;

    // Called when move input changes (automatically hooked up by Input System)
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(_moveInput.x, _moveInput.y, 0) * (moveSpeed * Time.deltaTime);
        transform.position += movement;
        
        if (_moveInput != Vector2.zero)
        {
            // Calculate the angle based on the input vector
            float angle = Mathf.Atan2(_moveInput.y, _moveInput.x) * Mathf.Rad2Deg;

            // Apply the rotation offset to account for your sprite's default facing direction
            transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
        }   
    }
}
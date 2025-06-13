using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float rotationOffset = -90f; // Adjust in Inspector if needed
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Tank tank;
    [SerializeField] private FloatSO moveSpeed;
    [SerializeField] private PlayerInput playerInput;

    private Vector2 _moveInput;

    private void OnEnable()
    {
        playerInput.enabled = true;
        
        tank.OnHitEvent += TankOnOnHitEvent;
    }

    private void OnDisable()
    {
        playerInput.enabled = false;
        
        tank.OnHitEvent -= TankOnOnHitEvent;
    }
    
    private void TankOnOnHitEvent()
    {
        playerInput.enabled = false;
    }

    // Called when move input changes (automatically hooked up by Input System)
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(_moveInput.x, _moveInput.y, 0) * (moveSpeed.Value * Time.deltaTime);

        transform.position += movement;

        if (_moveInput != Vector2.zero)
        {
            // Calculate the angle based on the input vector
            float angle = Mathf.Atan2(_moveInput.y, _moveInput.x) * Mathf.Rad2Deg;

            // Apply the rotation offset to account for your sprite's default facing direction
            transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

            ChangeRendererSpriteOnMove();
        }
    }

    private void ChangeRendererSpriteOnMove()
    {
        spriteRenderer.sprite = spriteRenderer.sprite == tank.tankSO.tanks[0].f1
            ? tank.tankSO.tanks[0].f0
            : tank.tankSO.tanks[0].f1;
    }
}
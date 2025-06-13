using System;
using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Tank : MonoBehaviour, IPoolable<Tank>, IDamageable
{
    [SerializeField] private SpriteRendererEffect spriteRendererEffect;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D _collider;
    public TankSO tankSO;
    [field: SerializeField] public Cannon Cannon { get; private set; }
    public IObjectPool<Tank> ObjectPool { get; set; }

    public event Action OnHitEvent;
    public event Action OnDeathEvent;

    private void OnEnable()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnAttack(InputValue value)
    {
        Cannon.Fire();
    }

    public void OnAttack()
    {
        OnAttack(null);
    }

    public void OnHit()
    {
        OnHitEvent?.Invoke();

        OnDisable();

        spriteRendererEffect.PlayEffect(0f, () =>
        {
            gameObject.SetActive(false);
            
            OnDeathEvent?.Invoke();
        });
    }
}
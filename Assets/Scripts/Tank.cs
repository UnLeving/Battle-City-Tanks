using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour, IDamageable
{
    [SerializeField] private SpriteRendererEffect explosionSpriteRendererEffect;
    [SerializeField] private SpriteRendererEffect invincibilitySpriteRendererEffect;
    
    private SpriteRenderer _renderer;
    private Collider2D _collider;
    public TankSO tankSO;
    public Cannon Cannon { get; private set; }

    public event Action OnHitEvent;
    public event Action<Tank> OnDeathEvent;

    private bool _isInvincible = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        
        Cannon = GetComponentInChildren<Cannon>();
    }

    private void OnEnable()
    {
        _renderer.enabled = true;
        _collider.enabled = true;

        _isInvincible = true;

        invincibilitySpriteRendererEffect.PlayEffect(5f, () => { _isInvincible = false; });
    }

    private void OnDisable()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }
    
    public void OnHit()
    {
        if (_isInvincible) return;

        OnHitEvent?.Invoke();

        OnDisable();

        explosionSpriteRendererEffect.PlayEffect(0f, () =>
        {
            gameObject.SetActive(false);

            OnDeathEvent?.Invoke(this);
        });
    }
}
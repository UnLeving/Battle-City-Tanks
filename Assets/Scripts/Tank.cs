using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Tank : MonoBehaviour, IPoolable<Tank>, IDamageable
{
    public TankSO tankSO;
    [field: SerializeField] public Cannon Cannon {  get; private set; }
    private void OnAttack(InputValue value)
    {
        Cannon.Fire();
    }

    public void OnAttack()
    {
        OnAttack(null);
    }

    public IObjectPool<Tank> ObjectPool { get; set; }
    public void OnHit()
    {
        Destroy(gameObject);
    }
}
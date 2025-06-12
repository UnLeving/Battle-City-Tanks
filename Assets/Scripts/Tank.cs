using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Tank : MonoBehaviour, IPoolable<Tank>
{
    public TankSO tankSO;
    [SerializeField] private Cannon cannon;
    
    private void OnAttack(InputValue value)
    {
        //Debug.Log("Attack");
        
        cannon.Fire();
    }

    public void OnAttack()
    {
        OnAttack(null);
    }

    public IObjectPool<Tank> ObjectPool { get; set; }
}
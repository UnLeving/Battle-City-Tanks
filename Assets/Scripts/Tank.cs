using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    public TankSO tankSO;
    [SerializeField] private Cannon cannon;
    
    private void OnAttack(InputValue value)
    {
        Debug.Log("Attack");
        
        cannon.Fire();
    }
}
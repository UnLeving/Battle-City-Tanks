using DI;
using UnityEngine;
using VContainer;

public class HQView : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject aliveView;
    [SerializeField] private GameObject deadView;

    private HQService hqService;
    
    public void Initialize(HQService hqService)
    {
        this.hqService = hqService;
        
        ShowAliveState();
    }
    
    public void OnHit()
    {
        hqService?.TakeDamage();
    }

    public void ShowDestroyedState()
    {
        deadView.SetActive(true);
        aliveView.SetActive(false);
    }

    public void ShowAliveState()
    {
        aliveView.SetActive(true);
        deadView.SetActive(false);
    }
}
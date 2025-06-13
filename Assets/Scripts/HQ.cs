using UnityEngine;

public class HQ : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject aliveView;
    [SerializeField] private GameObject deadView;

    private void OnDeath()
    {
        deadView.SetActive(true);
        aliveView.SetActive(false);
    }

    public void OnHit()
    {
        OnDeath();
    }
}
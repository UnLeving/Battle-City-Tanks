using UnityEngine;

public class HQ : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject deadView;

    private void OnDeath()
    {
        deadView.SetActive(true);
    }

    public void OnHit()
    {
        OnDeath();
    }
}
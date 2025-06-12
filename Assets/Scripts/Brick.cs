using UnityEngine;

public class Brick : MonoBehaviour, IDamageable
{
    public void OnHit()
    {
        gameObject.SetActive(false);
    }
}

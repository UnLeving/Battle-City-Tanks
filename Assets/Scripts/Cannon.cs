using Helpers;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private FloatSO shootInterval;
    private float _lastFireTime;
    
    public void Fire()
    {
        if (Time.time - _lastFireTime < shootInterval.Value) return;
        
        var round = ObjectPooling<Round>.Instance.Item;
       
       round.Init(transform.up, transform.position);
       
       _lastFireTime = Time.time;
       
       //Debug.Break();
    }
}
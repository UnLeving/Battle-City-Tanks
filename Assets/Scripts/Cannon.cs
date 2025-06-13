using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private FloatSO shootInterval;
    [SerializeField] private Round round;
    private float _lastFireTime;

    private void Start()
    {
        round.gameObject.SetActive(false);
    }

    public void Fire()
    {
        if (round.Released == false || Time.time - _lastFireTime < shootInterval.Value) return;

        round.gameObject.SetActive(true);

        round.  Init(transform.up, transform.position);

        _lastFireTime = Time.time;
    }
}
using System;
using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.Pool;

public class Round : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatSO speed;
    [SerializeField] private Transform viewTransform;
[SerializeField] private Collider2D colliderToExclude;
    private Coroutine _fireCoroutine;
    private float rotationOffset = -90f;

    public bool Released { get; private set;}
    private Transform _parentTransform;

    private void Start()
    {
        Released = true;
        
        _parentTransform = transform.parent;
    }

    public void Init(Vector3 dir, Vector3 startPos)
    {
        transform.parent = null;
        transform.position = startPos;

        // Calculate the angle based on the input vector
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Apply the rotation offset to account for your sprite's default facing direction
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        _fireCoroutine = StartCoroutine(Fire(dir));

        Released = false;
    }

    private IEnumerator Fire(Vector3 dir)
    {
        while (true)
        {
            transform.Translate(dir * (speed.Value * Time.deltaTime),  Space.World);

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider == colliderToExclude) return;
        
        StopCoroutine(_fireCoroutine);

        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnHit();
        }

        OnHit();
    }

    public void OnHit()
    {
        if (Released) return;

        Released = true;
        
        gameObject.SetActive(false);
        
        transform.parent = _parentTransform;
    }
}
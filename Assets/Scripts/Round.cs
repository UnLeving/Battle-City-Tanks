using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.Pool;

public class Round : MonoBehaviour, IPoolable<Round>
{
    [SerializeField] private FloatSO speed;
    [SerializeField] private Transform viewTransform;
    
    private Coroutine _fireCoroutine;
    private IObjectPool<Round> _objectPool;
    private float rotationOffset = -90f;
    public IObjectPool<Round> ObjectPool { set => _objectPool = value; }

    private bool _released;
    
    public void Init(Vector3 dir, Vector3 startPos)
    {
        transform.position = startPos;
        
        // Calculate the angle based on the input vector
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Apply the rotation offset to account for your sprite's default facing direction
        viewTransform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
        
        _fireCoroutine = StartCoroutine(Fire(dir));
        
        _released = false;
    }

    private IEnumerator Fire(Vector3 dir)
    {
        while (true)
        {
            transform.Translate(dir * (speed.Value * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D: " + other.gameObject.name);
        
        StopCoroutine(_fireCoroutine);

        Destroy(other.gameObject);
        
        if(_released) return;
        
        _objectPool.Release(this);
        
        _released = true;
    }
}
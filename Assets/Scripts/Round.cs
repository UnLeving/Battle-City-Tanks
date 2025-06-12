using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Round : MonoBehaviour
{
    [SerializeField] private FloatSO speed;
    
    private Coroutine _fireCoroutine;
    private IObjectPool<Round> _objectPool;
        
    public IObjectPool<Round> ObjectPool { set => _objectPool = value; }

    public void Init(Vector3 dir, Vector3 startPos)
    {
        transform.position = startPos;
        
        _fireCoroutine = StartCoroutine(Fire(dir));
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
        
        _objectPool.Release(this);
    }
}
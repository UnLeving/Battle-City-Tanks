using System.Collections;
using UnityEngine;

public class Round : MonoBehaviour
{
    private Coroutine _fireCoroutine;

    public void Init(Vector3 dir)
    {
        _fireCoroutine = StartCoroutine(Fire(dir));
    }

    private IEnumerator Fire(Vector3 dir)
    {
        while (true)
        {
            transform.Translate(dir * (.5f * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D: " + other.gameObject.name);
        
        StopCoroutine(_fireCoroutine);
    }
}
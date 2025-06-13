using System;
using System.Collections;
using UnityEngine;

public class SpriteRendererEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRendererEffectsSO spriteRendererEffectsSO;

    public void PlayEffect(float duration = 0f, Action onFinished = null)
    {
        StartCoroutine(Play(duration, onFinished));
    }
    
    private IEnumerator Play(float duration = 0f, Action onFinished = null)
    {
        float timer = 0f;

        do
        {
            //Debug.Log("inside DO");
            
            for (int i = 0; i < spriteRendererEffectsSO.sprites.Length; i++)
            {
                timer += Time.deltaTime;

                //Debug.Log("inside for");
                
                spriteRenderer.sprite = spriteRendererEffectsSO.sprites[i];

                yield return new WaitForSeconds(spriteRendererEffectsSO.delay);
            }
        } while (spriteRendererEffectsSO.loop || (duration > 0f && timer < duration));

        //Debug.Log("Finished");

        spriteRenderer.sprite = null;
        
        onFinished?.Invoke();
    }
}
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
        float startTime = Time.time;

        do
        {
            //Debug.Log("inside DO");
            
            for (int i = 0; i < spriteRendererEffectsSO.sprites.Length; i++)
            {
                spriteRenderer.sprite = spriteRendererEffectsSO.sprites[i];

                yield return new WaitForSeconds(spriteRendererEffectsSO.delay);
                
                if (duration > 0f && Time.time - startTime >= duration)
                    break;
            }
            
            //Debug.Log("inside for: duration: " + duration + " timer: " + timer);
            
        } while (spriteRendererEffectsSO.loop || (duration > 0f && Time.time - startTime < duration));

        //Debug.Log("Finished");

        spriteRenderer.sprite = null;
        
        onFinished?.Invoke();
    }
}
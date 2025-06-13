using UnityEngine;

[CreateAssetMenu(fileName = "SpriteRendererEffects", menuName = "SpriteRendererEffects")]
public class SpriteRendererEffectsSO : ScriptableObject
{
    public Sprite[] sprites;
    public float delay;
    public bool loop;
}
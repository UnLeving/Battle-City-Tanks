using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    public string Name;
    public int Level;
    public int EnemyCount;
}
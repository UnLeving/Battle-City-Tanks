using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Tank tankPrefab;
    [SerializeField] private SpawnPoint spawnPoint;
    
    private Tank _tank;
    
    private void Start()
    {
        InstantiateTank();

        Summon();
        
        _tank.OnDeathEvent += Summon;
    }
    
    private void InstantiateTank()
    {
        _tank = Instantiate(tankPrefab);
    }
    
    private void Summon()
    {
        _tank.transform.position = spawnPoint.transform.position;
        _tank.transform.rotation = spawnPoint.transform.rotation;
        
        _tank.gameObject.SetActive(true);
    }
}

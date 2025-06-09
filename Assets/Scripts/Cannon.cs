using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Round round;

    public void Fire()
    {
       var _round = Instantiate(round, transform.position, Quaternion.identity);
       
       _round.Init(Vector3.right);
    }
}
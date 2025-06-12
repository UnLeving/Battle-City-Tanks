using UnityEngine;

public class Cannon : MonoBehaviour
{
    public void Fire()
    {
        var _round = RoundObjectPooling.Instance.Round;
       
       _round.Init(transform.up, transform.position);
    }
}
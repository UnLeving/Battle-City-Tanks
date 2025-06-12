using Helpers;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public void Fire()
    {
        var _round = ObjectPooling<Round>.Instance.Item;
       
       _round.Init(transform.up, transform.position);
    }
}
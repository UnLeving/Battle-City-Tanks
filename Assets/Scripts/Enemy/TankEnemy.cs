using Helpers;
using UnityEngine.Pool;

public class TankEnemy : Tank, IPoolable<TankEnemy>
{
    public IObjectPool<TankEnemy> ObjectPool { get; set; }
}
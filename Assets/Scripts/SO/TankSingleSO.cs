using UnityEngine;

[ CreateAssetMenu( fileName = "Tank", menuName = "TankSingle")]
public class TankSingleSO : ScriptableObject
{
    public int level;
    public Sprite f0, f1;
    public Sprite l0, fl;
    public Sprite b0, b1;
    public Sprite r0, r1;
}
using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Tank tank;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D: " + other.gameObject.name);
    }
}
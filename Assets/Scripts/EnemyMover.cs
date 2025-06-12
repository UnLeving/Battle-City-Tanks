using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Tank tank;
    [SerializeField] private float rotationOffset = -90f;
    [SerializeField] private FloatSO moveSpeed;
    [SerializeField] private float directionChangeInterval = 2f;

    private Vector2Int currentDirection = Vector2Int.up;
    private Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    private float lastDirectionChange;

    private void Update()
    {
        ChooseNewDirection();

        UpdateMovement();

        UpdateRotation();

        UpdateFiring();
    }

    private void UpdateRotation()
    {
        if (currentDirection == Vector2.zero) return;

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        ChangeRendererSpriteOnMove();
    }

    private void UpdateMovement()
    {
        Vector3 movement = new Vector3(currentDirection.x, currentDirection.y, 0) * (moveSpeed.Value * Time.deltaTime);

        transform.position += movement;
    }

    private void ChangeRendererSpriteOnMove()
    {
        spriteRenderer.sprite = spriteRenderer.sprite == tank.tankSO.tanks[0].f1
            ? tank.tankSO.tanks[0].f0
            : tank.tankSO.tanks[0].f1;
    }

    private void ChooseNewDirection()
    {
        if (!(Time.time - lastDirectionChange > directionChangeInterval)) return;

        currentDirection = directions[Random.Range(0, directions.Length)];

        lastDirectionChange = Time.time;
    }
    
    private void UpdateFiring()
    {
        tank.OnAttack();
    }
}
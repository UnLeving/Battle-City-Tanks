using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Tank tank;
    [SerializeField] private float rotationOffset = -90f;
    [SerializeField] private FloatSO moveSpeed;
    [SerializeField] private float directionChangeInterval = 2f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform obstacleDetectorTransform;
    
    private Vector2Int currentDirection = Vector2Int.up;
    private Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    private float lastDirectionChange;

    private bool canMove;
    
    private void OnEnable()
    {
        Debug.Log("EnemyMover.OnEnable");
        
        tank.OnHitEvent += TankOnOnHitEvent;
        
        canMove = true;
    }

    private void Start()
    {
        currentDirection = Vector2Int.down;

        lastDirectionChange = Time.time;
    }
    
    private void OnDisable()
    {
        Debug.Log("EnemyMover.OnDisable");
        
        tank.OnHitEvent -= TankOnOnHitEvent;
        
        canMove = false;
    }

    private void Update()
    {
        if(canMove == false) return;
        
        ChooseNewDirection();

        UpdateMovement();

        UpdateRotation();

        UpdateFiring();
    }
    
    private void TankOnOnHitEvent()
    {
        canMove = false;
    }

    private void UpdateRotation()
    {
        if (currentDirection == Vector2.zero) return;

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

        //Debug.Log(angle);
        
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        ChangeRendererSpriteOnMove();
    }

    private void UpdateMovement()
    {
        if (IsObstacleAhead())
        {
            lastDirectionChange = 0f;
        }

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

    private bool IsObstacleAhead()
    {
        var obstacle = Physics2D.Raycast(obstacleDetectorTransform.position, currentDirection, 0.1f, obstacleLayer);

        //Debug.Log(obstacle.collider);
        
        return obstacle;
    }

    private void UpdateFiring()
    {
        tank.OnAttack();
    }
}
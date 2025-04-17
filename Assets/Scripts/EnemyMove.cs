using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _moveInterval = 0.5f;

    [SerializeField]
    private LayerMask _whatStopsMovement; // Match this with player's whatStopsMovement

    [SerializeField]
    private float _gridSize = 0.5f;

    private float _moveTimer;

    private EnemyDetect _enemyDetect;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _enemyDetect = GetComponent<EnemyDetect>();
    }

    private void Update()
    {
        _moveTimer -= Time.deltaTime;

        if (_moveTimer <= 0f)
        {
            UpdateMoveDirection();

            if (_moveDirection != Vector2.zero && CanMoveInDirection(_moveDirection))
            {
                MoveOneStep();
                _moveTimer = _moveInterval;
            }
        }
    }

    private void UpdateMoveDirection()
    {
        if (_enemyDetect.AwareOfPlayer)
        {
            Vector2 dirToPlayer = _enemyDetect.DirectionToPlayer;

            if (Mathf.Abs(dirToPlayer.x) > Mathf.Abs(dirToPlayer.y))
            {
                _moveDirection = dirToPlayer.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                _moveDirection = dirToPlayer.y > 0 ? Vector2.up : Vector2.down;
            }
        }
        else
        {
            _moveDirection = Vector2.zero;
        }
    }

    private void MoveOneStep()
    {
        Vector2 newPosition = (Vector2)transform.position + (_moveDirection * _gridSize);
        transform.position = newPosition;

        float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private bool CanMoveInDirection(Vector2 direction)
    {
        Vector2 targetPos = (Vector2)transform.position + direction * _gridSize;

        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, _whatStopsMovement);

        return hit == null;
    }
}
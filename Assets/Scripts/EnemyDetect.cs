using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _enemyDetectDistance;

    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _enemyDetectDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
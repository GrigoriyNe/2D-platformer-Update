using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private EnemyVision _vision;
    [SerializeField] private EnemyAtacker _atacker;
    [SerializeField] private EnemyAnimationDirect _animationDirect;

    private Enemy _enemy;

    private int _finishValueWaypoint = 1;
    private int _distanseAttack = 1;
    private int _currentWaypoint = 0;
    private int _degreeTurn = 180;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Update()
    {
        LogicMovement();
    }

    private void Move()
    {
        _animationDirect.SetAnimationWalk();

        float step = _speed * Time.deltaTime;
        Vector2 rotate = transform.eulerAngles;
        Vector2 startRotate = transform.eulerAngles;
        rotate.y = _degreeTurn;
        startRotate.y = 0;

        if (transform.position == _waypoints[_currentWaypoint].position)
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        Vector2 direction = _waypoints[_currentWaypoint].position;

        if (_currentWaypoint == _finishValueWaypoint || _waypoints[_currentWaypoint].transform.position.x < transform.position.x)
            transform.rotation = (Quaternion.Euler(rotate));
        else
            transform.rotation = (Quaternion.Euler(startRotate));

        transform.position = Vector2.MoveTowards(transform.position, direction, step);
    }

    private void LogicMovement()
    {
        if (_vision.IsPlayerSee)
        {
            FollowToPlayer(_vision.Target);
        }
        else if (_enemy.IsAlive == false)
        {
            return;
        }
        else
        {
            Move();
        }
    }

    private void FollowToPlayer(Vector2 target)
    {
        if (_enemy.IsAlive)
        {
            float step = _speed * Time.deltaTime;

            if (transform.position.x > target.x && transform.position.x - target.x <= _distanseAttack || target.x > transform.position.x && target.x - transform.position.x <= _distanseAttack)
                _atacker.Update();
            else
                transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }
}

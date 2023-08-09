using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MoveHorizontal))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Flip))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string HorizontalSpeed = "HorizontalSpeed";
    private const string Attack = "Attack";
    private const string IsDead = "isDead";

    [SerializeField] private Transform _rightBorderWaypoint;
    [SerializeField] private Transform _leftBorderWaypoint;
    [SerializeField] private Transform _target;

    private Transform _currentWaypoint;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private MoveHorizontal _moveHorizontal;
    private Flip _flip;
    private Attack _attack;
    private float _speed;
    private int _horizontalSpeedIndex;
    private int _attackIndex;
    private int _isDeadIndex;
    private float _followDistance;
    private float _attackDistance;
    private float attackRate = 0.5f;
    private float _nextAttackTime = 0f;

    private void Awake()
    {
        _currentWaypoint = _rightBorderWaypoint;
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveHorizontal = GetComponent<MoveHorizontal>();
        _attack = GetComponent<Attack>();
        _flip = GetComponent<Flip>();
        _speed = 0.5f;
        _followDistance = 1.5f;
        _attackDistance = 0.4f;
    }

    private void Start()
    {
        _horizontalSpeedIndex = Animator.StringToHash(HorizontalSpeed);
        _attackIndex = Animator.StringToHash(Attack);
        _isDeadIndex = Animator.StringToHash(IsDead);
    }

    private void Update()
    {
        _animator.SetFloat(_horizontalSpeedIndex, 
            Mathf.Abs(_rigidBody.velocity.x));

        if (_animator.GetBool(_isDeadIndex) == false && _target != null)
        {
            if (IsInRange(_attackDistance))
                AttackTarget();
            else if (IsInRange(_followDistance))
                FollowTarget();
            else
                Patrol();
        }
    }
    private void OnDrawGizmos()
    {
        float gizmoRadius = 0.1f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_leftBorderWaypoint.position, gizmoRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_rightBorderWaypoint.position, gizmoRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_rightBorderWaypoint.position,
            _leftBorderWaypoint.position);
    }

    private void FollowTarget()
    {
        float distance = _target.position.x - transform.position.x;
        float followSpeed = _speed;

        if ((distance < 0 && _speed > 0) || (distance > 0 && _speed < 0))
            followSpeed = -followSpeed;
        
        _moveHorizontal.MoveEvent(followSpeed);
        _flip.FlipEvent();
    }

    private void AttackTarget()
    {
        float stopSpeed = 0f;
        _moveHorizontal.MoveEvent(stopSpeed);

        if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger(_attackIndex);
            _attack.AttackEvent();
            _nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private bool IsInRange(float range)
    {
        float minOffsetY = 0.3f;
        float distanceX = Mathf.Abs(_target.position.x - transform.position.x);
        float distanceY = Mathf.Abs(_target.position.y - transform.position.y);
        return distanceX <= range && distanceY <= minOffsetY;
    }

    private void Patrol()
    {
        float distance = Vector2.Distance(transform.position, 
            _currentWaypoint.position);

        if (distance <= 0.5f && _currentWaypoint == _rightBorderWaypoint)
        {
            _currentWaypoint = _leftBorderWaypoint;
            _speed *= -1;
        }
        else if (distance <= 0.5f && _currentWaypoint == _leftBorderWaypoint)
        {
            _currentWaypoint = _rightBorderWaypoint;
            _speed *= -1;
        }

        _flip.FlipEvent();
        _moveHorizontal.MoveEvent(_speed);
    }    
}

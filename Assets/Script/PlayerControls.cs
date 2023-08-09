using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Health))]
public class PlayerControls : MonoBehaviour
{
    private const string HorizontalSpeed = "HorizontalSpeed";
    private const string VerticalSpeed = "VerticalSpeed";
    private const string Attack = "Attack";
    private const string IsDead = "isDead";

    [SerializeField] private UnityEvent _walkRightEvent;
    [SerializeField] private UnityEvent _walkLeftEvent;
    [SerializeField] private UnityEvent _runRightEvent;
    [SerializeField] private UnityEvent _runLeftEvent;
    [SerializeField] private UnityEvent _flipEvent;
    [SerializeField] private UnityEvent _moveVerticalEvent;
    [SerializeField] private UnityEvent _attackEvent;

    private int _horizontalSpeedIndex;
    private int _verticalSpeedIndex;
    private int _attackIndex;
    private int _isDeadIndex;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private float attackRate = 2f;
    private float _nextAttackTime = 0f;

    void Start()
    {
        _horizontalSpeedIndex = Animator.StringToHash(HorizontalSpeed);
        _verticalSpeedIndex = Animator.StringToHash(VerticalSpeed);
        _attackIndex = Animator.StringToHash(Attack);
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _isDeadIndex = Animator.StringToHash(IsDead);
    }

    void Update()
    {
        _animator.SetFloat(_horizontalSpeedIndex, Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetFloat(_verticalSpeedIndex, Mathf.Abs(_rigidBody.velocity.y));

        if (_animator.GetBool(_isDeadIndex) == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Mathf.Abs(_rigidBody.velocity.y) < 0.1)
                    _moveVerticalEvent.Invoke();
            }

            if (Input.GetKey(KeyCode.D))
            {
                _flipEvent.Invoke();

                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                    _runRightEvent.Invoke();
                else
                    _walkRightEvent.Invoke();                
            }

            if (Input.GetKey(KeyCode.A))
            {
                _flipEvent.Invoke();

                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
                    _runLeftEvent.Invoke();
                else
                    _walkLeftEvent.Invoke();
            }

            if (Time.time >= _nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _animator.SetTrigger(_attackIndex);
                    _attackEvent.Invoke();
                    _nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }
}

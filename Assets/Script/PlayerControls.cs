using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    private const string HorizontalSpeed = "HorizontalSpeed";
    private const string VerticalSpeed = "VerticalSpeed";
    private const string Attack = "Attack";

    [SerializeField] private UnityEvent _moveHorizontalRightEvent;
    [SerializeField] private UnityEvent _moveHorizontalLeftEvent;
    [SerializeField] private UnityEvent _flipEvent;
    [SerializeField] private UnityEvent _moveVerticalEvent;

    private int _horizontalSpeed;
    private int _verticalSpeed;
    private int _attack;
    private Animator _animator;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _horizontalSpeed = Animator.StringToHash(HorizontalSpeed);
        _verticalSpeed = Animator.StringToHash(VerticalSpeed);
        _attack = Animator.StringToHash(Attack);
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _animator.SetFloat(_horizontalSpeed, Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetFloat(_verticalSpeed, Mathf.Abs(_rigidBody.velocity.y));
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Mathf.Abs(_rigidBody.velocity.y) < 0.1)
                _moveVerticalEvent.Invoke();
        }

        if (Input.GetKey(KeyCode.D))
        {
            _flipEvent.Invoke();
            _moveHorizontalRightEvent.Invoke();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _flipEvent.Invoke();
            _moveHorizontalLeftEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(_attack);
        }        
    }
}

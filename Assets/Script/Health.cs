using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Health : MonoBehaviour
{
    private const string Hurt = "Hurt";
    private const string IsDead = "isDead";

    [SerializeField] private int _maxHealth;
    
    private Animator _animator;
    private int _currentHealth;
    private int _hurtIndex;
    private int _isDeadIndex;
    private float _timeToDestruction = 2;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
        _currentHealth = _maxHealth;
        _hurtIndex = Animator.StringToHash(Hurt);
        _isDeadIndex = Animator.StringToHash(IsDead);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _animator.SetTrigger(_hurtIndex);

        if (_currentHealth == 0) 
        {
            _animator.SetBool(_isDeadIndex, true);
            Destroy(gameObject, _timeToDestruction);
        }
    }
}

using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _damageValue;
    [SerializeField] private Transform _attackPoint;

    private float _attackRange = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void AttackEvent()
    {
        Collider2D[] enemies = 
            Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject != _attackPoint.parent.gameObject 
                && enemy.TryGetComponent(typeof(Health), out Component health))
            {
                ((Health)health).TakeDamage(_damageValue);
            }
        }
    }

}

using UnityEngine;

public class Attack : MonoBehaviour
{
    private Transform _attackPoint;
    private float _attackRange = 0.5f;

    private void AttackEvent()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D enemy in enemies)
        {
            enemy.
        }
    }

}

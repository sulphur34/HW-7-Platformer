using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    private Transform _player;
    private float _attractionSpeed = 1f;
    private float _attractionArea = 1.5f;

    private void Awake()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.position) < _attractionArea)
        {
            MoveTowardsTarget();
        }
    }

    private void FindPlayer()
    {
        var player = FindObjectOfType(typeof(PlayerControls)).GameObject();

        if (player != null)
        {
            _player = player.transform;
        }
    }

    private void MoveTowardsTarget()
    {
        var moveVector = Vector2.MoveTowards(transform.position, _player.position, _attractionSpeed * Time.deltaTime);
        transform.position = moveVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

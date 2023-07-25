using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class MoveVertical : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void MoveEvent(float jumpSpeed)
    {
        _rigidBody.velocity = new Vector3(_rigidBody.velocity.x,jumpSpeed,0);
    }
}
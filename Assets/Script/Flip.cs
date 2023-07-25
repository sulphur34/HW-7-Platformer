using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flip : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    public void FlipEvent()
    {
        Debug.Log(transform.rotation.y);
        float horizontalSpeed = _rigidBody.velocity.x;
        if (horizontalSpeed < 0 && transform.rotation.y == 0)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (horizontalSpeed > 0 && transform.rotation.y == -1)
        {
            transform.Rotate(0, -180, 0);
        }
    }
}

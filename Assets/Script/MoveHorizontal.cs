using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class MoveHorizontal : MonoBehaviour
{ 
    
    private Rigidbody2D _rigidBody;

    private void Awake()
    {        
        _rigidBody = GetComponent<Rigidbody2D>();        
    }
    public void MoveEvent(float movementSpeed)
    {
        _rigidBody.velocity = new Vector3(movementSpeed,_rigidBody.velocity.y,0);        
    }
}

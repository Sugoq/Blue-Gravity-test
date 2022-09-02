using UnityEngine;

public class PlayerMovement : SingletonMonoBehaviour<PlayerMovement>
{        
    [SerializeField] float moveVelocity;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] Animator anim;

    private void Update()
    {
        anim.SetFloat("VelX", myRigidbody.velocity.x);
        anim.SetFloat("VelY", myRigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = moveVelocity * InputController.instance.movement.normalized;
        myRigidbody.velocity = velocity;
    }
}

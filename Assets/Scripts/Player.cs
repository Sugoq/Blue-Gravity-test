using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{        
    [SerializeField] private float moveVelocity;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (myRigidbody.velocity.magnitude > 0.01)
        {
            anim.SetFloat("VelX", myRigidbody.velocity.x);
            anim.SetFloat("VelY", myRigidbody.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = moveVelocity * InputController.instance.movement.normalized;
        myRigidbody.velocity = velocity;
    }
}

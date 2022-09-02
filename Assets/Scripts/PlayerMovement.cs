using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    
    
    [SerializeField] float moveVelocity;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    private Vector2 movement;

    private void Awake() => instance = this;

    private void OnDestroy() => instance = null;
    
        
    



    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
              
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity * movement;
        if (movement.magnitude < 0.001) return;
        anim.SetFloat("VelX", movement.x);
        anim.SetFloat("VelY", movement.y);
    }
}

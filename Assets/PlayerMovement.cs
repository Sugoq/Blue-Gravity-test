using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveVelocity;
    [SerializeField] Rigidbody2D rb;
    private Vector2 movement;

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));        
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity * movement;
    }
}

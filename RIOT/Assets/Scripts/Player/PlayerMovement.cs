using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float speed;
    Vector3 movementVec;

    [Header("Jump")]
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayers;

    //[Header("Animation")]
    //public float hopAnimThreshhold;
    //public Animator anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Movement
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayers);
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        // Animations
        transform.localScale = new Vector3(movementVec.x > 0 ? -1 : movementVec.x < 0 ? 1 : transform.localScale.x, 1, 1);
        //anim.SetBool("isMoving", Mathf.Abs(movementVec.x) > hopAnimThreshhold);
    }

    void Update()
    {
        // Movement
        if (Input.GetAxis("Jump") > 0 & isGrounded)
        {
            Jump();
        }
    }

    void Jump ()
    {
        rb.velocity = Vector2.up * jumpForce;
        //anim.SetTrigger("jump");
    }
}

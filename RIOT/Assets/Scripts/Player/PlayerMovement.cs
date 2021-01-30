using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Camera camera;

    [Header("Movement")]
    public Rigidbody2D rb;
    public float speed;
    float hInput;

    [Header("Jump")]
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayers;
    bool isGrounded;

    [Header("Walls")]
    public Transform fontWallCheck;
    public float wallSlidingSpeed;
    bool isTouchingWall;
    bool isWallSliding;
    public Transform ceilingCheck;
    bool isTouchingCeiling;
    bool isAttachCeiling;

    //[Header("Animation")]
    //public float hopAnimThreshhold;
    //public Animator anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        camera.transform.position = new Vector3(Mathf.Lerp(camera.transform.position.x, transform.position.x, 0.05f),
            Mathf.Lerp(camera.transform.position.y, transform.position.y, 0.05f),
            camera.transform.position.z);
    }

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayers);

        isTouchingWall = Physics2D.OverlapCircle(fontWallCheck.position, checkRadius, groundLayers);
        isWallSliding = isTouchingWall && !isGrounded;

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        } else
        {
        }

        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, checkRadius, groundLayers);
        isAttachCeiling = isTouchingCeiling && !isWallSliding && !isGrounded && Input.GetAxis("Jump") == 0;

        if (isAttachCeiling)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0.35f;
        } else
        {
            rb.gravityScale = 2;
        }

        if (Input.GetAxis("Jump") != 0 & (isGrounded || isWallSliding || isAttachCeiling))
        {
            Jump();
        }


        if (isAttachCeiling)
        {
            sprite.transform.rotation = Quaternion.Euler(180, 0, 0);
        } else if (isWallSliding)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 0, 90 * transform.localScale.x);
        } else
        {
            sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        transform.localScale = new Vector3(hInput > 0 ? 1 : hInput < 0 ? -1 : transform.localScale.x, 1, 1);
    }

    void Jump ()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    public void SetTransforms(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        transform.position = pos;
        sprite.transform.rotation = rot;
        transform.localScale = scale;
    }
}

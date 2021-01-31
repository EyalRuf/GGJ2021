using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Camera camera;
    public PlayerImprints pi;
    public PlayerAnimations pa;

    [Header("Movement")]
    public bool locked;
    public Rigidbody2D rb;
    public float speed;
    public float hInput;
    public bool isFalling;

    [Header("Jump")]
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayers;
    public bool isGrounded;
    public bool justJumped;

    [Header("Walls")]
    public Transform fontWallCheck;
    public float wallSlidingSpeed;
    bool isTouchingWall;
    public bool isWallSliding;
    public Transform ceilingCheck;
    bool isTouchingCeiling;
    public bool isAttachCeiling;

    [Header("Bump")]
    public bool isBumped;
    public float bumpSpeed;

    [Header("Scaling")]
    public float jumpDownScale;
    public float sizeDownScale;
    Vector3 originalSpriteScale;
    float originalJumpForce;

    [Header("Checks")]
    public Vector2 groundSquareCheck;
    public Vector2 wallSquareCheck;
    public Vector2 ceilingSquareCheck;

    void Start()
    {
        originalSpriteScale = new Vector3(sprite.transform.localScale.x, sprite.transform.localScale.y, sprite.transform.localScale.z);
        originalJumpForce = jumpForce;
    }

    //[Header("Animation")]
    //public float hopAnimThreshhold;
    //public Animator anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!locked)
        {
            if (!isBumped)
            {
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            }

            camera.transform.position = new Vector3(Mathf.Lerp(camera.transform.position.x, transform.position.x, 0.05f),
                Mathf.Lerp(camera.transform.position.y, transform.position.y, 0.05f),
                camera.transform.position.z);
        }
    }

    void Update()
    {
        if (!locked) 
        { 
            hInput = Input.GetAxis("Horizontal");

            isGrounded = Physics2D.OverlapBox(groundCheck.position, groundSquareCheck * transform.localScale.y, 0, groundLayers) != null;
            isTouchingWall = Physics2D.OverlapBox(fontWallCheck.position, wallSquareCheck * transform.localScale.y, 0, groundLayers) != null;
            isTouchingCeiling = Physics2D.OverlapBox(ceilingCheck.position, ceilingSquareCheck * transform.localScale.y, 0, groundLayers) != null;

            isAttachCeiling = isTouchingCeiling && !isGrounded && Input.GetAxis("Jump") == 0;

            if (isAttachCeiling)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.gravityScale = 0.45f;
            } else
            {
                rb.gravityScale = 2;
            }

            isWallSliding = isTouchingWall && !isGrounded && !isAttachCeiling;

            if (isWallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            if (Input.GetAxis("Jump") != 0 & (isGrounded || isWallSliding))
            {
                Jump();
            }

            if (isAttachCeiling)
            {
                sprite.transform.rotation = Quaternion.Euler(180, 0, 0);
            } else if (isWallSliding)
            {
                sprite.transform.rotation = Quaternion.Euler(0, 0, -90 * Mathf.Sign(transform.localScale.x));
            } else
            {
                sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            isFalling = !isGrounded && !isWallSliding && !isAttachCeiling && rb.velocity.y < 0.3f;
            transform.localScale = 
                new Vector3(hInput > 0 ? -Mathf.Abs(transform.localScale.x) : hInput < 0 ? Mathf.Abs(transform.localScale.x) : transform.localScale.x, 
                transform.localScale.y, 
                transform.localScale.z);
        }
    }

    void Jump ()
    {
        if (!justJumped)
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                justJumped = true;
                pa.TriggerJump();
            } else
            {
                rb.velocity = Vector2.up * (jumpForce / 1.5f);
            }
            StartCoroutine(resetJumpAnim());
        }
    }

    IEnumerator resetJumpAnim()
    {
        yield return new WaitForSeconds(0.15f);
        justJumped = false;
        pa.ResetTriggerJump();
    }

    public void SetTransforms(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        transform.position = pos;
        sprite.transform.rotation = rot;
        transform.localScale = scale;
    }

    public void LockMovement()
    {
        locked = true;
        hInput = 0;
        rb.gravityScale = 0;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    public void UnlockMovement()
    {
        locked = false;
        rb.gravityScale = 2;
        rb.isKinematic = false;
    }

    public void Bump (Vector2 dir)
    {
        rb.velocity = dir * bumpSpeed;
        isBumped = true;
        pa.TriggerBump();
        StartCoroutine(Unbump());
    }

    IEnumerator Unbump()
    {
        yield return new WaitForSeconds(1);
        isBumped = false;
        pa.ResetTriggerBump();
    }

    public void UpdateStatsBasedOnHPAndImprints()
    {
        int multiplyer = 4 - pi.GetCurrPower();
        float scaleDown = 1 - (sizeDownScale * multiplyer);
        float jumpDown = jumpDownScale * multiplyer;

        transform.localScale = originalSpriteScale * scaleDown;
        jumpForce = originalJumpForce - jumpDown;
    }
}

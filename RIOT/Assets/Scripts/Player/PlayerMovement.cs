using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Camera camera;
    public PlayerImprints pi;

    [Header("Movement")]
    public bool locked;
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

    [Header("Bump")]
    public bool isBumped;
    public float bumpSpeed;

    [Header("Scaling")]
    public float jumpDownScale;
    public float sizeDownScale;
    Vector3 originalSpriteScale;
    float originalJumpForce;

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
                rb.gravityScale = 0.45f;
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
        StartCoroutine(Unbump());
    }

    IEnumerator Unbump()
    {
        yield return new WaitForSeconds(1);
        isBumped = false;
    }

    public void UpdateStatsBasedOnHPAndImprints()
    {
        int multiplyer = 4 -pi.GetCurrPower();
        float scaleDown = 1 - (sizeDownScale * multiplyer);
        float jumpDown = jumpDownScale * multiplyer;

        sprite.transform.localScale = originalSpriteScale * scaleDown;
        jumpForce = originalJumpForce - jumpDown;
    }
}

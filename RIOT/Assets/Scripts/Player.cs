using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float speed;
    public Vector3 movementVec;

    [Header("Jump")]
    public float jumpForce;
    public FloorCheck floorCheck;

    [Header("Animation")]
    public float hopAnimThreshhold;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        // Movement
        movementVec = Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (Input.GetAxis("Jump") > 0 & floorCheck.isOnFloor)
        {
            Jump();
        }

        // Animations
        transform.localScale = new Vector3(movementVec.x > 0 ? -1 : movementVec.x < 0 ? 1 : transform.localScale.x, 1, 1);
        anim.SetBool("isMoving", Mathf.Abs(movementVec.x) > hopAnimThreshhold);
    }

    void FixedUpdate()
    {
        // Movement
        transform.position += movementVec;
    }

    void Jump ()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        anim.SetTrigger("jump");
    }
}

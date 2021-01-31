using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float dir;
    public Rigidbody2D rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

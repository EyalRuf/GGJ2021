using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    public bool isOnFloor;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
            isOnFloor = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
            isOnFloor = false;
    }
}

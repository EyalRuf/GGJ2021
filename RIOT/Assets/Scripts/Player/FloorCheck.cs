using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    public bool isOnFloor;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            isOnFloor = true;
            Debug.Log("enter");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            isOnFloor = false;
            Debug.Log("exit");
        }
    }
}

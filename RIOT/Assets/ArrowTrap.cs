using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowSpeed;

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab).GetComponent<Arrow>();
        arrow.transform.position = new Vector3(transform.position.x + (1 * -Mathf.Sign(transform.localScale.x)), transform.position.y, transform.position.z);
        arrow.speed = arrowSpeed;
        arrow.transform.localScale = new Vector3(arrow.transform.localScale.x * Mathf.Sign(transform.localScale.x), arrow.transform.localScale.y, arrow.transform.localScale.z);
        arrow.dir = -1 * Mathf.Sign(transform.localScale.x);
    }
}

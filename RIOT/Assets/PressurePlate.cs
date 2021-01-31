using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public ArrowTrap trap;
    public Animator animator;
    public float shootCD;
    public bool canShoot = true;

    IEnumerator ShootCD()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
        animator.ResetTrigger("Pressed");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PressPlate();
        }
    }

    void PressPlate ()
    {
        if (canShoot)
        {
            animator.SetTrigger("Pressed");
            trap.Shoot();
            StartCoroutine(ShootCD());
        }
    }
}

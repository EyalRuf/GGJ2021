using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public PlayerImprints pi;
    public PlayerMovement pm;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (pi.HP == 0)
            {
                SceneManager.LoadScene("Lose", LoadSceneMode.Single);
            } else
            {
                // bump?
                if (pi.DamageSequence())
                {
                    Vector2 playerPos = pm.transform.position;
                    Vector2 colliderPos = collision.transform.position;

                    pm.Bump(playerPos - colliderPos);
                }
            }
        }
        else if (collision.tag == "Slime")
        {
            if (pi.HP < 4)
            {
                pi.Heal();
                Destroy(collision.gameObject);
            }
        }
        else if (collision.tag == "End")
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }
    }

    public void EndDamageSequence()
    {
        pm.UnlockMovement();
        pm.UpdateStatsBasedOnHPAndImprints();
    }
}

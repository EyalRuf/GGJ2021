using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerImprints : MonoBehaviour
{
    public PlayerMovement pm; 

    [Header("Imprints")]
    public int maxImprints;
    public GameObject imprintPrefab;
    public float spawnImprintCD;
    public bool canImprint;

    public GameObject imprint1 = null;
    public GameObject imprint2 = null;
    public GameObject imprint3 = null;
    public GameObject imprint4 = null;
    
    public bool pressedReturnToImprint;
    
    [Header("UI")]
    public Image imprint1UI;
    public Image imprint2UI;
    public Image imprint3UI;
    public Image imprint4UI;

    public Color imprintOnColor;
    public Color imprintOffColor;
    public Color imprintTeleportColor;

    // Update is called once per frame
    void Update()
    {
        if (!pressedReturnToImprint)
        {
            CreateOrDestroyImprints();
        } else
        {
            // if press an active imprint => return to it
            if (imprint1 != null && Input.GetKeyDown(KeyCode.Alpha1))
            {
                TeleportToImprintAndDestroyImprints(imprint1.GetComponent<Imprint>());
            }
            else if (imprint2 != null && Input.GetKeyDown(KeyCode.Alpha2))
            {
                TeleportToImprintAndDestroyImprints(imprint2.GetComponent<Imprint>());
            }
            else if (imprint3 != null && Input.GetKeyDown(KeyCode.Alpha3))
            {
                TeleportToImprintAndDestroyImprints(imprint3.GetComponent<Imprint>());
            }
            else if (imprint3 != null && Input.GetKeyDown(KeyCode.Alpha4))
            {
                TeleportToImprintAndDestroyImprints(imprint4.GetComponent<Imprint>());
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            ToggleReturningToImprint();
        }
    }

    void CreateOrDestroyImprints()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (imprint1 == null && canImprint)
            {
                imprint1UI.color = imprintOnColor;
                SpawnImprint(out imprint1);
            }
            else if (imprint1 != null)
            {
                Destroy(imprint1);
                imprint1UI.color = imprintOffColor;
                imprint1 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (imprint2 == null && canImprint)
            {
                imprint2UI.color = imprintOnColor;
                SpawnImprint(out imprint2);
            }
            else if (imprint2 != null)
            {
                Destroy(imprint2);
                imprint2UI.color = imprintOffColor;
                imprint2 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (imprint3 == null && canImprint)
            {
                imprint3UI.color = imprintOnColor;
                SpawnImprint(out imprint3);
            }
            else if (imprint3 != null)
            {
                Destroy(imprint3);
                imprint3UI.color = imprintOffColor;
                imprint3 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (imprint4 == null && canImprint)
            {
                imprint4UI.color = imprintOnColor;
                SpawnImprint(out imprint4);
            }
            else if (imprint4 != null)
            {
                Destroy(imprint4);
                imprint4UI.color = imprintOffColor;
                imprint4 = null;
            }
        }
    }

    void SpawnImprint(out GameObject imprint)
    {
        imprint = Instantiate(imprintPrefab);
        imprint.GetComponent<Imprint>().SetTransforms(pm.transform.position, pm.sprite.transform.rotation, pm.transform.localScale);

        canImprint = false;
        StartCoroutine(ImprintCD());
    }

    IEnumerator ImprintCD()
    {
        yield return new WaitForSeconds(spawnImprintCD);
        canImprint = true;
    }

    void ToggleReturningToImprint ()
    {
        if (pressedReturnToImprint)
        {
            //unhighlight
            if (imprint1 != null)
            {
                imprint1UI.color = imprintOnColor;
            }
            if (imprint2 != null)
            {
                imprint2UI.color = imprintOnColor;
            }
            if (imprint3 != null)
            {
                imprint3UI.color = imprintOnColor;
            }
            if (imprint4 != null)
            {
                imprint4UI.color = imprintOnColor;
            }
        }
        else
        {
            //highlight
            if (imprint1 != null)
            {
                imprint1UI.color = imprintTeleportColor;
            }
            if (imprint2 != null)
            {
                imprint2UI.color = imprintTeleportColor;
            }
            if (imprint3 != null)
            {
                imprint3UI.color = imprintTeleportColor;
            }
            if (imprint4 != null)
            {
                imprint4UI.color = imprintTeleportColor;
            }
        }

        pressedReturnToImprint = !pressedReturnToImprint;
    }

    void TeleportToImprintAndDestroyImprints(Imprint imprint)
    {
        pm.SetTransforms(imprint.transform.position, imprint.sprite.transform.rotation, imprint.transform.localScale);

        pressedReturnToImprint = false;

        Destroy(imprint1);
        imprint1UI.color = imprintOffColor;
        imprint1 = null;

        Destroy(imprint2);
        imprint2UI.color = imprintOffColor;
        imprint2 = null;

        Destroy(imprint3);
        imprint3UI.color = imprintOffColor;
        imprint3 = null;

        Destroy(imprint4);
        imprint4UI.color = imprintOffColor;
        imprint4 = null;
    }
}

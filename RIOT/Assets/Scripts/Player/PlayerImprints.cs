using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerImprints : MonoBehaviour
{
    public int maxImprints;
    public GameObject imprintPrefab;
    public float spawnImprintCD;
    public bool canImprint;

    public GameObject imprint1 = null;
    public GameObject imprint2 = null;
    public GameObject imprint3 = null;
    public GameObject imprint4 = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (imprint1 == null && canImprint)
            {
                SpawnImprint(out imprint1);
            } else if (imprint1 != null)
            {
                Destroy(imprint1);
                imprint1 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (imprint2 == null && canImprint)
            {
                SpawnImprint(out imprint2);
            }
            else if (imprint2 != null)
            {
                Destroy(imprint2);
                imprint2 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (imprint3 == null && canImprint)
            {
                SpawnImprint(out imprint3);
            }
            else if (imprint3 != null)
            {
                Destroy(imprint3);
                imprint3 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (imprint4 == null && canImprint)
            {
                SpawnImprint(out imprint4);
            }
            else if (imprint4 != null)
            {
                Destroy(imprint4);
                imprint4 = null;
            }
        }
    }

    void SpawnImprint(out GameObject imprint)
    {
        imprint = Instantiate(imprintPrefab);
        imprint.transform.position = this.transform.position;
        imprint.transform.rotation = this.transform.rotation;
        imprint.transform.localScale = this.transform.localScale;

        canImprint = false;
        StartCoroutine(ImprintCD());
    }

    IEnumerator ImprintCD()
    {
        yield return new WaitForSeconds(spawnImprintCD);
        canImprint = true;
    }
}

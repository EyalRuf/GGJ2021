using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerImprints : MonoBehaviour
{
    public PlayerMovement pm;
    public PlayerHealth ph;
    public bool isChoosingRespawnPoint;
    public int HP = 4;

    [Header("Imprints")]
    public GameObject imprint1Prefab;
    public GameObject imprint2Prefab;
    public GameObject imprint3Prefab;
    public GameObject imprint4Prefab;
    public float spawnImprintCD;
    public bool canImprint;

    public GameObject imprint1 = null;
    public GameObject imprint2 = null;
    public GameObject imprint3 = null;
    public GameObject imprint4 = null;
    
    public bool pressedReturnToImprint;

    public bool isImp1Dead;
    public bool isImp2Dead;
    public bool isImp3Dead;
    public bool isImp4Dead;

    [Header("UI")]
    public Image imprint1UIImage;
    public Image imprint2UIImage;
    public Image imprint3UIImage;
    public Image imprint4UIImage;

    public GameObject imprint1UI_GO;
    public GameObject imprint2UI_GO;
    public GameObject imprint3UI_GO;
    public GameObject imprint4UI_GO;

    public Color imprintOnColor;
    public Color imprintOffColor;
    public Color imprintTeleportColor;

    public GameObject go_pb1;
    public ProgressBar pb1;
    public float heldTime1;
    public bool spawnKeyUp1;

    public GameObject go_pb2;
    public ProgressBar pb2;
    public float heldTime2;
    public bool spawnKeyUp2;

    public GameObject go_pb3;
    public ProgressBar pb3;
    public float heldTime3;
    public bool spawnKeyUp3;

    public GameObject go_pb4;
    public ProgressBar pb4;
    public float heldTime4;
    public bool spawnKeyUp4;

    // Update is called once per frame
    void Update()
    {
        if (isChoosingRespawnPoint)
        {
            if (imprint1 != null && Input.GetKeyDown(KeyCode.Alpha1))
            {
                TeleportToImprint(imprint1.GetComponent<Imprint>());
                EndDamageSequence();
            }
            else if (imprint2 != null && Input.GetKeyDown(KeyCode.Alpha2))
            {
                TeleportToImprint(imprint2.GetComponent<Imprint>());
                EndDamageSequence();
            }
            else if (imprint3 != null && Input.GetKeyDown(KeyCode.Alpha3))
            {
                TeleportToImprint(imprint3.GetComponent<Imprint>());
                EndDamageSequence();
            }
            else if (imprint4 != null && Input.GetKeyDown(KeyCode.Alpha4))
            {
                TeleportToImprint(imprint4.GetComponent<Imprint>());
                EndDamageSequence();
            }
        } else
        {
            UpdatePressedReturnToImprint();

            if (!pressedReturnToImprint)
            {
                CreateOrDestroyImprints();
            } else
            {
                // if press an active imprint => return to it
                if (imprint1 != null && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    TeleportToImprint(imprint1.GetComponent<Imprint>());
                }
                else if (imprint2 != null && Input.GetKeyDown(KeyCode.Alpha2))
                {
                    TeleportToImprint(imprint2.GetComponent<Imprint>());
                }
                else if (imprint3 != null && Input.GetKeyDown(KeyCode.Alpha3))
                {
                    TeleportToImprint(imprint3.GetComponent<Imprint>());
                }
                else if (imprint4 != null && Input.GetKeyDown(KeyCode.Alpha4))
                {
                    TeleportToImprint(imprint4.GetComponent<Imprint>());
                }
            }
        }
    }

    void CreateOrDestroyImprints()
    {
        if (!isImp1Dead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (imprint1 == null && canImprint)
                {
                    imprint1UIImage.color = imprintOnColor;
                    SpawnImprint(out imprint1, imprint1Prefab);
                    spawnKeyUp1 = false;
                }
            } 
            else if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                spawnKeyUp1 = true;
            }
            else if (Input.GetKey(KeyCode.Alpha1) && imprint1 != null && spawnKeyUp1)
            {
                heldTime1 += Time.deltaTime;

                if (heldTime1 > 0.15f)
                {
                    go_pb1.SetActive(true);
                    pb1.curr = heldTime1 * 100;

                    if (pb1.curr > pb1.max)
                    {
                        Destroy(imprint1);
                        imprint1UIImage.color = imprintOffColor;
                        imprint1 = null;
                        pm.UpdateStatsBasedOnHPAndImprints();

                        heldTime1 = 0;
                        pb1.curr = 0;
                        go_pb1.SetActive(false);
                    }
                }
            } 
            else
            {
                heldTime1 = 0;
                pb1.curr = 0;
                go_pb1.SetActive(false);
            }
        }

        if (!isImp2Dead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (imprint2 == null && canImprint)
                {
                    imprint2UIImage.color = imprintOnColor;
                    SpawnImprint(out imprint2, imprint2Prefab);
                    spawnKeyUp2 = false;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                spawnKeyUp2 = true;
            }
            else if (Input.GetKey(KeyCode.Alpha2) && imprint2 != null && spawnKeyUp2)
            {
                heldTime2 += Time.deltaTime;

                if (heldTime2 > 0.15f)
                {
                    go_pb2.SetActive(true);
                    pb2.curr = heldTime2 * 100;

                    if (pb2.curr > pb2.max)
                    {
                        Destroy(imprint2);
                        imprint2UIImage.color = imprintOffColor;
                        imprint2 = null;
                        pm.UpdateStatsBasedOnHPAndImprints();

                        heldTime2 = 0;
                        pb2.curr = 0;
                        go_pb2.SetActive(false);
                    }
                }
            }
            else
            {
                heldTime2 = 0;
                pb2.curr = 0;
                go_pb2.SetActive(false);
            }
        }

        if (!isImp3Dead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (imprint3 == null && canImprint)
                {
                    imprint3UIImage.color = imprintOnColor;
                    SpawnImprint(out imprint3, imprint3Prefab);
                    spawnKeyUp3 = false;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                spawnKeyUp3 = true;
            }
            else if (Input.GetKey(KeyCode.Alpha3) && imprint3 != null && spawnKeyUp3)
            {
                heldTime3 += Time.deltaTime;

                if (heldTime3 > 0.15f)
                {
                    go_pb3.SetActive(true);
                    pb3.curr = heldTime3 * 100;

                    if (pb3.curr > pb3.max)
                    {
                        Destroy(imprint3);
                        imprint3UIImage.color = imprintOffColor;
                        imprint3 = null;
                        pm.UpdateStatsBasedOnHPAndImprints();

                        heldTime3 = 0;
                        pb3.curr = 0;
                        go_pb3.SetActive(false);
                    }
                }
            }
            else
            {
                heldTime3 = 0;
                pb3.curr = 0;
                go_pb3.SetActive(false);
            }
        }

        if (!isImp4Dead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (imprint4 == null && canImprint)
                {
                    imprint4UIImage.color = imprintOnColor;
                    SpawnImprint(out imprint4, imprint4Prefab);
                    spawnKeyUp4 = false;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                spawnKeyUp4 = true;
            }
            else if (Input.GetKey(KeyCode.Alpha4) && imprint4 != null && spawnKeyUp4)
            {
                heldTime4 += Time.deltaTime;

                if (heldTime4 > 0.15f)
                {
                    go_pb4.SetActive(true);
                    pb4.curr = heldTime4 * 100;

                    if (pb4.curr > pb4.max)
                    {
                        Destroy(imprint4);
                        imprint4UIImage.color = imprintOffColor;
                        imprint4 = null;
                        pm.UpdateStatsBasedOnHPAndImprints();

                        heldTime4 = 0;
                        pb4.curr = 0;
                        go_pb4.SetActive(false);
                    }
                }
            }
            else
            {
                heldTime4 = 0;
                pb4.curr = 0;
                go_pb4.SetActive(false);
            }
        }
    }

    void SpawnImprint(out GameObject imprint, GameObject prefab)
    {
        imprint = Instantiate(prefab);
        imprint.GetComponent<Imprint>().SetTransforms(pm.transform.position, pm.sprite.transform.rotation, pm.transform.localScale);

        pm.UpdateStatsBasedOnHPAndImprints();
        canImprint = false;
        StartCoroutine(ImprintCD());
    }

    IEnumerator ImprintCD()
    {
        yield return new WaitForSeconds(spawnImprintCD);
        canImprint = true;
    }

    void UpdatePressedReturnToImprint ()
    {
        pressedReturnToImprint = Input.GetKey(KeyCode.Space);
        if (pressedReturnToImprint)
        {
            //highlight
            changeAliveImprintsColor(imprintTeleportColor);
        }
        else
        {
            //unhighlight
            changeAliveImprintsColor(imprintOnColor);
        }
    }

    void changeAliveImprintsColor (Color c)
    {
        if (imprint1 != null)
        {
            imprint1UIImage.color = c;
        }
        if (imprint2 != null)
        {
            imprint2UIImage.color = c;
        }
        if (imprint3 != null)
        {
            imprint3UIImage.color = c;
        }
        if (imprint4 != null)
        {
            imprint4UIImage.color = c;
        }
    }

    void TeleportToImprint(Imprint imprint)
    {
        pm.SetTransforms(imprint.transform.position, imprint.sprite.transform.rotation, imprint.transform.localScale);

        if (imprint1 == imprint.gameObject)
        {
            Destroy(imprint1);
            imprint1UIImage.color = imprintOffColor;
            imprint1 = null;
            pm.UpdateStatsBasedOnHPAndImprints();
        } else if (imprint2 == imprint.gameObject)
        {
            Destroy(imprint2);
            imprint2UIImage.color = imprintOffColor;
            imprint2 = null;
            pm.UpdateStatsBasedOnHPAndImprints();
        } else if (imprint3 == imprint.gameObject)
        {
            Destroy(imprint3);
            imprint3UIImage.color = imprintOffColor;
            imprint3 = null;
            pm.UpdateStatsBasedOnHPAndImprints();
        } else if (imprint4 == imprint.gameObject)
        {
            Destroy(imprint4);
            imprint4UIImage.color = imprintOffColor;
            imprint4 = null;
            pm.UpdateStatsBasedOnHPAndImprints();
        }
    }

    public bool DamageSequence()
    {
        if (HP == GetImprintsUsed())
        {
            isChoosingRespawnPoint = true;
            pm.LockMovement();
            changeAliveImprintsColor(imprintTeleportColor);
            return false;
        } else
        {
            EndDamageSequence();
            return true;
        }
    }

    public void EndDamageSequence()
    {
        isChoosingRespawnPoint = false;
        HP--;

        int destroyImpNum = GetHighestUnusedImprint();

        if (destroyImpNum == 4)
        {
            Destroy(imprint4);
            imprint4UIImage.color = imprintOffColor;
            imprint4 = null;
            isImp4Dead = true;
            imprint4UI_GO.SetActive(false);
        } else if (destroyImpNum == 3)
        {
            Destroy(imprint3);
            imprint3UIImage.color = imprintOffColor;
            imprint3 = null;
            isImp3Dead = true;
            imprint3UI_GO.SetActive(false);
        } else if (destroyImpNum == 2)
        {
            Destroy(imprint2);
            imprint2UIImage.color = imprintOffColor;
            imprint2 = null;
            isImp2Dead = true;
            imprint2UI_GO.SetActive(false);
        } else if (destroyImpNum == 1)
        {
            Destroy(imprint1);
            imprint1UIImage.color = imprintOffColor;
            imprint1 = null;
            isImp1Dead = true;
            imprint1UI_GO.SetActive(false);
        }

        ph.EndDamageSequence();
    }

    public int GetImprintsUsed()
    {
        int imprintsUsed = 0;
        if (imprint1 != null)
            imprintsUsed++;
        if (imprint2 != null)
            imprintsUsed++;
        if (imprint3 != null)
            imprintsUsed++;
        if (imprint4 != null)
            imprintsUsed++;

        return imprintsUsed;
    }

    public int GetHighestUnusedImprint ()
    {
        if (!isImp4Dead && imprint4 == null)
            return 4;
        if (!isImp3Dead && imprint3 == null)
            return 3;
        if (!isImp2Dead && imprint2 == null)
            return 2;
        return 1;
    }

    public int GetCurrPower ()
    {
        return HP - GetImprintsUsed();
    }
}

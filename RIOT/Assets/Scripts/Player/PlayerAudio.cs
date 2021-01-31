using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public PlayerMovement pm;
    public PlayerImprints pi;
    public AudioSource walk;
    public AudioSource jump;
    public AudioSource poop;
    public AudioSource tp;
    public AudioSource hit;

    // Update is called once per frame
    void Update()
    {
        walk.enabled = pm.isWalking && !pi.isChoosingRespawnPoint;
    }

    public void JumpSound()
    {
        jump.Play();
    }

    public void PoopSound()
    {
        poop.Play();
    }

    public void TPSound()
    {
        tp.Play();
    }

    public void HitSound()
    {
        hit.Play();
    }
}

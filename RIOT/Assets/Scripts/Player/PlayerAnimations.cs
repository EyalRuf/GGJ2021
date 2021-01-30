using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour
{
    private string PARAM_NAME_isMoving = "isMoving";
    private string PARAM_NAME_jump = "jump";
    private string PARAM_NAME_bump = "bump";
    private string PARAM_NAME_bumpT = "bumpT";
    private string PARAM_NAME_grounded = "grounded";
    private string PARAM_NAME_falling = "falling";

    public PlayerMovement pm;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(PARAM_NAME_isMoving, pm.hInput != 0);
        anim.SetBool(PARAM_NAME_grounded, !pm.justJumped && (pm.isGrounded || pm.isWallSliding || pm.isAttachCeiling));
        anim.SetBool(PARAM_NAME_bump, pm.isBumped);
        anim.SetBool(PARAM_NAME_falling, pm.isFalling);
    }

    public void TriggerJump()
    {
        anim.SetTrigger(PARAM_NAME_jump);
    }

    public void TriggerBump()
    {
        anim.SetTrigger(PARAM_NAME_bumpT);
    }

    public void ResetTriggerJump()
    {
        anim.ResetTrigger(PARAM_NAME_bumpT);
    }

    public void ResetTriggerBump()
    {
        anim.ResetTrigger(PARAM_NAME_bumpT);
    }
}

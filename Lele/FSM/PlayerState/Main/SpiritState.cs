using System.Collections;
using UnityEngine;

public class SpiritState : PlayerState
{
    public SpiritState(PlayerController pc) : base(pc)
    {
    }
    public override void Enter()
    {
        pc.IsSpiritState = true;
        pc.RB.linearVelocity = Vector2.zero; // Reset velocity to prevent unwanted movement
        pc.ANIMATOR.CrossFade(AnimStates.TurningToSpritState, 0.1f);
        pc.RB.gravityScale = pc.PATTRIBUTES.NegativeGravityScale;
        Debug.Log("Entering SpiritState");
        pc.StartCoroutine(WaitAndPlay());
    }
    public override void LogicUpdate()
    {
        // Logic for SpiritState can be added here if needed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            pc.ChangeState(pc.IdleState, null); // Example transition to IdleState
            Debug.LogError("currently use 1 to restart by simply changing state. it is for testing only !!! future need to reload scene and load to most recent saved data");
        }
    }
    public override void Exit()
    {
        pc.IsSpiritState = false;
        pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale; // Reset gravity scale to default
        Debug.Log("Exiting SpiritState");
    }
    public override IEnumerator WaitAndPlay()
    { 
        var theClip = GetAnimationClipByName(AnimStates.TurningToSprit);
        
        if (theClip != null)
        {
            
            float waitTime = theClip.length;
            float elapsedTime = 0f;
            while (elapsedTime < waitTime)
            {
                if (pc.IsSpiritState == false)
                {
                    yield break; // Exit if the state changes
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("Animation clip for TurningToSprit not found.");
        }
        pc.ANIMATOR.CrossFade(AnimStates.Spirit, 0.1f);
    }
}

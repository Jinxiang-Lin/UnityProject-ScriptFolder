using UnityEngine;
using System.Collections;
public class RiseToFallState : PlayerState
{
    public RiseToFallState(PlayerController pc) : base(pc)
    {
    }
    public override void Enter()
    {
        pc.RB.gravityScale = pc.PATTRIBUTES.FallGravityScale * 0.7f;
        pc.StartCoroutine(WaitAndPlay());
    }
    public override void Exit()
    {
        
        Debug.Log("Exiting RiseToFallState");
    }
    public override void PhysicsUpdate()
    {
        pc.CurrentMovement.HorizontalMovement();

    }
    
    public override IEnumerator WaitAndPlay()
    {
        if (!pc.IsWilting)
        {
            var theClip = GetAnimationClipByName(AnimStates.RisingToFalling);
            pc.ANIMATOR.CrossFade(AnimStates.RisingToFallingState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedRisingToFalling);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedRisingToFallingState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
    }

    private IEnumerator WaitForAnimationOrStateChange(AnimationClip theClip)
    {
        float waitTime = theClip != null ? theClip.length : 0f;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            if (pc.IsOnWall && pc.GrabOnWallTriggered)
            {
                pc.ChangeState(pc.FallToGrabState, null);
                yield break;
            }
            if (pc.RopeShootingTriggered)
            {
                pc.ChangeState(pc.RopeShootingState, null);
                Debug.Log("Rope shooting triggered during RiseToFallState!!!!!");
                yield break;
                
            }
            if (pc.IsSpiritState)
            {
                //pc.ChangeState(pc.SpiritState, null);
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pc.ChangeState(pc.FallState, pc.FallMovement);
    }
    
}

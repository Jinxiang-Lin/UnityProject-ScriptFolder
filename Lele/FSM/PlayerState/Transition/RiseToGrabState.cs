using UnityEngine;
using System.Collections;
public class RiseToGrabState : PlayerState
{
    public RiseToGrabState(PlayerController pc) : base(pc)
    {
    }
    public override void Enter()
    {
        if (pc.PreviousState != null)
        {
            pc.StartCoroutine(WaitAndPlay());
        }
    }
    
    public override IEnumerator WaitAndPlay()
    {
        if (!pc.IsWilting)
        {
            var theClip = GetAnimationClipByName(AnimStates.RiseToGrabOnWall);
            pc.ANIMATOR.CrossFade(AnimStates.RiseToGrabOnWallState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedRiseToGrab);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedRiseToGrabState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
    }
    private IEnumerator WaitForAnimationOrStateChange(AnimationClip theClip)
    {
        float waitTime = theClip != null ? theClip.length : 0f;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            if (ShouldChangeState())
            {
                // If the state should change before the animation ends, break out of the loop
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pc.ChangeState(pc.GrabState, pc.GrabMovement);
    }
    private bool ShouldChangeState()
    {
        // Check if the player is no longer in the idle state or if the jump button is pressed
        return false; // Implement your logic here
    }
}

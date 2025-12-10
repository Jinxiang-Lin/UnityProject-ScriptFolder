using UnityEngine;
using System.Collections;
public class LandingToIdleState : PlayerState
{
    public LandingToIdleState(PlayerController pc) : base(pc)
    {
    }
    public override void Enter()
    {
        if (pc.PreviousState != null)
        {
            pc.StartCoroutine(WaitAndPlay(pc.PreviousState));
        }
    }
    /*public override IEnumerator WaitAndPlay(PlayerState prevState)
    {
        AnimationClip theClip = null;
        if (!pc.IsWilting)
        {
            theClip = GetAnimationClipByName(AnimStates.LandingToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.LandingToIdleState, 0.1f);
        }
        else
        {
            theClip = GetAnimationClipByName(AnimStates.WiltedLandingToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedLandingToIdleState, 0.1f);
        }
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
        pc.ChangeState(pc.IdleState, pc.IdleMovement);
    }*/
    public override IEnumerator WaitAndPlay(PlayerState prevState)
    {
        if (!pc.IsWilting)
        {
            var theClip = GetAnimationClipByName(AnimStates.LandingToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.LandingToIdleState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedLandingToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedLandingToIdleState, 0.1f);
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
        pc.ChangeState(pc.IdleState, pc.IdleMovement);
    }
    private bool ShouldChangeState()
    {
        // Check if the player is no longer in the idle state or if the jump button is pressed
        return false; // Implement your logic here
    }
}

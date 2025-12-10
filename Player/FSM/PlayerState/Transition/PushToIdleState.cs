using UnityEngine;
using System.Collections;
public class PushToIdleState : PlayerState
{
    public PushToIdleState(PlayerController pc) : base(pc)
    {
    }
    public override void Enter()
    {
        if (pc.PreviousState != null)
        {
            pc.StartCoroutine(WaitAndPlay(pc.PreviousState));
        }
    }
    public override IEnumerator WaitAndPlay(PlayerState prevState)
    {
        
        if (!pc.IsWilting)
        {
            var theClip = GetAnimationClipByName(AnimStates.PushToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.PushToIdleState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedPushToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedPushToIdleState, 0.1f);
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

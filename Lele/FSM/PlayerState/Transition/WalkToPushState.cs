using UnityEngine;
using System.Collections;
public class WalkToPushState : PlayerState
{
    public WalkToPushState(PlayerController pc) : base(pc)
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
            var theClip = GetAnimationClipByName(AnimStates.RunToPush);
            pc.ANIMATOR.CrossFade(AnimStates.RunToPushState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedWalkToPush);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedWalkToPushState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
    }
    private IEnumerator WaitForAnimationOrStateChange(AnimationClip theClip)
    {
        float waitTime = theClip != null ? theClip.length : 0f;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            if (Mathf.Abs(pc.HDir) < 0.1f)
            {
                // If the state should change before the animation ends, break out of the loop
                pc.ChangeState(pc.IdleState, pc.IdleMovement);
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pc.ChangeState(pc.PushState, pc.PushMovement);
    }
    private bool ShouldChangeState()
    {
        // Check if the player is no longer in the idle state or if the jump button is pressed
        return false; // Implement your logic here
    }
}

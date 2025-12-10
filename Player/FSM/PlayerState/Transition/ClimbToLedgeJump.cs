using UnityEngine;
using System.Collections;
public class ClimbToLedgeJump : PlayerState
{
    public ClimbToLedgeJump(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        Debug.Log("enter climb to ledge jump state");
        if (pc.PreviousState != null)
        {
            pc.StartCoroutine(WaitAndPlay(pc.PreviousState));
        }
    }
    
    public override void PhysicsUpdate()
    {
        pc.CurrentMovement.HVMovement();

    }
    public override IEnumerator WaitAndPlay(PlayerState prevState)
    {
        if (!pc.IsWilting)
        {
            var theClip = GetAnimationClipByName(AnimStates.ClimbToLedgeJumpFullAction); ; // shoud change
            pc.ANIMATOR.CrossFade(AnimStates.ClimbToLedgeJumpFullActionState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedClimbToJumpFullAction);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedClimbToJumpFullActionState, 0.1f);
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

                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pc.ChangeState(pc.FallState, pc.FallMovement);
    }
    private bool ShouldChangeState()
    {
        // Check if the player is no longer in the idle state or if the jump button is pressed
        return false; // Implement your logic here
    }
}

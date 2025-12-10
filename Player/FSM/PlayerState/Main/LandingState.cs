using UnityEngine;
using System.Collections;
public class LandingState : PlayerState
{
    public LandingState(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        pc.RB.linearVelocity = Vector2.zero; // Reset vertical velocity to prevent unwanted movement
        pc.StartCoroutine(WaitAndPlay());

    }
    public override void LogicUpdate()
    {
        
    }
    public override void PhysicsUpdate()
    {
        pc.CurrentMovement.VerticalMovement();
    }
    public override IEnumerator WaitAndPlay()
    {
        AnimationClip theClip = null;
        if (!pc.IsWilting)
        {
            theClip = GetAnimationClipByName(AnimStates.Landing);
            pc.ANIMATOR.CrossFade(AnimStates.LandingState, 0.1f);
        }
        else
        {
            theClip = GetAnimationClipByName(AnimStates.WiltedLanding);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedLandingState, 0.1f);
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
        if (Mathf.Abs(pc.HDir) < 0.1f)
        {
            pc.ChangeState(pc.LandingToIdleState, null);
        }
        else if (Mathf.Abs(pc.HDir) > 0.1f)
        {
            pc.ChangeState(pc.LandingToWalkState, pc.LandingToWalkMovement);
        }
       
    }
    private bool ShouldChangeState()
    {
        // Check if the player is no longer in the idle state or if the jump button is pressed
        return false; // Implement your logic here
    }
}

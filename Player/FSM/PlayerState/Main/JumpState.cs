using UnityEngine;
using System.Collections;
public class JumpState : PlayerState
{
    public JumpState(PlayerController pc) : base(pc) { }
    bool isRising = false;
    public override void Enter()
    {
        pc.RB.gravityScale = pc.PATTRIBUTES.JumpGravityScale; // Increase gravity for a more realistic jump
        pc.JumpTriggered = false;
        pc.CurrentMovement.VerticalMovement();
        isRising = false;
        pc.StartCoroutine(WaitAndPlay(pc.PreviousState));
        
        Debug.Log("Entering JumpState");
    }
    public override void Exit()
    {
        pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale; // Reset gravity scale to default
        isRising = false;
        Debug.Log("Exiting JumpState");       
    }
    public override void LogicUpdate()
    {
        if (isRising)
        {
            if (pc.IsCeilinged || pc.RB.linearVelocityY <= 0f)
            {
                pc.ChangeState(pc.RiseToFallState, pc.RiseToFallMovement);

            }
            else if (pc.GrabOnWallTriggered && pc.IsOnWall)
            {
                pc.ChangeState(pc.RiseToGrabState, null);

            }
        }
     
    }
    public override void PhysicsUpdate()
    {
       pc.CurrentMovement.HorizontalMovement();
    }
    public override IEnumerator WaitAndPlay(PlayerState prev)
    {
        switch (prev)
        {
            case IdleState _:
                if (!pc.IsWilting)
                {
                    var theClip = GetAnimationClipByName(AnimStates.IdleToRising);
                    pc.ANIMATOR.CrossFade(AnimStates.IdleToJumpState, 0.1f);
                    yield return WaitForAnimationOrStateChange(theClip);
                }
                else
                {
                    var theClip = GetAnimationClipByName(AnimStates.WiltedIdleToRising);
                    pc.ANIMATOR.CrossFade(AnimStates.WiltedIdleToJumpState, 0.1f);
                    yield return WaitForAnimationOrStateChange(theClip);
                }
                break;
            case RunToIdleState _:
                if (!pc.IsWilting)
                {
                    var theClip = GetAnimationClipByName(AnimStates.IdleToRising);
                    pc.ANIMATOR.CrossFade(AnimStates.IdleToJumpState, 0.1f);
                    yield return WaitForAnimationOrStateChange(theClip);
                }
                else
                {
                    var theClip = GetAnimationClipByName(AnimStates.WiltedIdleToRising);
                    pc.ANIMATOR.CrossFade(AnimStates.WiltedIdleToJumpState, 0.1f);
                    yield return WaitForAnimationOrStateChange(theClip);
                }
                break;
            case RunState _:
                if (!pc.IsWilting)
                {
                    var theClip = GetAnimationClipByName(AnimStates.RunToJump);
                    pc.ANIMATOR.CrossFade(AnimStates.RunToJumpState, 0.1f);
                    yield return WaitForAnimationOrStateChange(theClip);
                }
                else
                {
                    var theClip = GetAnimationClipByName(AnimStates.WiltedWalkToJump);
                    pc.ANIMATOR.CrossFade(AnimStates.WiltedWalkToJumpState, 0.1f);
                    yield return WaitForAnimationOrStateChange(theClip);
                }
                break;
            default:

                Debug.LogWarning($"Unknown previous state for JumpState {pc.PreviousState}");
                yield break;
        }
        
    }
    private IEnumerator WaitForAnimationOrStateChange(AnimationClip theClip)
    {
        float waitTime = theClip != null ? theClip.length : 0f;
        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // After the animation finishes, transition to the rising state
        isRising = true;
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Rising, 0.1f);
        }
        else
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedRising, 0.1f);
        }
    }
    
}

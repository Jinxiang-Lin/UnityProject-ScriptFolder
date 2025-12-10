using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController pc) : base(pc) { }
    
    public override void Enter()
    {
        pc.RB.linearVelocity = Vector2.zero; // Reset horizontal velocity
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.Play(AnimStates.Idle);
        }
        else
        {
            pc.ANIMATOR.Play(AnimStates.IdleWilting);
        }
    }
    public override void LogicUpdate()
    {
        if (pc.JumpTriggered)
        {
            pc.ChangeState(pc.JumpState, pc.JumpMovement);
        }
        else if (!pc.IsGrounded)
        {
            pc.ChangeState(pc.FallState, pc.FallMovement);
        }
        else if (pc.IsGrounded && Mathf.Abs(pc.HDir) > 0.1f)
        {
            pc.ChangeState(pc.IdleToRunState, pc.IdleToRunMovement);
        }
        if (pc.RopeShootingTriggered)
        {
            pc.ChangeState(pc.RopeShootingState, null);
        }
    }
    
    
}

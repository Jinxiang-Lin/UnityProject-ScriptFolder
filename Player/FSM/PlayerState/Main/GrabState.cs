using UnityEngine;

public class GrabState : PlayerState
{
    public GrabState(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        if (pc.IsOnWall)
        {
            pc.RB.gravityScale = pc.PATTRIBUTES.ZeroGravityScale;
        }
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.Play(AnimStates.GrabOnWall);
        }
        else
        {
            pc.ANIMATOR.Play(AnimStates.WiltedGrabOnWall);
        }
    }
    public override void Exit()
    {
        pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale;
    }
    public override void LogicUpdate()
    {
        if (pc.IsOnWall && !pc.IsGrounded)
        {
            if (pc.GrabOnWallTriggered && Mathf.Abs(pc.VDir) > 0)
            {
                pc.ChangeState(pc.WallClimbingState, pc.WallClimbingMovement);
            }
            else if (!pc.GrabOnWallTriggered)
            {
                pc.ChangeState(pc.FallState, pc.FallMovement);
            }
        }
        else if (pc.IsOnWall && pc.IsGrounded)
        {
            if (pc.GrabOnWallTriggered && Mathf.Abs(pc.VDir) > 0)
            {
                pc.ChangeState(pc.WallClimbingState, pc.WallClimbingMovement);
            }
            else if (!pc.GrabOnWallTriggered)
            {
                pc.ChangeState(pc.IdleState, pc.IdleMovement);
            }
        }
        else if (!pc.IsOnWall && !pc.IsGrounded)
        { 
            pc.ChangeState(pc.FallState, pc.FallMovement);
        }
    }
    public override void PhysicsUpdate()
    {
        pc.CurrentMovement.HVMovement();
        //pc.CurrentMovement.HorizontalAndVerticalMovement();
    }
    
}

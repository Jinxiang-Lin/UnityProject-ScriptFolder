using JetBrains.Annotations;
using UnityEngine;

public class WallClimbingState : PlayerState
{
    public WallClimbingState(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        pc.RB.gravityScale = pc.PATTRIBUTES.ZeroGravityScale;
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.CrossFade(AnimStates.wallClimbing, 0.1f);
        }
        else
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedClimb, 0.1f);
        }
    }
    public override void Exit()
    {
   
        pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale;
        pc.RB.linearVelocityY = 0f; // Reset vertical velocity to prevent unwanted movement
    }
    public override void LogicUpdate()
    {
        pc.LedgeDetector.PerformDetection();
        if (pc.LedgeDetector.IsLedgeDetected)
        {
            pc.ChangeState(pc.ClimbToLedgeJumpState, pc.ClimbToLedgeJumpMovement);
        }
        else if (pc.GrabOnWallTriggered && Mathf.Approximately(pc.VDir, 0))
        {
            pc.ChangeState(pc.GrabState, pc.GrabMovement);
        }
        else if(!pc.GrabOnWallTriggered || !pc.IsOnWall)
        {
            pc.ChangeState(pc.FallState, pc.FallMovement);
        }
    }
    public override void PhysicsUpdate()
    {
        pc.CurrentMovement.VerticalMovement();
    }
}

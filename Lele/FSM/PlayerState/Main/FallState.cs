using UnityEngine;
using System.Collections;
public class FallState : PlayerState
{
    public FallState(PlayerController pc) : base(pc) { }
    public override void Enter() 
    {
        pc.RB.gravityScale = pc.PATTRIBUTES.FallGravityScale;
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Falling, 0.1f);
        }
        else
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedFalling, 0.1f);
        }

    }
    public override void Exit()
    {
        Debug.Log("Exiting FallState");
        // Reset vertical velocity if needed
        pc.RB.linearVelocity = Vector2.zero; // Reset horizontal velocity
        pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale; // Reset gravity scale to default
    }
    public override void LogicUpdate()
    {
        if (pc.IsGrounded)
        {
            pc.ChangeState(pc.LandingState, pc.LandingMovement);
            
        }
        if (pc.GrabOnWallTriggered && pc.IsOnWall)
        {
            pc.ChangeState(pc.FallToGrabState, null);
        }
        if (pc.RopeShootingTriggered)
        {
            pc.ChangeState(pc.RopeShootingState, null);
        }

    }
    public override void PhysicsUpdate()
    {
        
        pc.CurrentMovement.HorizontalMovement();
       
    }
}

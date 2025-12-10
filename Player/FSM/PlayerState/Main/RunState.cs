using UnityEngine;
public class RunState : PlayerState
{
    public RunState(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Run, 0.1f);
        }
        else
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedWalk, 0.1f);
        }
    }
    public override void Exit()
    {
       pc.RB.linearVelocityX = 0f;
    }
    
  
    public override void LogicUpdate()
    {
        if (Mathf.Abs(pc.HDir) < 0.1f)
        {
            pc.ChangeState(pc.RunToIdleState, null);
        }
        
        if (Mathf.Abs(pc.HDir) > 0.1f && pc.IsOnWall
                && Mathf.Sign(pc.HDir) == Mathf.Sign(pc.transform.localScale.x))
        {
            pc.ChangeState(pc.WalkToPushState, null);
        }
        else
        {
            if (pc.JumpTriggered)
            {
                pc.ChangeState(pc.JumpState, pc.JumpMovement);
            }
            if (Mathf.Abs(pc.HDir) < 0.1f)
            {
                pc.ChangeState(pc.RunToIdleState, null);
            }
            if (pc.CoyoteCounter <= 0)
            {
                pc.ChangeState(pc.FallState, pc.FallMovement);
            }
        }
        
    }
    public override void PhysicsUpdate()
    {
        pc.CurrentMovement.HorizontalMovement();
    }
   
    
}

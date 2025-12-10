using UnityEngine;
using System.Collections;
public class PushState : PlayerState
{
    public PushState(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Push, 0.1f);
        }
        else
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedPush, 0.1f);
        }
    }
    public override void LogicUpdate()
    {
        if (pc.GrabOnWallTriggered)
        {
            if (pc.IsOnWall) pc.ChangeState(pc.PushToClimbState, null);
        }
        else
        {
            if (Mathf.Abs(pc.HDir) < 0.1f || !pc.IsOnWall)
            {
                pc.ChangeState(pc.PushToIdleState, null);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
    
}

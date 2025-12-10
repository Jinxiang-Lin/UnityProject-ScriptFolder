using UnityEngine;
using System.Collections;
public class FullState : PlantState
{
    public FullState(PlayerController pc) : base(pc) { }
    public override void Enter()
    {
        pc.IsWilting = false;
        pc.StartCoroutine(WaitAndPlay());
        Debug.Log("enter full state");
    }
    public override void Exit()
    {
        pc.IsWilting = false;
        Debug.Log("exit full state");
    }
    public override void LogicalUpdate()
    {
        if (pc.WateringState.CurrentWaterLevel <= pc.WateringState.WiltingThreshold)
        {
            pc.ChangePlantState(pc.WiltedState);
        }
    }
    public override IEnumerator WaitAndPlay()
    {
        AnimationClip theClip = null;
        
        if (pc.CurrentState is IdleState)
        {
            theClip = GetAnimationClipByName(AnimStates.IdleWiltedToFullTrans);
            pc.ANIMATOR.CrossFade(AnimStates.IdleWiltedToFullTrans, 0.1f);
        }
        else if (pc.CurrentState is RunState)
        {
            theClip = GetAnimationClipByName(AnimStates.WalkWiltedToFullTrans);
            pc.ANIMATOR.CrossFade(AnimStates.WalkWiltedToFullTrans, 0.1f);
        }
        else if (pc.CurrentState is JumpState)
        {
            theClip = GetAnimationClipByName(AnimStates.RisingWiltedToFullTrans);
            pc.ANIMATOR.CrossFade(AnimStates.RisingWiltedToFullTrans, 0.1f);
        }
        else if (pc.CurrentState is FallState)
        {
            theClip = GetAnimationClipByName(AnimStates.FallingWiltedToFullTrans);
            pc.ANIMATOR.CrossFade(AnimStates.FallingWiltedToFullTrans, 0.1f);
        }
        float waitTime = theClip != null ? theClip.length : 0f;
        yield return new WaitForSeconds(waitTime);
        if (pc.CurrentState is IdleState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Idle, 0.1f);
        }
        else if (pc.CurrentState is RunState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Run, 0.1f);
        }
        else if (pc.CurrentState is JumpState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.Rising, 0.1f);
        }
        else if (pc.CurrentState is FallState)
        { 
            pc.ANIMATOR.CrossFade(AnimStates.Falling, 0.1f);
        }
        
    }
    
}

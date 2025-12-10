using UnityEngine;
using System.Collections;
public class WiltedState : PlantState
{
    public WiltedState(PlayerController pc) : base(pc) 
    {
    }
    
    public override void Enter()
    {
        pc.IsWilting = true;
        pc.StartCoroutine(WaitAndPlay());
        Debug.Log("enter wilted state");
    }
    public override void Exit()
    {
        pc.IsWilting = false;
        Debug.Log("exit wilted state");
    }
    public override void LogicalUpdate()
    {
        if (pc.WateringState.CurrentWaterLevel > pc.WateringState.WiltingThreshold)
        { 
            pc.ChangePlantState(pc.FullState);
        }
    }
    public override IEnumerator WaitAndPlay()
    {
        AnimationClip theClip = null;
       
        if (pc.CurrentState is IdleState)
        {
            theClip = GetAnimationClipByName(AnimStates.IdleWiltingTrans);
            pc.ANIMATOR.CrossFade(AnimStates.IdleWiltingTrans, 0.1f);
        }
        else if (pc.CurrentState is RunState)
        {
            theClip = GetAnimationClipByName(AnimStates.WalkFullToWiltedTrans);
            pc.ANIMATOR.CrossFade(AnimStates.WalkFullToWiltedTrans, 0.1f);
        }
        else if (pc.CurrentState is JumpState)
        {
            theClip = GetAnimationClipByName(AnimStates.RisingFullToWiltedTrans);
            pc.ANIMATOR.CrossFade(AnimStates.RisingFullToWiltedTrans, 0.1f);
        }
        else if (pc.CurrentState is FallState)
        {
            theClip = GetAnimationClipByName(AnimStates.FallingFullToWiltedTrans);
            pc.ANIMATOR.CrossFade(AnimStates.FallingFullToWiltedTrans, 0.1f);
        }
        float waitTime = theClip != null ? theClip.length : 0f;
        yield return new WaitForSeconds(waitTime);
        if (pc.CurrentState is IdleState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.IdleWilting, 0.1f);
        }
        else if (pc.CurrentState is RunState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedWalk, 0.1f);
        }
        else if (pc.CurrentState is JumpState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedRising, 0.1f);
        }
        else if (pc.CurrentState is FallState)
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedFalling, 0.1f);
        }
        
    }
   
}

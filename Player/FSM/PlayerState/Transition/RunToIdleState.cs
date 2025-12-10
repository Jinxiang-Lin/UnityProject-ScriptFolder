using UnityEngine;
using System.Collections;
public class RunToIdleState : PlayerState
{
    public RunToIdleState(PlayerController pc) : base(pc)
    {
    }
    public override void Enter()
    {
        pc.StartCoroutine(WaitAndPlay(pc.PreviousState));
    }
    public override IEnumerator WaitAndPlay(PlayerState prevState)
    {
        if (!pc.IsWilting)
        {
            var theClip = GetAnimationClipByName(AnimStates.RunToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.RunToIdleState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        else
        {
            var theClip = GetAnimationClipByName(AnimStates.WiltedWalkToIdle);
            pc.ANIMATOR.CrossFade(AnimStates.WiltedWalkToIdleState, 0.1f);
            yield return WaitForAnimationOrStateChange(theClip);
        }
        
    }
    private IEnumerator WaitForAnimationOrStateChange(AnimationClip theClip)
    {
        float waitTime = theClip != null ? theClip.length : 0f;
        float elapsedTime = 0f;
        Debug.Log($"Waiting for animation clip: {theClip?.name} with duration: {waitTime}");
        while (elapsedTime < waitTime)
        {
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pc.ChangeState(pc.IdleState, pc.IdleMovement);
    }
    
}

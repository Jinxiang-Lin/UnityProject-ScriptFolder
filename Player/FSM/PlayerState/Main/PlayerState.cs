using System.Collections;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController pc;
    
    public PlayerState(PlayerController pc)
    { 
        this.pc = pc;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void OnJump() { }
    public virtual IEnumerator WaitAndPlay(PlayerState state) { yield return null; }
    public virtual IEnumerator WaitAndPlay() { yield return null; }
    public virtual AnimationClip GetAnimationClipByName(string clipName)
    {
        foreach (AnimationClip clip in pc.ANIMATOR.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {

                return clip;
            }

        }
        return null;
    }
}

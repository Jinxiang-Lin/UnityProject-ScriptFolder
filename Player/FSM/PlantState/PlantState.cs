using UnityEngine;
using System.Collections;
public abstract class PlantState
{
    protected PlayerController pc;
   
    public PlantState(PlayerController pc)
    {
        this.pc = pc;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void LogicalUpdate() { }
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

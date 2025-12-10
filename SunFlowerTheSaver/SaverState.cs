using UnityEngine;
using System.Collections;

public abstract class SaverState
{
    protected SaveManager sm;
    protected SaverState(SaveManager saveManager)
    {
        this.sm = saveManager;
    }
    public virtual void Enter() { }
    public abstract void Exit();
    public virtual IEnumerator WaitAndPlay() { yield return null; }
    public virtual AnimationClip GetAnimationClipByName(string clipName)
    {
        foreach (AnimationClip clip in sm.Ani.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {

                return clip;
            }

        }
        return null;
    }
}

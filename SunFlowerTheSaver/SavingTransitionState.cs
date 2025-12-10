using UnityEngine;

public class SavingTransitionState : SaverState
{
    public SavingTransitionState(SaveManager sm) : base(sm) 
    {
    }
    public override void Enter()
    {
        sm.StartCoroutine(WaitAndPlay());
    }
    public override void Exit()
    {
      
    }
    public override System.Collections.IEnumerator WaitAndPlay()
    {
        Debug.Log("Entering SavingTransitionState");
        AnimationClip clip = GetAnimationClipByName(SaverAnimator.awaken);
        if (clip != null)
        {
            sm.Ani.Play(clip.name);
            yield return new WaitForSeconds(clip.length);
        }
        sm.ChangeSaverState(sm.SavedState); // Transition to SavedState after playing the animation
    }

}

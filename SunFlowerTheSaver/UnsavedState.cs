using UnityEngine;

public class UnsavedState : SaverState
{
    public UnsavedState(SaveManager sm) : base(sm)
    {
    }
    public override void Enter()
    {
        Debug.Log("Entering UnsavedState");
        sm.Ani.Play(SaverAnimator.sadlyIdle);

    }
    public override void Exit()
    {
        Debug.Log("Exiting UnsavedState");
    }

}

using UnityEngine;

public class SavedState : SaverState
{
    public SavedState(SaveManager sm) : base(sm)
    {
    }
    public override void Enter()
    {
        Debug.Log("Entering savedState");
        sm.Ani.Play(SaverAnimator.stayAwaken);
        SaveSystem.SaveGameData(sm.GameDataSaved, sm.GameDataSaved.id);
    }
    public override void Exit()
    {
        Debug.Log("Exiting savedState");
    }
}

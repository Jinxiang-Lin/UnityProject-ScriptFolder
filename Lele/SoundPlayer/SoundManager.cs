using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region sounds
    FootstepSound footstepSound;
    #endregion
    private void Start()
    {
        footstepSound = new FootstepSound();
    }
    #region play sounds animation events
    public void PlayFootstepSoundLeft()
    {
        footstepSound?.PlayerLeftSound(this.transform);
    }
    public void PlayFootstepSoundRight()
    {
        footstepSound?.PlayerRightSound(this.transform);
    }
    #endregion
}

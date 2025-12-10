using UnityEngine;

public abstract class MySound
{

    public virtual void PlaySound() { }
    public virtual void PlayerLeftSound(Transform transform) { }
    public virtual void PlayerRightSound(Transform transform) { }
}

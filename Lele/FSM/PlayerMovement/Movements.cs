using UnityEngine;

public abstract class Movements
{
    protected PlayerController pc;
    public Movements(PlayerController pc)
    {
        this.pc = pc;
    }
    public virtual void HorizontalMovement() { }
    public virtual void VerticalMovement() { }
    public virtual void HVMovement() { }
    public virtual void Flip() 
    {
        if (pc.HDir > 0 && pc.RB.transform.localScale.x < 0)
        {
            pc.RB.transform.localScale = new Vector3(Mathf.Abs(pc.RB.transform.localScale.x), pc.RB.transform.localScale.y, pc.RB.transform.localScale.z);
        }
        else if (pc.HDir < 0 && pc.RB.transform.localScale.x > 0)
        {
            pc.RB.transform.localScale = new Vector3(-Mathf.Abs(pc.RB.transform.localScale.x), pc.RB.transform.localScale.y, pc.RB.transform.localScale.z);
        }
    }
}

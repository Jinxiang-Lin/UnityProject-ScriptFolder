using UnityEngine;

public class IdleToRunMovement : Movements
{
    public IdleToRunMovement(PlayerController pc) : base(pc) { }
    public override void HorizontalMovement()
    {
        Flip();
        float newHorizontalSpeed = 0f;
        if (pc.IsWilting)
        {
            newHorizontalSpeed = pc.HDir * pc.PATTRIBUTES.HorizontalSpeed * pc.PATTRIBUTES.HorizontalReductionVar;
        }
        else
        {
            newHorizontalSpeed = pc.HDir * pc.PATTRIBUTES.HorizontalSpeed;
        }
        pc.RB.linearVelocityX = newHorizontalSpeed;
        if (pc.IsOnWall && (Mathf.Sign(pc.HDir) == Mathf.Sign(pc.transform.localScale.x)))
        {
            pc.RB.linearVelocityX = 0f;
        }
    }
}

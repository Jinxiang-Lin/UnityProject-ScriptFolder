using UnityEngine;

public class FallMovement : Movements
{
    
    public FallMovement(PlayerController pc) : base(pc) { }
    public override void HorizontalMovement()
    {
        Flip();
        float newHorizontalSpeed;
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

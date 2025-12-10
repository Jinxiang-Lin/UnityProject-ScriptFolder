using UnityEngine;

public class ClimbToLedgeJumpMovement : Movements
{
    public ClimbToLedgeJumpMovement(PlayerController pc) : base(pc) { }
    public override void HVMovement()
    {
        float newX = pc.PATTRIBUTES.LedgeJumpForceHorizontal * pc.transform.localScale.x;
        float newY = pc.PATTRIBUTES.LedgeJumpForceVerticle;
        pc.RB.linearVelocity = new(newX, newY);
            
    }
}

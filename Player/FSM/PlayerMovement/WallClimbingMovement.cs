using UnityEngine;

public class WallClimbingMovement : Movements
{
    public WallClimbingMovement(PlayerController pc) : base(pc) { }
    public override void VerticalMovement()
    {
        float newY;
        if (pc.IsWilting)
        {
            newY = pc.PATTRIBUTES.ClimbSpeed * pc.VDir * pc.PATTRIBUTES.VerticalReductionVar;
        }
        else
        {
            newY = pc.PATTRIBUTES.ClimbSpeed * pc.VDir;
        }
        pc.RB.linearVelocityY = newY;
    }
}

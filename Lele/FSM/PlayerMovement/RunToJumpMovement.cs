using UnityEngine;

public class RunToJumpMovement : Movements
{
    public RunToJumpMovement(PlayerController pc) : base(pc) { }
    public override void HorizontalMovement()
    {
        Flip();
        pc.RB.linearVelocity = new Vector2(pc.HDir * pc.PATTRIBUTES.HorizontalSpeed, pc.RB.linearVelocity.y);
    }
    public override void VerticalMovement()
    {
        pc.RB.linearVelocity = new Vector2(pc.RB.linearVelocity.x, pc.PATTRIBUTES.InitialJumpForce); // Use JumpSpeed for vertical movement
    }
}

using UnityEngine;

public class IdleToJumpMovement : Movements
{
    public IdleToJumpMovement(PlayerController pc) : base(pc) { }
    public override void HorizontalMovement()
    {
        Flip();
        pc.RB.linearVelocity = new Vector2(pc.HDir * pc.PATTRIBUTES.HorizontalSpeed, pc.RB.linearVelocity.y);
    }
    public override void VerticalMovement()
    {
        //pc.RB.AddForce(Vector2.up * pc.PATTRIBUTES.InitialJumpForce * 0.5f, ForceMode2D.Impulse);
        //pc.RB.linearVelocity = new Vector2(pc.RB.linearVelocity.x, pc.PATTRIBUTES.InitialJumpForce); // Use JumpSpeed for vertical movement
    }
}

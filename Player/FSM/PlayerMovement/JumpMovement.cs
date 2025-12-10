using UnityEngine;

public class JumpMovement : Movements
{
    public JumpMovement(PlayerController pc) : base(pc) { }
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
    public override void VerticalMovement()
    {
        Vector2 newForce; // Declare without assigning a default value.  
        if (pc.IsWilting)
        {
            newForce = pc.PATTRIBUTES.InitialJumpForce * pc.PATTRIBUTES.VerticalReductionVar * Vector2.up;
        }
        else
        {
            newForce = Vector2.up * pc.PATTRIBUTES.InitialJumpForce;
        }
        pc.RB.AddForce(newForce, ForceMode2D.Impulse);

    }
}

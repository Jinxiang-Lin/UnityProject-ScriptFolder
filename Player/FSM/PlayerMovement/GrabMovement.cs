using UnityEngine;

public class GrabMovement : Movements
{
    public GrabMovement(PlayerController pc) : base(pc)
    {
    }
    public override void HVMovement()
    {
        pc.RB.linearVelocity = Vector2.zero;
    }
}

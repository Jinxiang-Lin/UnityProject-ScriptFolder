using UnityEngine;

public class CeilingDetector : ContactDetectorBase
{
    readonly float distance = 0f; // Distance to check for ceiling
    readonly float angle = 0f; // Angle for the box cast, 0 means aligned with the world axes   
    readonly Vector2 direction = Vector2.up; // Direction to check for ceiling
    float halfHeight;
    Vector2 checkOffset;
    Vector2 checkSize;
    public CeilingDetector(PlayerController pc) : base(pc) 
    {
        halfHeight = pc.BOXCOLLIDER.size.y / 2f;
        checkOffset = new Vector2(pc.BOXCOLLIDER.offset.x, pc.BOXCOLLIDER.offset.y + halfHeight);
        checkSize = new Vector2(pc.BOXCOLLIDER.size.x, 0.2f);
    }
    public override void PerformDetection()
    {
        
        Vector2 origin = (Vector2)pc.transform.position + checkOffset;
        RaycastHit2D hit = Physics2D.BoxCast(origin, checkSize, angle, direction, distance, pc.CL);
        if (hit.collider != null)
        {
            pc.IsCeilinged = true;
            //pc.ChangeState(pc.RiseToFallState, pc.RiseToFallMovement);
            // Handle ceiling collision logic here
        }
        else
        {
            pc.IsCeilinged = false;
        }
        Color color = pc.IsCeilinged ? Color.green : Color.red;
        DrawBoxCast(origin, checkSize, angle, direction, distance, color);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class GroundDetector : ContactDetectorBase
{
    Vector3 groundCorrection;
   
    float halfHeight;
    Vector2 checkOffset;
    Vector2 checkSize;
    float angle;
    readonly Vector2 direction = Vector2.down;
    readonly float distance = 0.1f;

    public Vector3 GroundCorrection { get => groundCorrection; private set => groundCorrection = value; }

    public GroundDetector(PlayerController pc) : base(pc) 
    {
        halfHeight = pc.BOXCOLLIDER.size.y / 2f;
        checkOffset = new Vector2(pc.BOXCOLLIDER.offset.x, pc.BOXCOLLIDER.offset.y - halfHeight);
        checkSize = new Vector2(pc.BOXCOLLIDER.size.x, 0.05f);
        groundCorrection = Vector3.zero;
        
        angle = 0f;
    }
   
    public override void PerformDetection()
    {
        Vector2 origin = (Vector2)pc.transform.position + checkOffset;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, checkSize, angle, direction, distance, pc.GL);
        if (hit.collider != null && hit.normal.y > 0.6f)
        {
            pc.IsGrounded = true;
            // change this to landing state, and switch landing state to idle or walk state
            if (pc.CurrentState is LandingState)
            {
                //pc.GroundNormal = hit.normal;
                //pc.GroundPoint = hit.point;
                float desiredPlayerBottomY = hit.point.y;
                float currentPlayerBottomY = pc.BOXCOLLIDER.bounds.min.y;
                float yCorrection = desiredPlayerBottomY - currentPlayerBottomY;
             
                groundCorrection = new Vector3(0f, yCorrection, 0f);
               
            }
            
        }
        else
        {
            pc.IsGrounded = false;
            //pc.GroundNormal = Vector2.zero;
            //pc.GroundPoint = Vector2.zero;
        }
        Color color = pc.IsGrounded ? Color.green : Color.red;
        Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
        DrawBoxCast(origin, checkSize, angle, direction, distance, color);
    }
}

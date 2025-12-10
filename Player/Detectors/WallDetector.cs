using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WallDetector : ContactDetectorBase
{
    float halfWidth;
    
    Vector2 checkOffset;
    Vector2 checkSize;
    readonly float checkDistance = 0.1f; // Distance to check for walls
    public WallDetector(PlayerController pc) : base(pc) 
    {
        halfWidth = pc.BOXCOLLIDER.size.x / 2f;
        
        checkSize = new Vector2(0.2f, pc.BOXCOLLIDER.size.y * 0.9f);
    }
    public override void PerformDetection()
    {
        if (pc.transform.localScale.x > 0f)
        {
            checkOffset = new Vector2(pc.BOXCOLLIDER.offset.x + halfWidth, pc.BOXCOLLIDER.offset.y);
        }
        else if (pc.transform.localScale.x < 0f)
        {
            checkOffset = new Vector2(pc.BOXCOLLIDER.offset.x - halfWidth, pc.BOXCOLLIDER.offset.y);
        }
        
        Vector2 origin = (Vector2)pc.transform.position + checkOffset;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, checkSize, 0f, Vector2.right * pc.transform.localScale.x, checkDistance, pc.WL);
        if (hit.collider != null)
        { 
            pc.IsOnWall = true;
        }
        else
        {
            pc.IsOnWall = false;
        }
        Color rayColor = pc.IsOnWall ? Color.blue : Color.cyan;
        
        DrawBoxCast(origin, checkSize, 0f, Vector2.right * pc.HDir, 0, rayColor);
    }
}

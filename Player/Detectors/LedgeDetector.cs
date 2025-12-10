using UnityEngine;

public class LedgeDetector : ContactDetectorBase
{
    private readonly float checkDistance = 0.3f;
    private bool isLedgeDetected;

    public bool IsLedgeDetected => isLedgeDetected;
   
    public LedgeDetector(PlayerController pc) : base(pc) 
    {
       
    }

    public override void PerformDetection()
    {
        CheckLedge();
    }

    private void CheckLedge()
    {
        Vector2 dir = pc.transform.localScale.x > 0f ? Vector2.right : Vector2.left;
        Vector2 origin = pc.transform.localScale.x > 0f
            ? new Vector2(pc.BOXCOLLIDER.bounds.max.x, pc.BOXCOLLIDER.bounds.max.y - 0.4f)
            : new Vector2(pc.BOXCOLLIDER.bounds.min.x, pc.BOXCOLLIDER.bounds.max.y - 0.4f);

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, checkDistance, pc.WL);

        isLedgeDetected = hit.collider == null;

        Debug.DrawRay(origin, dir * checkDistance, isLedgeDetected ? Color.green : Color.red);
    }
}

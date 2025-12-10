using UnityEngine;

public class RopeShootingState : PlayerState
{
    const int positionCountForRope = 2;
    float currentRopeLength = 0f;
    float ropeSpeed = 30f;
    Vector2 shotDir = Vector2.zero;
    Vector2 CurrentEndPos = Vector2.zero;
    
    private float maxRopeLength = 15f;
    bool ropeReachMaxLength = false;
    Vector2 ropeStartPos = Vector2.zero;
    Vector2 ropeTargetPos = Vector2.zero;
    float pullSpeed = 10f;
    bool isPulling = false;
    public RopeShootingState(PlayerController pc) : base(pc) 
    {
    }
    public override void Enter()
    {
        if (!pc.IsWilting)
        {
            pc.ANIMATOR.CrossFade(null, 0.1f);
        }
        else
        {
            pc.ANIMATOR.CrossFade(AnimStates.WiltedRopeShooting, 0.1f);
        }
        
        shotDir = new Vector2(pc.transform.localScale.x, pc.VDir).normalized;
        pc.LineRenderer.positionCount = positionCountForRope;
        pc.RopeShootingTriggered = false;
        pc.LineRenderer.startWidth = 0.25f;
        pc.LineRenderer.endWidth = 0.25f;
        ropeStartPos = pc.transform.position;
        CurrentEndPos = ropeStartPos;
        ropeTargetPos = Vector2.zero;
        currentRopeLength = 0f;
        ropeReachMaxLength = false;
        isPulling = false;

    }
    public override void Exit()
    {
        ropeReachMaxLength = false;
        currentRopeLength = 0f;
        pc.LineRenderer.positionCount = 0;
        CurrentEndPos = Vector2.zero;
        shotDir = Vector2.zero;
        pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale;
    }
    public override void LogicUpdate()
    {
        if (ropeReachMaxLength && !isPulling)
        { 
            pc.ChangeState(pc.IdleState, null);
        }
    }
    public override void PhysicsUpdate()
    {
        if (!ropeReachMaxLength)
        {
            ShootingRope();
        }
        else if(isPulling)
        { 
            PullPlayerToTarget();
        }
    }
    
    void ShootingRope()
    {
        // Move the rope end toward the direction
        ropeStartPos = pc.transform.position;
        CurrentEndPos += ropeSpeed * Time.fixedDeltaTime * shotDir;
        currentRopeLength = Vector2.Distance(ropeStartPos, CurrentEndPos);

        // Update LineRenderer
        pc.LineRenderer.SetPosition(0, ropeStartPos); // Keep start position fixed to player
        pc.LineRenderer.SetPosition(1, CurrentEndPos); // Update the end position as the rope extends
        // Check if the rope touches something (e.g., ground, wall, or ceiling)
        RaycastHit2D hit = Physics2D.Raycast(ropeStartPos, shotDir, currentRopeLength, pc.WallAndCeilingLayer);
        Debug.Log($"attachableSurface is {pc.WallAndCeilingLayer}");
        if (hit.collider != null && currentRopeLength >= hit.distance)
        {
            // Rope hits something, player can hang
            CurrentEndPos = hit.point;  // Set the rope end at the point where it hit
            ropeReachMaxLength = true;  // Stop extending rope
           
            Debug.Log($"Rope hit: {hit.collider.name} at position {hit.point}");
            ropeTargetPos = hit.point; // Set the target position for pulling the player
            isPulling = true; // Start pulling the player towards the rope end
            pc.RB.gravityScale = 0.2f;
        }
        // Check if the rope reaches its maximum length or hits something
        else if (currentRopeLength >= maxRopeLength)
        {
            ropeReachMaxLength = true; // Stop extending when the rope reaches max length
        }
    
    }
    void PullPlayerToTarget()
    {
        Vector2 playerPos = pc.transform.position;
        Vector2 newPos = Vector2.MoveTowards(playerPos, ropeTargetPos, pullSpeed * Time.fixedDeltaTime);

        pc.transform.position = newPos;

        // Update the rope to shrink with player movement
        ropeStartPos = newPos;
        pc.LineRenderer.SetPosition(0, ropeStartPos);
        pc.LineRenderer.SetPosition(1, CurrentEndPos);

        // Stop when player reaches the target
        if (Vector2.Distance(newPos, ropeTargetPos) < 0.01f || pc.IsOnWall || pc.IsCeilinged)
        {
            isPulling = false;
            Debug.Log("Player reached rope end");
            pc.RB.gravityScale = pc.PATTRIBUTES.BaseGravityScale;
            // Optionally transition to a new state (e.g., hanging)
            pc.ChangeState(pc.IdleState, null);
        }
    }


}

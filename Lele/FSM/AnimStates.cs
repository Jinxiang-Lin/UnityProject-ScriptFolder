using System.Diagnostics.Contracts;
using UnityEngine;

public static class AnimStates
{
    //main states
    public static readonly int Idle = Animator.StringToHash("idle");
    public static readonly int IdleWilting = Animator.StringToHash("idleWilting");
    public static readonly int Run = Animator.StringToHash("run");
    public static readonly int WiltedWalk = Animator.StringToHash("waltedWalk");
    public static readonly int Rising = Animator.StringToHash("rising");
    public static readonly int Falling = Animator.StringToHash("falling");
    public static readonly int WiltedRising = Animator.StringToHash("wiltedRising");
    public static readonly int WiltedFalling = Animator.StringToHash("wiltedFalling");
    public static readonly int WiltedPush = Animator.StringToHash("wiltedPush");
    public static readonly int WiltedClimb = Animator.StringToHash("wiltedClimb");
    public static readonly int wallClimbing = Animator.StringToHash("wallClimbing");
    public static readonly int WiltedGrabOnWall = Animator.StringToHash("wiltedGrabOnWall");
    public static readonly int GrabOnWall = Animator.StringToHash("grabOnWall");
    public static readonly int Push = Animator.StringToHash("push");
    public static readonly int Spirit = Animator.StringToHash("spirit");
    public static readonly int TurningToSpritState = Animator.StringToHash("turningToSprit");
    public static readonly int WiltedRopeShooting = Animator.StringToHash("wiltedRopeShooting");
    //transition states V2
    public static readonly int IdleToRunState = Animator.StringToHash("idleToRun");
    public static readonly int WiltedIdleToWalkState = Animator.StringToHash("wiltedIdleToWalk");

    public static readonly int IdleToJumpState = Animator.StringToHash("iToR");
    public static readonly int WiltedIdleToJumpState = Animator.StringToHash("wiltedIdleToRising");

    public static readonly int FallToRunState = Animator.StringToHash("fallToRun");
    public static readonly int WiltedFallToRunState = Animator.StringToHash("wiltedFallToWalk");

    //public static readonly int FallToIdleState = Animator.StringToHash("fToi");
    //public static readonly int WiltedFallToIdleState = Animator.StringToHash("wiltedFallingToIdle");

    public static readonly int RunToIdleState = Animator.StringToHash("runToIdle");
    public static readonly int WiltedWalkToIdleState = Animator.StringToHash("wiltedWalkToIdle");

    public static readonly int RunToJumpState = Animator.StringToHash("runToJump");
    public static readonly int WiltedWalkToJumpState = Animator.StringToHash("wiltedWalkToJump");

    public static readonly int RisingToFallingState = Animator.StringToHash("rTof");
    public static readonly int WiltedRisingToFallingState = Animator.StringToHash("wiltedRisingToFalling");

    public static readonly int WiltedPushToIdleState = Animator.StringToHash("wiltedPushToIdle");
    public static readonly int PushToIdleState = Animator.StringToHash("pushToIdle");

    public static readonly int WiltedPushToClimbState = Animator.StringToHash("wiltedPushToClimb");
    public static readonly int PushToGrabOnWallState = Animator.StringToHash("pushToGrabOnWall");

    public static readonly int WiltedWalkToPushState = Animator.StringToHash("wiltedWalkToPush");
    public static readonly int RunToPushState = Animator.StringToHash("runToPush");

    public static readonly int WiltedRiseToGrabState = Animator.StringToHash("wiltedRiseToGrab");
    public static readonly int RiseToGrabOnWallState = Animator.StringToHash("riseToGrabOnWall");

    public static readonly int WiltedFallToGrabState = Animator.StringToHash("wiltedFallToGrab");
    public static readonly int FallToGrabWallState = Animator.StringToHash("fallToGrabWall");

    public static readonly int WiltedLandingState = Animator.StringToHash("wiltedLanding");
    public static readonly int LandingState = Animator.StringToHash("landing");

    public static readonly int WiltedLandingToIdleState = Animator.StringToHash("wiltedLandingToIdle");
    public static readonly int LandingToIdleState = Animator.StringToHash("landingToIdle");

    public static readonly int WiltedLandingToWalkState = Animator.StringToHash("wiltedLandingToWalk");
    public static readonly int LandingToRunState = Animator.StringToHash("landingToRun");

    public static readonly int ClimbToLedgeJumpFullActionState = Animator.StringToHash("ClimbToLedgeJumpFullAction");
    public static readonly int WiltedClimbToJumpFullActionState = Animator.StringToHash("wiltedClimbToJumpFullAction");
    //transition states V1
    //public static readonly string FallToIdle = "fToi"; // check
    public static readonly string RunToIdle = "runToIdle"; // check
    public static readonly string IdleToRun = "idleToRun"; // check
    //public static readonly string FallToRun = "fallToRun"; // check
    public static readonly string RunToJump = "runToJump";// check
    public static readonly string IdleToRising = "iToR"; // check
    public static readonly string RisingToFalling = "rTof"; // check
    public static readonly string Landing = "landing"; // check
    public static readonly string LandingToIdle = "landingToIdle"; // check
    public static readonly string LandingToRun = "landingToRun"; // check
    public static readonly string WiltedIdleToWalk = "wiltedIdleToWalk"; // check
    public static readonly string WiltedWalkToIdle = "wiltedWalkToIdle"; // check
    public static readonly string WiltedIdleToRising = "wiltedIdleToRising"; // check
    public static readonly string WiltedWalkToJump = "wiltedWalkToJump"; // check
    public static readonly string WiltedRisingToFalling = "wiltedRisingToFalling";// check
    //public static readonly string WiltedFallingToIdle = "wiltedFallingToIdle"; // check
    public static readonly string WiltedFallToWalk = "wiltedFallToWalk"; // check
    public static readonly string WiltedLandingToWalk = "wiltedLandingToWalk"; // check
    public static readonly string WiltedWalkToPush = "wiltedWalkToPush"; // check
    public static readonly string WiltedPushToIdle = "wiltedPushToIdle";// check
    public static readonly string WiltedPushToClimb = "wiltedPushToClimb";// check
    public static readonly string WiltedRiseToGrab = "wiltedRiseToGrab"; // check
    public static readonly string WiltedFallToGrab = "wiltedFallToGrab"; // check
    public static readonly string WiltedLanding = "wiltedLanding"; // check
    public static readonly string WiltedLandingToIdle = "wiltedLandingToIdle"; // check
    public static readonly string FallToGrabWall = "fallToGrabWall"; // check
    public static readonly string PushToGrabOnWall = "pushToGrabOnWall"; // check
    public static readonly string RiseToGrabOnWall = "riseToGrabOnWall"; // check
    public static readonly string RunToPush = "runToPush"; // check
    public static readonly string PushToIdle = "pushToIdle"; // check
    public static readonly string ClimbToLedgeJumpFullAction = "ClimbToLedgeJumpFullAction"; // check
    public static readonly string WiltedClimbToJumpFullAction = "wiltedClimbToJumpFullAction"; // check

    public static readonly string FallingFullToWiltedTrans = "fallingFullToWilted";
    public static readonly string FallingWiltedToFullTrans = "fallingWiltedToFullTrans";
    public static readonly string WalkFullToWiltedTrans = "walkFullToWiltedTrans";
    public static readonly string WalkWiltedToFullTrans = "walkWiltedToFullTrans";
    public static readonly string RisingFullToWiltedTrans = "risingFullToWiltedTrans";
    public static readonly string RisingWiltedToFullTrans = "risingWiltedToFullTrans";
    public static readonly string IdleWiltingTrans = "idleWiltingTransition";
    public static readonly string IdleWiltedToFullTrans = "IdleWiltedToFullTrans";
    public static readonly string TurningToSprit = "turningToSprit";
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : PlayerManager
{
    

    [Header("UI Assignment")]
    [SerializeField] private Slider waterSlider;
    
    //ContactDetector section
    [Header("Contact Detector")]
    [SerializeField] bool isGrounded;
    [SerializeField] bool isOnWall;
    [SerializeField] bool isCeilinged;
    //
    [SerializeField] bool isWilting;
    [SerializeField] bool jumpTriggered;
    [SerializeField] bool grabOnWallTriggered;
    [SerializeField] bool isSpiritState;
    [SerializeField] bool ropeShootingTriggered;
    const float jumpBufferTime = 0.1f;
    [SerializeField] float jumpBufferCounter = 0f;
    const float coyoteTime = 0.1f;
    [SerializeField] float coyoteCounter = 0f;
    WateringState wateringState;
    private bool wasGroundedLastFrame;
    LineRenderer lineRenderer;
    //Detector
    public GroundDetector GroundDetector { get; private set; }
    public WallDetector WallDetector { get; private set; }
    public CeilingDetector CeilingDetector { get; private set; }
    public LedgeDetector LedgeDetector { get; private set; }
    //public PlayersDetector Detector { get; private set; }
    //States
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState { get; private set; }
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public PushState PushState { get; private set; }
    public GrabState GrabState { get; private set; }
    public SpiritState SpiritState { get; private set; }
    public WallClimbingState WallClimbingState { get; private set; }
    public RopeShootingState RopeShootingState { get; private set; }
    //Transition States
    public IdleToRunState IdleToRunState { get; private set; }
    //public IdleToJumpState IdleToJumpState { get; private set; }
    
    public LandingToIdleState LandingToIdleState { get; private set; }
    public RunToIdleState RunToIdleState { get; private set; }
    //public RunToJumpState RunToJumpState { get; private set; }
    public RiseToFallState RiseToFallState { get; private set; }
    public PushToIdleState PushToIdleState { get; private set; }
    public PushToClimbState PushToClimbState { get; private set; }
    public WalkToPushState WalkToPushState { get; private set; }
    public RiseToGrabState RiseToGrabState { get; private set; }
    public FallToGrabState FallToGrabState { get; private set; }
    public LandingState LandingState { get; private set; }
    public LandingToWalk LandingToWalkState { get; private set; }
    //Input variables
    public float HDir { get; private set; }
    public float VDir { get; private set; }
    //Movements
    public Movements CurrentMovement { get; private set; }
    public IdleMovement IdleMovement { get; private set; }
    public RunMovement RunMovement { get; private set; }
    public JumpMovement JumpMovement { get; private set; }
    public FallMovement FallMovement { get; private set; }
    public PushMovement PushMovement { get; private set; }
    public WallClimbingMovement WallClimbingMovement { get; private set; }
    public LandingMovement LandingMovement { get; private set; }
    public GrabMovement GrabMovement { get; private set; }
    public ClimbToLedgeJump ClimbToLedgeJumpState    { get; private set; }
    //Transition Movements
    public RiseToFallMovement RiseToFallMovement { get; private set; }
    public IdleToRunMovement IdleToRunMovement { get; private set; }
   
    public LandingToWalkMovement LandingToWalkMovement { get; private set; }
    public ClimbToLedgeJumpMovement ClimbToLedgeJumpMovement { get; private set; }

    //Plant states
    public bool IsWilting { get => isWilting; set => isWilting = value; }
    public PlantState CurrentPlantSate { get; private set; }
    public FullState FullState { get; private set; }
    public WiltedState WiltedState { get; private set; }
    public bool JumpTriggered { get => jumpTriggered; set => jumpTriggered = value; }
    public float CoyoteCounter { get => coyoteCounter; set => coyoteCounter = value; }
    public bool GrabOnWallTriggered { get => grabOnWallTriggered; set => grabOnWallTriggered = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsOnWall { get => isOnWall; set => isOnWall = value; }
    public bool IsCeilinged { get => isCeilinged; set => isCeilinged = value; }
    public bool IsSpiritState { get => isSpiritState; set => isSpiritState = value; }
    public Slider WaterSlider { get => waterSlider; set => waterSlider = value; }
    public WateringState WateringState { get => wateringState; set => wateringState = value; }
    public LineRenderer LineRenderer { get => lineRenderer; set => lineRenderer = value; }
    public bool RopeShootingTriggered { get => ropeShootingTriggered; set => ropeShootingTriggered = value; }

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        //RB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //RB.interpolation = RigidbodyInterpolation2D.Interpolate;
        //states
        IdleState = new IdleState(this);
        RunState = new RunState(this);
        JumpState = new JumpState(this);
        FallState = new FallState(this);
        PushState = new PushState(this);
        GrabState = new GrabState(this);
        SpiritState = new SpiritState(this);
        WallClimbingState = new WallClimbingState(this);
        RopeShootingState = new RopeShootingState(this);
        CurrentState = IdleState;
        PreviousState = null;
        //transition states
        IdleToRunState = new IdleToRunState(this);
        
        
        LandingToIdleState = new LandingToIdleState(this);
        RunToIdleState = new RunToIdleState(this);
       
        RiseToFallState = new RiseToFallState(this);
        PushToIdleState = new PushToIdleState(this);
        PushToClimbState = new PushToClimbState(this);
        WalkToPushState = new WalkToPushState(this);
        RiseToGrabState = new RiseToGrabState(this);
        FallToGrabState = new FallToGrabState(this);
        LandingState = new LandingState(this);
        LandingToWalkState = new LandingToWalk(this);
        ClimbToLedgeJumpState = new ClimbToLedgeJump(this);
        //movements
        IdleMovement = new IdleMovement(this);
        RunMovement = new RunMovement(this);
        JumpMovement = new JumpMovement(this);
        FallMovement = new FallMovement(this);
        PushMovement = new PushMovement(this);
        WallClimbingMovement = new WallClimbingMovement(this);
        LandingMovement = new LandingMovement(this);
        ClimbToLedgeJumpMovement = new ClimbToLedgeJumpMovement(this);
        GrabMovement = new GrabMovement(this);
        CurrentMovement = IdleMovement;
        //transition movements
        RiseToFallMovement = new RiseToFallMovement(this);
        IdleToRunMovement = new IdleToRunMovement(this);
        
        
        LandingToWalkMovement = new LandingToWalkMovement(this);
        //plant States
        FullState = new FullState(this);
        WiltedState = new WiltedState(this);
        
        //water state
        wateringState = new WateringState(this);
        //ContactDetector
        GroundDetector = new GroundDetector(this);
        WallDetector = new WallDetector(this);
        CeilingDetector = new CeilingDetector(this);
        LedgeDetector = new LedgeDetector(this);
        waterSlider = transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>();
        if (waterSlider == null)
        {
            Debug.LogError("Water slider not assigned in UIManager.");
        }
    }
    protected override void Start()
    {
        base.Start();
        JumpTriggered = false;
        isSpiritState = false;
        VDir = 0f;
        HDir = 0f;
        //Detector = new(BOXCOLLIDER, transform, GL, WL, CL);
        wateringState?.Enter();
        CurrentState?.Enter();
        if (wateringState.CurrentWaterLevel <= wateringState.WiltingThreshold)
        {
            CurrentPlantSate = WiltedState;
        }
        else
        {
            CurrentPlantSate = FullState;
        }
        CurrentPlantSate?.Enter();
        
    }
    //game logic
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsWilting = !IsWilting;
        }
        
        CurrentState?.LogicUpdate();
        wateringState?.LogicalUpdate();
        CurrentPlantSate?.LogicalUpdate();
        HandleCoyoteTime();
        HandleJumpBuffer();
       
    }
    //movements and physics stuff
    private void FixedUpdate()
    {
        //Detector.SurfaceDetectorCaller();
        CeilingDetector?.PerformDetection();
        GroundDetector?.PerformDetection();
        WallDetector?.PerformDetection();
        
        CurrentState?.PhysicsUpdate();
        
        
    }
    public void ChangeState(PlayerState newState, Movements newMovement)
    {
        PreviousState = CurrentState;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentMovement = newMovement;
        CurrentState?.Enter();
    }
    public void ChangePlantState(PlantState newPlantState)
    {
        if (newPlantState is WiltedState)
        {
            isWilting = true;
        }
        else
        {
            isWilting = false;
        }
        CurrentPlantSate = newPlantState;
        CurrentPlantSate?.Enter();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isSpiritState)
        {
            HDir = context.ReadValue<Vector2>().x;
            VDir = context.ReadValue<Vector2>().y;
        }
        
    }
    public void OnShootingRope(InputAction.CallbackContext context)
    {
        if (!isSpiritState)
        {
            if (context.performed && !(CurrentState is RopeShootingState) && !ropeShootingTriggered)
            {
                ropeShootingTriggered = true;
            }
        }

    }
    public void OnClimb(InputAction.CallbackContext context)
    {
        if (!isSpiritState)
        {
            if (context.performed)
            {
                grabOnWallTriggered = true;
            }
            else if (context.canceled)
            {
                grabOnWallTriggered = false;
            }
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isSpiritState)
        {
            if (!(CurrentState is PushState))
            {
                if (context.performed)
                {
                    jumpBufferCounter = jumpBufferTime;
                }
            }
        }
        
        
    }

    private void HandleCoyoteTime()
    {
        if (IsGrounded)
        {
            if (!wasGroundedLastFrame)
            {
                coyoteCounter = coyoteTime;
                wasGroundedLastFrame = true;
            }
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
            wasGroundedLastFrame = false;
        }
    }

    private void HandleJumpBuffer()
    {
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
            if (coyoteCounter > 0)
            {
                JumpTriggered = true;
                jumpBufferCounter = 0;
            }
        }
    }
    public void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has died.");
        ChangeState(SpiritState, null);
    }
    public void ConsumeWater(float amount)
    {
        if (wateringState != null)
        {
            wateringState.CurrentWaterLevel += amount;
            wateringState.CurrentWaterLevel = Mathf.Clamp(wateringState.CurrentWaterLevel, wateringState.MinWaterLevel,
                wateringState.MaxWaterLevel);
            WaterSlider.value = wateringState.CurrentWaterLevel;
            Debug.Log($"Consumed water: {amount}, Current water level: {wateringState.CurrentWaterLevel}");
        }
    }
    
}

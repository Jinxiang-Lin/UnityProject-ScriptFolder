using UnityEngine;

public class PlayerAttributes : PlayerManager
{
    [Header("Horizontal Movement Stats")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalReductionVar;
    [SerializeField] private float ledgeJumpForceHorizontal;

    [Header("Vertical Movement stats")]
    [SerializeField] private float initialJumpForce;
    [SerializeField] private float verticalReductionVar;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float ledgeJumpForceVerticle;

    [Header("Gravity Scale")]
    readonly private float baseGravityScale = 1f;
    readonly private float zeroGravityScale = 0f;
    [SerializeField] private float jumpGravityScale;
    [SerializeField] private float fallGravityScale;
    [SerializeField] private float negativeGravityScale;



    public float HorizontalSpeed { get => horizontalSpeed; set => horizontalSpeed = value; }
    public float InitialJumpForce { get => initialJumpForce; set => initialJumpForce = value; }
    public float HorizontalReductionVar {
        get
        {
            if (horizontalReductionVar <= 0f)
            {
                horizontalReductionVar = 0.5f;
            }
            return horizontalReductionVar;
        }
        set => horizontalReductionVar = value; 
    }

    public float VerticalReductionVar
    {
        get
        {
            if (verticalReductionVar <= 0f)
            {
                verticalReductionVar = 0.5f;
            }
            return verticalReductionVar;
        }
        set => verticalReductionVar = value;
    }

    public float ClimbSpeed 
    { 
        get
        {
            if (climbSpeed <= 0f)
            {
                climbSpeed = 2f;
            }
            return climbSpeed;
        }
        set => climbSpeed = value;
    }

    public float LedgeJumpForceHorizontal { get => ledgeJumpForceHorizontal; set => ledgeJumpForceHorizontal = value; }
    public float LedgeJumpForceVerticle { get => ledgeJumpForceVerticle; set => ledgeJumpForceVerticle = value; }

    public float BaseGravityScale => baseGravityScale;

    public float ZeroGravityScale => zeroGravityScale;

    public float JumpGravityScale 
    {
        get
        {
            if (jumpGravityScale <= 0f) jumpGravityScale = 3f; // Default jump gravity scale
            return jumpGravityScale;
        }
        set => jumpGravityScale = value; 
    }
    public float FallGravityScale 
    {
        get
        { 
            if(fallGravityScale <= 0f) fallGravityScale = 3f; // Default fall gravity scale
            return fallGravityScale;
        }
        set => fallGravityScale = value; }

    public float NegativeGravityScale 
    {
        get
        { 
            if(negativeGravityScale >= 0f) negativeGravityScale = -0.05f; // Default negative gravity scale
            return negativeGravityScale;
        }
        set => negativeGravityScale = value; }
}

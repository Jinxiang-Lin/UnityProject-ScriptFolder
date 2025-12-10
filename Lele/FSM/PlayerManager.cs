using UnityEngine;


public abstract class PlayerManager : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _ani;
    private CapsuleCollider2D _bCol;
    PlayerAttributes _attributes;
    LayerMask gl;
    LayerMask wl;
    LayerMask cl;
    LayerMask wallAndCeilingLayer;
    public Rigidbody2D RB
    {
        get
        {
            if (_rb == null)
            { 
                _rb = GetComponent<Rigidbody2D>();
            }
            return _rb;
        }
    }
    public Animator ANIMATOR
    {
        get
        {
            if (_ani == null)
            { 
                _ani = transform.GetChild(0).GetComponent<Animator>();
            }
            return _ani;
        }
    }

    public CapsuleCollider2D BOXCOLLIDER
    {
        get
        {
            if (_bCol == null)
            { 
                _bCol = GetComponent<CapsuleCollider2D>();
            }
            return _bCol;
        }
    }

    public LayerMask GL { get => gl; }
    public LayerMask WL { get => wl; }
    public LayerMask CL { get => cl; }

    public PlayerAttributes PATTRIBUTES
    {
        get
        {
            if (_attributes == null)
            { 
                _attributes = GetComponent<PlayerAttributes>();
            }
            return _attributes;
        }
    }

    public LayerMask WallAndCeilingLayer { get => wallAndCeilingLayer; }

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ani = transform.GetChild(0).GetComponent<Animator>();
        _bCol = GetComponent<CapsuleCollider2D>();
        _attributes = GetComponent<PlayerAttributes>();
    }
    protected virtual void Start()
    {
        
        gl = LayerMask.GetMask("Ground");
        wl = LayerMask.GetMask("Wall");
        cl = LayerMask.GetMask("Ceiling");
        wallAndCeilingLayer = wl | cl;
    }
}

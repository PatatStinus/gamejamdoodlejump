using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    private float _moveInput;
    private Rigidbody2D _rb2D;

    public Transform groundCheckPoint, groundCheckPoint2;
    public LayerMask whatIsGround;
    private bool _isGrounded;

    public float hangTime = .1f;
    private float _hangCounter;
    private float _moveRightLeft;

    private bool _touchingWall;
    public Transform wallCheck, wallCheck2;
    public LayerMask wallLayer;
    private bool _wallJumping;
    public float pushOutFromWallForce;



    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        //stores the value of the direction player last moved
        if (_moveInput > 0 && _isGrounded)
        {
            _moveRightLeft = moveSpeed;
        }
        else if (_moveInput < 0 && _isGrounded)
        {
            _moveRightLeft = -moveSpeed;
        }

        //check if player is on the ground
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, whatIsGround) ||
                     Physics2D.OverlapCircle(groundCheckPoint2.position, 0.1f, whatIsGround);

        //check if player is touching a wall
        _touchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer) || 
                        Physics2D.OverlapCircle(wallCheck2.position, 0.1f, wallLayer);

        //if the player jumps a bit to late he will still jump and movement only when grounded cant change mid air
        if (_isGrounded)
        {
            _hangCounter = hangTime;
            _rb2D.velocity = new Vector2(_moveInput * moveSpeed, _rb2D.velocity.y);
            _wallJumping = false;
        }
        else
        {
            _hangCounter -= Time.deltaTime;
        }

        //jump in the air
        if (Input.GetButton("Jump") && _hangCounter > 0f)
        {
            _rb2D.velocity = new Vector2(_moveRightLeft, jumpForce);
        }

        //jump reduced in height if jump button is let go before reaching max height
        if (Input.GetButtonUp("Jump") && _rb2D.velocity.y > 0)
        {
            _rb2D.velocity = new Vector2(_moveRightLeft, _rb2D.velocity.y * .5f);
        }

        //if player touches a wall while in the air bounce to the other side
        if (_touchingWall && !_isGrounded && !_wallJumping)
        {
            _wallJumping = true;
            _rb2D.velocity = new Vector2(-_moveRightLeft * pushOutFromWallForce, _rb2D.velocity.y * 0.5f);
        }


    }

}

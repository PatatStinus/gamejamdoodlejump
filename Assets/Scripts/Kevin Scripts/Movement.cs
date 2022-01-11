using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IO;

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
    //private float moveRightLeft;


    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        //Movement horizontally
        if (_isGrounded)
        {
            _rb2D.velocity = new Vector2(_moveInput * moveSpeed, _rb2D.velocity.y);
        }

        /* if (_moveInput > 0 && _isGrounded)
         {
             moveRightLeft = moveSpeed;
         }
         else if (_moveInput < 0 && _isGrounded)
         {
             moveRightLeft = -moveSpeed;
         }
        */

        //check if player is on the ground
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, whatIsGround) ||
                     Physics2D.OverlapCircle(groundCheckPoint2.position, 0.1f, whatIsGround);

        //if the player jumps a bit to late he will still jump
        if (_isGrounded)
        {
            _hangCounter = hangTime;
        }
        else
        {
            _hangCounter -= Time.deltaTime;
        }

        //jump in the air
        if (Input.GetButtonDown("Jump") && _hangCounter > 0f)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpForce);
        }
        //jump reduced in height if jump button is let go before reaching max height
        if (Input.GetButtonUp("Jump") && _rb2D.velocity.y > 0)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _rb2D.velocity.y * .5f);
        }
    }
}

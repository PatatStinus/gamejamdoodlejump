using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IO;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D _rb2D;

    public Transform groundCheckPoint, groundCheckPoint2;
    public LayerMask whatIsGround;
    private bool _isGrounded;

    public float hangTime = .2f;
    private float _hangCounter;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Movement horizontally
        _rb2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, _rb2D.velocity.y);
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

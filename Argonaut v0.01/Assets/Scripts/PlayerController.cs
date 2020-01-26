using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed; //variable to adjust player speed
    public float playerJumpForce; //variable to adjust player jump force
    public float checkRadius; //variable to check radius
    public int extraJumpValue; // variable to adjust extra jumps via unity
    public Transform groundCheck; //Transform variable to check the ground
    public LayerMask whatIsGround; //LayerMask variable to know what is ground

    private float playerMoveInput; //variable for player move input
    private bool isGrounded; //bool variable for checking if player is on ground
    private SpriteRenderer psrite; //SpriteRender variable to control spriterenderer via script
    private Rigidbody2D rb; //RigidBody2D variable to control rigidbody2d via script
    private int extraJumps; //variable to check extra jumps

    // Start is called before the first frame update
    void Start()
    {
        //tweak player rigid body thru script
        rb = GetComponent<Rigidbody2D>();
        psrite = GetComponent<SpriteRenderer>();

        extraJumps = extraJumpValue;
    }

    void Update()
    {
        playerJump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //get left and right move input
        playerMoveInput = Input.GetAxisRaw("Horizontal"); 
        rb.velocity = new Vector2(playerMoveInput * playerSpeed, rb.velocity.y);

        //flip sprite
        if (playerMoveInput > 0)
        {
            psrite.flipX = false;
        }else if (playerMoveInput < 0)
        {
            psrite.flipX = true;
        }

        
    }

    void playerJump()
    {
        if(isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * playerJumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * playerJumpForce;
        }
    }

    
}

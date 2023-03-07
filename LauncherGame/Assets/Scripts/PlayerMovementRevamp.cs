using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRevamp : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    // Base Movement Variables
    private bool canJump;
    private bool firstJump;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;


    // Rocket Jump Variables
    private bool rocketJumpAvailable;
    [SerializeField] private float rocketChargeVal = 0.0f;
    [SerializeField] private float rocketChargeScale = .05f;
    [SerializeField] private float rocketChargeMax = 10.0f;


    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    private void Update()    {
        IsGrounded();
        movementMechanics();
        UpdateAnimationState();
        Debug.Log("canJump: " + canJump + " | rocketJumpAvailable: " + rocketJumpAvailable + " | rocketChargeVal: " + rocketChargeVal);
    }
    void movementMechanics()
    {
        /* Sets a velocity of X based on the left-right axis input (A, D by default)
        canJump is a variable set by the IsGrounded() function. firstJump tracks the first jumpkey depression and release.
        Once the user jumps, canJump is set to false, firstJump is set to false, rocketJumpAvailable is set to true
        One the jump key is released and pressed again, a rocketjump intitates. This charges the rocketChargeVal by increments of rocketChargeScale until released, which then launches the player. */
        
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && canJump)
        {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                firstJump = true;
        }
        else if(Input.GetButtonUp("Jump") && firstJump)    
        {
            rocketJumpAvailable = true;
            firstJump = false;
        }
        else if (rocketJumpAvailable && Input.GetButton("Jump"))
            rocketChargeVal += rocketChargeScale;
        else if(rocketJumpAvailable && Input.GetButtonUp("Jump"))
        {
            if(rocketChargeVal > rocketChargeMax)
                rocketChargeVal = rocketChargeMax;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce + rocketChargeVal);
            rocketJumpAvailable = false;
            firstJump = true;
        }

    }
    void IsGrounded()
    {

        /* Checks a created BoxCast below the player character to see if it connects to any texture tagged 'jumpableGround'. If so, canJump is set to true
        If canJump is true, the rocket jump is disabled, and the charge value is reset. */

        canJump = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        if(canJump)
        {
            rocketJumpAvailable = false;
            rocketChargeVal = 0;
        }
    }
    void UpdateAnimationState()
    {
        /* Creates a MovementState variable to be passed to the Unity editor. The value of this state can be running, idle, jumping, or false. 
        Uses dirX and velocity.y to determine current movement, also flipping the sprite on negative x-directions. */

        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
}
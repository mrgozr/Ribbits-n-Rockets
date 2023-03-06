using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    // MOVEMENT FORCE SECTION
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // ROCKET JUMP SECTION
    private bool rocketReady;
    private float rocketCD = 1.2f;
    private float rocketCDCurrent = 0.0f;
    private float rocketPower = 0.0f;
    [SerializeField] private float rocketPowerScale = 0.05f;
    [SerializeField] private float rocketPowerMax = 10;
    private float rocketPowerAtomic = 100;
    private bool rocketAtomicEnabled = false;
    private Transform launchDirection;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // IF PLAYER JUMPS
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ROCKET JUMP COOLDOWN CHECK
        if(rocketCDCurrent >= rocketCD)
        {
            rocketReady = true;
        }
        else
        {
            rocketCDCurrent = rocketCDCurrent + Time.deltaTime;
            rocketReady = false;
        }

        if(IsGrounded())
        {
            rocketPower = 0;
        }

        // CHARGE UP ROCKET JUMP
        if (Input.GetKey("space") && rocketReady)
        {
            rocketPower += rocketPowerScale;
        }

        // IF ROCKET JUMP IS RELEASED
        if (Input.GetKey("space") == false && rocketPower > 0 && rocketReady && !IsGrounded())
        {
            // MAX LIMIT ON HOW MUCH EXTRA FORCE ADDED TO JUMP
            if (rocketPower > rocketPowerMax)
            {
                rocketPower = rocketPowerMax;
            }
            // IF ATOMIC CHARGE IS ENABLED (POSSIBLY CONSIDERED A CHEAT IF ENABLED)
            if (rocketPower > rocketPowerAtomic && rocketAtomicEnabled)
            {
                rocketPower = rocketPowerAtomic;
            }
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce + rocketPower);
            rocketPower = 0;
            rocketCDCurrent = 0.0f;
        }
        Debug.Log("rocketPower =" + rocketPower);

        // IF PLAYER USES ROCKET JUMP
        //if (Input.GetMouseButtonDown(0) && rocketReady)
        //{
            // RESTART COOLDOWN AND LAUNCH PLAYER
            //rocketCDCurrent = 0.0f;
            //RocketLaunchPlayer();
        //}
        
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        //Several if() checks to determine player movement, updates sprite accordingly [uses DirX and vel.y respectively]
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

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // ROCKET JUMP CALCULATIONS AND LAUNCH
    private void RocketLaunchPlayer()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse: " + cursorPosition);
        Debug.Log("RB: " + rb.transform.position);

        // GET DIFFERENCE BETWEEN MOUSE AND PLAYER POSITION AND GET ANGLE IN RADIANS/DEGREES
        float xdif = cursorPosition.x - rb.transform.position.x;
        float ydif = cursorPosition.y - rb.transform.position.y;
        float anglerad = Mathf.Atan2(xdif,ydif);                    // ANGLE IN RADIANS
        float angledeg = Mathf.Atan2(xdif,ydif) * Mathf.Rad2Deg;    // ANGLE IN DEGREES
        Debug.Log("angle: " + angledeg);
        // DEFAULT VELOCITY (FOR NOW) THAT IS BASICALLY A SUPER JUMP
        rb.velocity = new Vector2(rb.velocity.x*2, jumpForce*2);
    }
}

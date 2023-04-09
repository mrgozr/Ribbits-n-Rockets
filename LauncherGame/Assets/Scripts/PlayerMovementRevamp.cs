using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementRevamp : MonoBehaviour
{
    public Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    // Base Movement Variables
    private bool canJump;
    private bool firstJump;
    private bool isFalling;
    private bool fallJump;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float baseMoveSpeed;

    // Rocket Jump Variables
    private bool rocketJumpAvailable;
    public bool isChargingRocketJump;
    public float rocketChargeVal = 1.0f;
    public float rocketChargeScale = .05f;
    public float rocketChargeMax = 10.0f;

    // Sprite Modification Variables
    private enum MovementState { idle, running, jumping, falling }

    // Sound Variables
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource rocketChargeSoundEffect;
    [SerializeField] private AudioSource rocketReleaseSoundEffect;
    private bool rocketChargeStartedSound;

    private float TimeT;
    private float showtime;

    private void Awake()
    {
        baseMoveSpeed = moveSpeed;
    }
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
        ChargeJumpAudio();
        Debug.Log(
        "   | isChargingRocketJump: " + isChargingRocketJump
        + " | rocketJumpAvailable: " + rocketJumpAvailable
        + " | rocketChargeVal: " + rocketChargeVal
        + " | firstJump: " + firstJump
        + " | isFalling: " + isFalling
        + " | fallJump: " + fallJump
        );

        // showtime += Time.deltaTime;
        // Debug.Log ("TIMER:" + showtime);

    }
    void movementMechanics()
    {
        /* Sets a velocity of X based on the left-right axis input (A, D by default)
        canJump is a variable set by the IsGrounded() function. firstJump tracks the first jumpkey depression and release.
        Once the user jumps, canJump is set to false, firstJump is set to false, rocketJumpAvailable is set to true
        Once the jump key is released and pressed again, a rocketjump intitates. This charges the rocketChargeVal by increments of rocketChargeScale until released, which then launches the player. */
        
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Cancel"))  //Main menu interface
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetButtonDown("Jump") && canJump)  //First jump button
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            firstJump = true;
        }
        else if(Input.GetButtonUp("Jump") && firstJump)  //First jump button released
        {
            rocketJumpAvailable = true;
            firstJump = false;
        }
        else if ( ( rocketJumpAvailable && Input.GetButton("Jump") ) || ( isFalling && Input.GetButtonDown("Jump") && fallJump ) )  //Charging rocket jump
        {
            rocketChargeVal += (rocketChargeScale * Time.deltaTime);
            isChargingRocketJump = true;
        }
        else if( ( rocketJumpAvailable && Input.GetButtonUp("Jump") ) || ( isFalling && Input.GetButtonUp("Jump") && fallJump ) )   //Release rocket jump
        {
            rocketChargeSoundEffect.Stop();
            rocketReleaseSoundEffect.Play();
            isChargingRocketJump = false;

            if(rocketChargeVal > rocketChargeMax)
                rocketChargeVal = rocketChargeMax;

            moveSpeed += (rocketChargeVal/2.5f);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce + rocketChargeVal);
            rocketJumpAvailable = false;
            fallJump = false;
        }

    }
    void IsGrounded()
    {

        /* Checks a created BoxCast below the player character to see if it connects to any texture tagged 'jumpableGround'. If so, canJump is set to true
        If canJump is true, the rocket jump is disabled, and the charge value is reset. */

        canJump = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        if(canJump)
        {
            isChargingRocketJump = false;
            rocketJumpAvailable = false;
            fallJump = true;
            rocketChargeSoundEffect.Stop();
            rocketChargeVal = 0;
            moveSpeed = 10.0f;
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

        if(rb.velocity.y < -.1f)
            isFalling = true;
        else
            isFalling = false;

        anim.SetInteger("state", (int)state);
    }
    void ChargeJumpAudio()
    {
        if(isChargingRocketJump && !rocketChargeStartedSound)
        {
            rocketChargeStartedSound = true;
            rocketChargeSoundEffect.Play();
        }
        if(!isChargingRocketJump)
            rocketChargeStartedSound = false;
        /* Uses a semaphore variable (rocketChargeStartedSound) to lock and check this value, so the sound file does not repeatedly play every frame, but rather once until done.        
        */
    }
}
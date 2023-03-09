using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    private Rigidbody2D cb;             // CHARGE BAR SPRITE BODY
    private SpriteRenderer cbsprite;    // CHARGE BAR SPRITE RENDERER
    PlayerMovementRevamp playerData;    // GRABS PLAYER DATA
    public Sprite noCharge;             // NO CHARGE SPRITE
    public Sprite firstCharge;          // FIRST CHARGE SPRITE
    public Sprite secondCharge;         // SECOND CHARGE SPRITE
    public Sprite maxCharge;            // MAX CHARGE SPRITE

    // Awake starts before Start
    void Awake()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerMovementRevamp>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cb = GetComponent<Rigidbody2D>();
        cbsprite = GetComponent<SpriteRenderer>();
        cbsprite.sprite = noCharge;
        cbsprite.sprite = firstCharge;
        cbsprite.sprite = secondCharge;
        cbsprite.sprite = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        // IF PLAYER IS CHARGING UP ROCKET JUMP
        if (playerData.isChargingRocketJump)
        {
            cbsprite.enabled = true;    // SHOW THE CHARGE BAR
            followPlayer();             // SET BAR POSITION
            chargeTheBar();             // SET BAR SPRITE
        }
        // IF PLAYER ISN'T CHARGING UP ROCKET JUMP
        else
        {
            cbsprite.enabled = false;   // HIDE THE CHARGE BAR
        }
    }

    // FOLLOWS PLAYER AND SETS THE POSITION OF CHARGE BAR ABOVE
    void followPlayer()
    {
        Vector3 cbPosition = cb.transform.position;
        cbPosition.x = playerData.rb.transform.position.x + 2;
        cbPosition.y = playerData.rb.transform.position.y + 2;
        cb.transform.position = cbPosition;
    }

    // SHOWS BAR CHARGING PROGRESS TO USER
    void chargeTheBar()
    {
        float maxcapacity = playerData.rocketChargeMax;
        float currentcharge = playerData.rocketChargeVal * playerData.rocketChargeScale;

        // MAX CAPACITY
        if (currentcharge >= maxcapacity)
        {
            cbsprite.sprite = maxCharge;
        }
        // 2/3 CHARGE
        else if (currentcharge >= ((maxcapacity*2)/3))
        {
            cbsprite.sprite = secondCharge;
        }
        // 1/3 CHARGE
        else if (currentcharge >= maxcapacity / 3)
        {
            cbsprite.sprite = firstCharge;
        }
        // NO CHARGE
        else
        {
            cbsprite.sprite = noCharge;
        }
    }
}

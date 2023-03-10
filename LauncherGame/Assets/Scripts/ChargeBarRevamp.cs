using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarRevamp : MonoBehaviour
{
    private Rigidbody2D cb;             // CHARGE BAR SPRITE BODY
    private SpriteRenderer cbsprite;    // CHARGE BAR SPRITE RENDERER
    public Sprite emptyCharge;
    public Sprite noCharge;             // NO CHARGE SPRITE
    public Sprite firstCharge;          // FIRST CHARGE SPRITE
    public Sprite secondCharge;         // SECOND CHARGE SPRITE
    public Sprite maxCharge;            // MAX CHARGE SPRITE

    private float rocketChargeV;
    private float rocketChargeM;
    
    public PlayerMovementRevamp playerData;

    // Awake starts before Start
    void Awake()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerMovementRevamp>();
        rocketChargeM = playerData.rocketChargeMax;      
    }

    // Start is called before the first frame update
    void Start()
    {
        cbsprite = GetComponent<SpriteRenderer>();
        cbsprite.sprite = emptyCharge;
        cbsprite.sprite = noCharge;
        cbsprite.sprite = firstCharge;
        cbsprite.sprite = secondCharge;
        cbsprite.sprite = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        chargeUpTheBar(); 
        //         Debug.Log(
        // "   | rocketChargeV: " + rocketChargeV
        // + " | rocketChargeM: " + rocketChargeM
        // );
    }
    // SHOWS BAR CHARGING PROGRESS TO USER
    void chargeUpTheBar()
    {
        rocketChargeV = playerData.rocketChargeVal;
        // MAX CAPACITY
        if (rocketChargeV >= rocketChargeM)
        {
            cbsprite.sprite = maxCharge;
        }
        // 2/3 CHARGE
        else if (rocketChargeV >= (2.0f * (rocketChargeM/ 3.0f) ))
        {
            cbsprite.sprite = secondCharge;
        }
        // 1/3 CHARGE
        else if (rocketChargeV >= rocketChargeM / 3.0f)
        {
            cbsprite.sprite = firstCharge;
        }
        // NO CHARGE
        else if (rocketChargeV > 0)
        {
            cbsprite.sprite = noCharge;
        }
        // NO UI
        else
        {
            cbsprite.sprite = emptyCharge;
        }
    }
}
    


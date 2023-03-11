using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarRevamp : MonoBehaviour
{
    private Rigidbody2D cb;             // CHARGE BAR SPRITE BODY
    private SpriteRenderer cbsprite;    // CHARGE BAR SPRITE RENDERER
    public Sprite blankCharge;
    public Sprite noCharge;             // NO CHARGE SPRITE
    public Sprite firstCharge;          // FIRST CHARGE SPRITE
    public Sprite secondCharge;         // SECOND CHARGE SPRITE
    public Sprite maxCharge;            // MAX CHARGE SPRITE


    [SerializeField] private float timerAnim;
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
        cbsprite.sprite = blankCharge;
        cbsprite.sprite = noCharge;
        cbsprite.sprite = firstCharge;
        cbsprite.sprite = secondCharge;
        cbsprite.sprite = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        chargeUpTheBar(); 
        Debug.Log("Time = " + timerAnim);
    }
    // SHOWS BAR CHARGING PROGRESS TO USER
    void chargeUpTheBar()
    {
        if(playerData.isChargingRocketJump)
        {
            timerAnim += Time.deltaTime;
            // MAX CAPACITY
            if (timerAnim >= .99f)
            {
                cbsprite.sprite = maxCharge;
            }
            // 2/3 CHARGE
            else if (timerAnim >= .66f)
            {
                cbsprite.sprite = secondCharge;
            }
            // 1/3 CHARGE
            else if (timerAnim >= .33f)
            {
                cbsprite.sprite = firstCharge;
            }
            // NO CHARGE
            else if (timerAnim > 0)
            {
                cbsprite.sprite = noCharge;
            }
            // NO UI
        }
        else
        {
            timerAnim = 0;
            cbsprite.sprite = blankCharge;
        }            
    }
}
    


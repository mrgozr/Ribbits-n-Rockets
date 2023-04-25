using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarRevamp : MonoBehaviour
{
    private Rigidbody2D cb;             // CHARGE BAR SPRITE BODY
    private SpriteRenderer cbsprite;    // CHARGE BAR SPRITE RENDERER
    
    public Sprite blankCharge;
    public Sprite noCharge;             // NO CHARGE SPRITE
    public Sprite firstCharge1;         // FIRST CHARGE SPRITE  1/2
    public Sprite firstCharge2;         // FIRST CHARGE SPRITE  2/2
    public Sprite secondCharge1;        // SECOND CHARGE SPRITE 1/2
    public Sprite secondCharge2;        // SECOND CHARGE SPRITE 2/2
    public Sprite maxCharge1;           // MAX CHARGE SPRITE    1/2
    public Sprite maxCharge2;           // MAX CHARGE SPRITE    2/2

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
        cbsprite.sprite = firstCharge1;
        cbsprite.sprite = firstCharge2;
        cbsprite.sprite = secondCharge1;
        cbsprite.sprite = secondCharge2;
        cbsprite.sprite = maxCharge1;
        cbsprite.sprite = maxCharge2;        
    }

    // Update is called once per frame
    void Update()
    {
        chargeUpTheBar(); 
    }
    // SHOWS BAR CHARGING PROGRESS TO USER
    void chargeUpTheBar()
    {
        if(playerData.isChargingRocketJump)
        {
            timerAnim += Time.deltaTime;

            if (timerAnim >= .72f)
            {
                cbsprite.sprite = maxCharge2;
            }
            else if (timerAnim >= .60f)
            {
                cbsprite.sprite = maxCharge1;
            }            
            // MAX CAPACITY
            else if (timerAnim >= .48f)
            {
                cbsprite.sprite = secondCharge2;
            }
            // 2/3 CHARGE
            else if (timerAnim >= .36f)
            {
                cbsprite.sprite = secondCharge1;
            }
            // 1/3 CHARGE
            else if (timerAnim >= .24f)
            {
                cbsprite.sprite = firstCharge2;
            }
            else if (timerAnim >= .12f)
            {
                cbsprite.sprite = firstCharge1;
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
    


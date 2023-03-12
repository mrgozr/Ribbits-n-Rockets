/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{

    PlayerLife playerData;
    KeyBehavior keyData;
    private SpriteRenderer doorSprite;
    public Sprite closed;
    public Sprite open;

    // Start is called before the first frame update
    void Awake()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerLife>();
        keyData = GameObject.Find("Key").GetComponent<KeyBehavior>();
    }

    void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerData.hasKey)
        {
            //if the player touches the door and has the key, open the door
            doorSprite.sprite = open;
            GetComponent<BoxCollider2D>().isTrigger = true;
            keyData.keyUsed = true;
        }
    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{

    [SerializeField] Transform targetToFollow;
    PlayerLife playerData;
    public bool keyUsed = false;

    void Awake()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerData.hasKey && !keyUsed)
        {
            //if the player has picked up the key, have the key follow the player
            transform.position = new Vector3(
                targetToFollow.position.x, (targetToFollow.position.y + 2), transform.position.z
            );
        }
        if(keyUsed)
        {
            Destroy(GameObject.Find("Key"));
        }
    }
}
*/
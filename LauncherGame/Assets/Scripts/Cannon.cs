using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonball;
    public float fireRate;
    private float timer;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        // set the timer to the fire rate + the delay when the cannon first spawns
        timer = fireRate + delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0){
            timer -= Time.deltaTime;
        }
        //when the timer hits 0, shoot a cannonball and reset the timer
        else{
            Instantiate(cannonball, transform.position, transform.rotation);
            timer = fireRate;
        }
    }
}

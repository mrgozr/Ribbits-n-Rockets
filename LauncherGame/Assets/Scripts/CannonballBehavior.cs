using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Start()
    {
        //when spawned, start moving in the direction of the parent object
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 6;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when colliding with any object, destroy self
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{

    [SerializeField] Transform targetToFollow;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        /* transform.position is a call to change the camera's position. We move 3 variables on a Vector3(x,y,z)
        X and Z are locked in, Y is Mathf.Clamped to set a floor for the camera (to not show beneath the player). 
        The third variable can later be changed into a ceiling ((targetToFollow.position.y, 0f, [CEILING HERE]))*/
        transform.position = new Vector3(
            transform.position.x, Mathf.Clamp(targetToFollow.position.y, 0f, targetToFollow.position.y), transform.position.z);
    }
}

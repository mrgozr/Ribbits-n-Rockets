using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //Level 1 is index 3, 2 is index 4, etc.

        //BELOW CODE USED FOR CAMERA ONLY FOLLOW Y MOVEMENT
        if(SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            transform.position = new Vector3(
                transform.position.x, Mathf.Clamp(targetToFollow.position.y, 0f, 22.0f), transform.position.z
            );
        }
        //BELOW CODE USED FOR CAMERA ONLY FOLLOW X MOVEMENT
        if(SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            transform.position = new Vector3(
                Mathf.Clamp(targetToFollow.position.x, 0f, 134.0f), 
                transform.position.y, 
                transform.position.z
            );
        }
        //below code used for level 5 only
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            transform.position = new Vector3(
                transform.position.x, Mathf.Clamp(targetToFollow.position.y, 0f, 41.0f), transform.position.z
            );
        }
    }
}

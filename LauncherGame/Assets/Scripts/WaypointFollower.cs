using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    //create a list to hold all the waypoint gameobjects
    [SerializeField] private GameObject[] waypoints;
    //variable to hold the current waypoint to move towards
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        //if within 0.1 units of the current target waypoint, switch to the next waypoint in the list
        //if there is no next target waypoint, start over at the beginning of the list
        if(Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        //move towards the next target waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}

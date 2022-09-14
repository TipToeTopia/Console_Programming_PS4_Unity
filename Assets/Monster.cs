using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//i edited this code from the scifigametemplate
public class Monster: MonoBehaviour
{
    
    NavMeshAgent theAgent;//TheNavMesh
    public Transform[] patrolWayPoints; // An array of transforms for the patrol route
    private int wayPointIndex; // A counter for the way point array
    public float patrolSpeed = 2f; // Speed of walking Monster
    public float patrolWaitTime = 1f; // The amount of time to wait when the Monster has reached the patrol points
    public float patrolTimer; // A timer for the patrolWaitTime
    public GameObject Player;
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();// GetNavMeshAgent call it theAgent
        theAgent.autoBraking = false;
    }


    void Update()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < 2)//if the player is really close to the monster or less than 2 
        {
            GameManager.Instance.Dead(); //call function Dead from GameManager
        }
        
        theAgent.speed = patrolSpeed; // patrol speed

        if (theAgent.remainingDistance < theAgent.stoppingDistance) // if near the next waypoint or there is no destination...
        {
            patrolTimer = patrolTimer + Time.deltaTime; // timer begins

            if (patrolTimer >= patrolWaitTime) // If the timer greater or equal then  the wait time
            {
                if (wayPointIndex == patrolWayPoints.Length - 1) // if Monster at final destination
                {
                    wayPointIndex = 0; // initialised at 0 (reset once reached final waypoint reached)
                }
                else
                {
                    wayPointIndex++; // otherwise if not we cycle through the destination
                }
                patrolTimer = 0; // reset timer after each destination
            }
        }
        else
        {
            patrolTimer = 0; // if not near a destination, reset the timer.
        }
        theAgent.destination = patrolWayPoints[wayPointIndex].position; // set the destination to the patrolWayPoint
    }


}
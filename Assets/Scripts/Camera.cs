using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Vector3 Offset;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        //set camera to be at a certain position 
        Offset = Player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        //the camera follows the player
        transform.position = Player.position - Offset;
        
    }
}

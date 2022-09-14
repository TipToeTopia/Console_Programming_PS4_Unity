using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        // i edited this code from https://www.youtube.com/watch?v=HlDGSStxuHI&list=PLX2vGYjWbI0Q-s4_lX0h4i2zbZqlg4OfF&index=6&ab_channel=Unity
        //rotate the game object on z axis by 60
        transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);

    }

    public void Reset()
    {
        

            gameObject.SetActive(true);//make collectibles appear 
        
    }
}

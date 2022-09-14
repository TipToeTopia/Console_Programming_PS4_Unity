using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// i got this code from https://www.youtube.com/watch?v=rO19dA2jksk&feature=emb_title&ab_channel=Jayanam
public class Platform : MonoBehaviour
{
    public GameObject Player;//Player

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)//if the object is the player
        {
            Player.transform.parent = transform;//make the player stick to the platform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)//if the object is the player
        {
            Player.transform.parent = null;//lets the player not stick to the platform anymore
        }
    }
}

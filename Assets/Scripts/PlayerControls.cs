using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;
// I learned how to use the charactercontroller and I edited this code from https://www.youtube.com/watch?v=IstYXj_k4NA&list=PLiyfvmtjWC_V_H-VMGGAZi7n5E0gyhc37&index=2&ab_channel=gamesplusjames
public class PlayerControls : MonoBehaviour
{
    private Vector3 Moving;
    public CharacterController CC;
    public float Jump;
    public float Speed;
    public float GravityAmmount;
    public float Rotation;//rotation speed 
    public Animator Anim;
    Color m_LightbarColour;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the CharacterController
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //calls the Movement function into the Update
        Movement();


    }

    void Movement()
    {
        // if the player is on the ground allow the player to turn and set the y value of Moving and allow the player to only jump in they are on the ground
        if (CC.isGrounded)
        {
            Moving = new Vector3(0, 0, Input.GetAxis("leftstick1vertical") * Speed);// i edited this code and the line underneath from this video https://www.youtube.com/watch?v=e5g1nJcjz-M&feature=emb_title
            Moving = transform.TransformDirection(Moving);
            Moving.y = 0f;
            Anim.SetBool("isGrounded", true);//dont play jump animation if the player is grounded
            if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick1Button0", true)))// if jump is pressed(spacebar) then jump and play the jump animation because isGrounded bool is false
            {
                Anim.SetBool("isGrounded", false);
                Moving.y = Jump;
            }

        }
        Moving.y = Moving.y + Physics.gravity.y * GravityAmmount * Time.deltaTime;//gravity and GravityAmount effecting the movement of the player and allowing me to set how much gravity i want
        CC.Move(Moving * Time.deltaTime);//Smooth moving speed
        transform.Rotate(0,Input.GetAxis("rightstick1horizontal") * Rotation * Time.deltaTime,0);//how fast the player turns around
        Anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("leftstick1vertical"))));//play animation of player running
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //Call IncScore function from GameManager
            GameManager.Instance.IncScore();
            //if the player touches the collectible then make the collectible disappear
            other.gameObject.SetActive(false);

           // if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOnGamepad(0);

            StartCoroutine(vibratedelay());

        }
    }

   IEnumerator vibratedelay()
   {
       
        PS4Input.PadSetVibration(0, 230, 0);
        yield return new WaitForSeconds(0.2f);
        PS4Input.PadSetVibration(0, 0, 0);

    }

}

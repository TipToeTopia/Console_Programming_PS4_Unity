using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

#if UNITY_PS4
using UnityEngine.PS4;
#endif

// i edited this code from the tilesgame
public class GameManager : MonoBehaviour
{
    GameObject player;  // the player GameObject
    GameObject[] collectibles; // the tiles


    public float starttime; // time the game started
    public float resettime;

    public TextMeshProUGUI gameover_txt;    // Gameover txt object
    public TextMeshProUGUI timer_txt;       // Timer txt object
    public TextMeshProUGUI score_txt;       // Score txt object
    public TextMeshProUGUI lives_txt;       // Lives txt object
    Color m_LightbarColour;


    bool gameinprogess = true;  // Game in progress status

    public int lives = 3;   // number of lives
    public int score = 0;   // score
    int maxscore = 0;       // max score

    Vector3 startposition;  // player start position

    // Start is called before the first frame update
    void Start()
    {
        // Get the player GameObject

        player = GameObject.Find("Player");
        // Get the start position of the player, so that it can be put back here when a life is lost
        startposition =player.transform.position;

        // Set the gameover text to blank
        gameover_txt.text = "";
        // Set the lives text
        lives_txt.text = "Lives: " + lives;
        // Set the score text
        score_txt.text = "Score: " + score;
        starttime = Time.time;
        // Get the number of gameobjects with the Pick Up tag
        collectibles = GameObject.FindGameObjectsWithTag("Pick Up");
        maxscore = collectibles.Length;

    }

    public void Restart()
    {
        // reset lives and score
        lives = 3;
        score = 0;

        // Set the gameover text to blank
        gameover_txt.text = "";
        // Set the lives text
        lives_txt.text = "Lives: " + lives;
        // Set the score text
        score_txt.text = "Score: " + score;

        // reset player position
        ResetPlayerToStart();

        // reset collectibles
        foreach (GameObject collectibles in collectibles)
            collectibles.GetComponent<PickUp>().Reset();

        // Start
        resettime = Time.time - starttime; //reset the time to 0

        // game in progress
        gameinprogess = true;
    }

    // Main game loop
    void Update()
    {
        if (score == 10 && lives > 0)
        {


            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.green, Time.deltaTime * 4f);

            PS4Input.PadSetLightBar(0,
                                Mathf.RoundToInt(m_LightbarColour.r * 255),
                                Mathf.RoundToInt(m_LightbarColour.g * 255),
                                Mathf.RoundToInt(m_LightbarColour.b * 255));
        }
        else if (lives > 0)
        {
            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.blue, Time.deltaTime * 4f);

            PS4Input.PadSetLightBar(0,
                                Mathf.RoundToInt(m_LightbarColour.r * 255),
                                Mathf.RoundToInt(m_LightbarColour.g * 255),
                                Mathf.RoundToInt(m_LightbarColour.b * 255));
        }
        else if (lives <= 0)
        {
            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.red, Time.deltaTime * 4f);

            PS4Input.PadSetLightBar(0,
                                Mathf.RoundToInt(m_LightbarColour.r * 255),
                                Mathf.RoundToInt(m_LightbarColour.g * 255),
                                Mathf.RoundToInt(m_LightbarColour.b * 255));

            
        }
        

            // If gameover, nothing to do
            if (!gameinprogess)
            {
            if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick1Button3", true)))
            {
                Restart();
                
            }

            return;
        }

        // Set the timer text to the current game time
        timer_txt.text = "Time: " + Time.time;

        // If the player y position is less than 2
        // Decrement the lives by one
        // Otherwise move the player back to the start position
        if (player.transform.position.y < 2)
        {
          Dead();
        }


    }

    // Set the player back to the start position on lose a live
    void ResetPlayerToStart()
    {
        player.transform.position = startposition;

       
        //Debug.Log("true");
        //m_LightbarColour = Color.Lerp(m_LightbarColour, Color.blue, Time.deltaTime * 4f);

        //PS4Input.PadSetLightBar(1,
        //athf.RoundToInt(m_LightbarColour.r * 255),
        //Mathf.RoundToInt(m_LightbarColour.g * 255),
        //Mathf.RoundToInt(m_LightbarColour.b * 255));
    }

    // Increase the score
    public void IncScore()
    {



        score = score + 1;
        score_txt.text = "Score: " + score;

        if (score == maxscore)
        {
            gameinprogess = false;
            gameover_txt.text = "YOU WIN - Triangle To Restart";//gameover text to "YOU WIN - R TO RESTART"
           
            return;

        }

    }
    public void Dead()//How the player can lose and what to do when the player has lost
    {
        // Display the current number of lives
        lives = lives - 1;
        lives_txt.text = "Lives: " + lives;
        // If the number of lives is zero
        if (lives == 0)
        {
            gameinprogess = false;// Set the game in progress status to false
            gameover_txt.text = "YOU LOSE - TRIANGLE TO RESTART";//gameover text to "YOU LOSE - R TO RESTART"

            StartCoroutine(vibratedelay());      
            return;
            

        }
        else
        {
            ResetPlayerToStart();
        }

    }
    // Set up the game instance singleton
    void Awake()
    {
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // This is a C# property - the code below isn't using it
    // as it is accessing the private static instance directly.
    // Use this property from other classes.
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;

    IEnumerator vibratedelay()
    {

        PS4Input.PadSetVibration(0, 255, 0);
        yield return new WaitForSeconds(0.5f);
        PS4Input.PadSetVibration(0, 0, 0);

    }
}


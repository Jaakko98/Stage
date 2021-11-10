using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //VARIABLES
    public float time = 0f;
    public int checkpointsReached;
    private int checkpoints;
    private GameObject[] checkpointObjects;
    private bool gameOngoing = true;
    private Vector3 playerStartpos;

    //REFERENCES
    public TextMeshProUGUI timer;
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI endtimer;
    public GameObject player;
    public GameObject startposition;
    public GameObject endgamecanvas;


    //METHODS
    public void CheckpointReached()
    {
        checkpointsReached += 1;
        if(checkpointsReached <= checkpoints)
        {
            gameOngoing = false;
            
            if(PlayerPrefs.GetFloat("Highscore") == 0 || time < PlayerPrefs.GetFloat("Highscore"))
            {
                PlayerPrefs.SetFloat("Highscore", time);
                
            }
            highscore.text = "Highscore: " + FormatTime(PlayerPrefs.GetFloat("Highscore"));
            endtimer.text = "Time: " + FormatTime(time);
            endgamecanvas.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("Game ended");
            //end game
        }
    }
    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /* old restart
        time = 0f;
        Debug.Log("position" + player.transform.position);
        Debug.Log("startpos" + playerStartpos);
        //player.transform.position = playerStartpos;
        //player.transform.position = startposition.transform.position;
        checkpointsReached = 0;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameOngoing = true;
        foreach(GameObject checkpoint in checkpointObjects)
        {
            checkpoint.tag = "CheckPoint";
        }
        Debug.Log("restarted");
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        startposition = GameObject.Find("StartPosition");
        playerStartpos = player.transform.position;
        checkpointObjects = GameObject.FindGameObjectsWithTag("CheckPoint");
        checkpoints = checkpointObjects.Length;
        Debug.Log(PlayerPrefs.GetFloat("Highscore"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (!gameOngoing)
        {
            return;
        }
        time += Time.deltaTime;
        timer.text = "Time: " + FormatTime(time);
    }
}

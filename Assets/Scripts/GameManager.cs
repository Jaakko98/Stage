using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private GameObject player;

    //METHODS
    public void CheckpointReached()
    {
        checkpointsReached += 1;
        if(checkpointsReached <= checkpoints)
        {
            gameOngoing = false;
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
        time = 0f;
        Debug.Log("position" + player.transform.position);
        Debug.Log("startpos" + playerStartpos);
        player.transform.position = playerStartpos;
        checkpointsReached = 0;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameOngoing = true;
        foreach(GameObject checkpoint in checkpointObjects)
        {
            checkpoint.tag = "CheckPoint";
        }
        Debug.Log("restarted");
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerStartpos = player.transform.position;
        checkpointObjects = GameObject.FindGameObjectsWithTag("CheckPoint");
        checkpoints = checkpointObjects.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOngoing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
            return;
        }
        time += Time.deltaTime;
        timer.text = "Time: " + FormatTime(time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //REFERENCES

    public GameObject destination;
    public GameObject player;
    public Vector3 telePos;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleported");
        Debug.Log("Destination " + destination.transform.position);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = destination.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}

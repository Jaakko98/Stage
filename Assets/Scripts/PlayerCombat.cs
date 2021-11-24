using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //REFERENCES

    private Health playerHealth;
    private Health enemyHealth;
    private GameObject touchedObject;
    

    //METHODS

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy" && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("hit enemy");
            enemyHealth = other.GetComponent<Health>();
            enemyHealth.decreaseHealth(20);
            Debug.Log("enemy health is " + enemyHealth.getHealth());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

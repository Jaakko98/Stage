using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //VARIABLES

    private float attackCooldown = 0f;
    public float attackSpeed = 1f;

    //REFERENCES

    private Health playerHealth;
    private Health enemyHealth;
    private GameObject touchedObject;
    private GameObject model;
    private Animator anim;
    

    //METHODS

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy" && Input.GetButtonDown("Fire1") && attackCooldown <= 0f)
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
        model = GameObject.Find("model");
        anim = model.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && attackCooldown <= 0f)
        {
            attackCooldown = 1 / attackSpeed;
            anim.SetTrigger("Attack");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //REFERENCES
    

    //VARIABLES
    private int health = 100;
    private int maxHealth = 100;
    private bool isAlive = true;

    public void increaseHealth(int amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void decreaseHealth(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            health = 0;
            Debug.Log(gameObject.name + " Health is 0");
            isAlive = false;
        }
    }
    public bool IsAlive()
    {
        return isAlive;
    }

    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 100;
    private int maxHealth = 100;

    public void increaseHealth(int amount)
    {
        health += amount;
    }
    public void decreaseHealth(int amount)
    {
        health -= amount;
        if(health < 0)
        {
            health = 0;
            Debug.Log(gameObject.name + " Health is 0");
            Destroy(gameObject);
        }
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
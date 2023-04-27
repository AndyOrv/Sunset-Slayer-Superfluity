using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-- Author: Andrew Orvis
-- Description: 
 */


public class Health : MonoBehaviour
{
    [Header("Health")]
    public float maxhealth;
    private float health;//make get and set



    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        Debug.Log("Current Health: " + health);
    }

    /*
    public Slider slider; // Reference to the slider component

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
     
     */
}

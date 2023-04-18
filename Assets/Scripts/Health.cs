using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ChangeHealth(float amount)
    {
        health += amount;
        Debug.Log("Current Health: " + health);
    }
}
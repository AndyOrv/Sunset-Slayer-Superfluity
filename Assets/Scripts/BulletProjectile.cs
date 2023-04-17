using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public bool destory; // add get set
    public bool ignorePlayer = false;

    private Rigidbody rb;
    private float dmg = 10;//add get and set

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if object has a health script aka is alive
        if (other.GetComponent<Health>() != null)
        {
            other.GetComponent<Health>().ChangeHealth(dmg);
        }

        if(destory)
            Destroy(gameObject);
    }
}

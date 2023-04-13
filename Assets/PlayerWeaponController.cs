using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] RayCastWeapon gun;


    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.StartFiring();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            gun.StopFiring();
        }
    }
}

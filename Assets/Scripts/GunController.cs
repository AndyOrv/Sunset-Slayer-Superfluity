using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] RayCastWeapon gun;
    [SerializeField] bool ignorePlayer;
    [SerializeField] bool playerControlled;

    [Header("Auto Fire commands")]
    public bool fireWeapon;

    private void LateUpdate()
    {
        if (playerControlled)
        {
            PlayerFire();
        }
        else
        {
            AutoFire();
        }
    }

    private void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.StartFiring();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            gun.StopFiring();
        }
    }
    private void AutoFire()
    {
        if (fireWeapon)
        {
            gun.StartFiring();
            fireWeapon = false;
        }
        if (!fireWeapon)
        {
            gun.StopFiring();
        }
    }
}

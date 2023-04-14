using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{
    public bool isFiring = false;

    [Header("Particle Systems")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    [Header("Ray Cast Components")]
    public Transform rayOrigin;
    public Transform target;

    Ray ray;
    RaycastHit hitInfo;
    public LayerMask IgnoreMe;

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.Emit(1);

        ray.origin = rayOrigin.position;
        ray.direction = target.position - rayOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if(Physics.Raycast(ray, out hitInfo, 1000f, ~IgnoreMe))
        {       
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }
    public void StopFiring()
    {
        isFiring = false;
    }
}

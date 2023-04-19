using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{
    [Header("Weapon stats")]
    [SerializeField] float damage;

    [Header("Particle Systems")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    [Header("Ray Cast Components")]
    public Transform rayOrigin;
    public Transform target;
    public bool fireForward;
    public bool isFiring = false;

    Ray ray;
    RaycastHit hitInfo;
    public LayerMask IgnoreMe;

    private void Start()
    {
        //Get bullet projectile component of bullet -> set ignoreplayer to = ignoreplayer
    }

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.Emit(1);

        ray.origin = rayOrigin.position;
        if(fireForward)
            ray.direction = rayOrigin.forward;
        else
            ray.direction = target.position - rayOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if(Physics.Raycast(ray, out hitInfo, 1000f, ~IgnoreMe)) //this is preventing bullets from flying away
        {       
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;

            doDamage(hitInfo, -damage);
        }
    }

    private void doDamage(RaycastHit target, float damage)
    {
        Health heth = target.collider.GetComponent<Health>();
        if (heth != null)
        {
            heth.ChangeHealth(damage);
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
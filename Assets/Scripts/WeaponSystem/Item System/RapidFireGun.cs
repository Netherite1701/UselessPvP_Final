using System.Collections;
using UnityEngine;

public class RapidFireGun : Gun
{
    [SerializeField] Camera cam;
    [SerializeField] float fireRate = 5f;
    public override void Use()
    {
        Fire();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
        }
    }


    public void StartFiring()
    {
        if (fireRate <= 0f)
            Shoot();
        else
            InvokeRepeating("Shoot", 0f, 1f / fireRate);
    }

    public void StopFiring()
    {
        CancelInvoke("Shoot");
    }
    public void Fire()
    {

    }
}

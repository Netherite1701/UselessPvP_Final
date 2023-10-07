using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;
    [SerializeField] PhotonView pv;
    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Debug.Log(gameObject.name);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            //Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage, pv.ViewID);
        }
    }
}

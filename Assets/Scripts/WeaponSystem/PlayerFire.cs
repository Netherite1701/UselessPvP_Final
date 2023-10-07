using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFire : MonoBehaviour
{
    public GunScript gunScript;
    string currentWeapon;
    [SerializeField] PhotonView PV;
    [SerializeField] PlayerLook pauseMenu;
    [SerializeField] Transform cameraTransform;

    [SerializeField] PlayerClass playerClass;
    string className;
    //  ((GunInfo)gunScript.itemInfo)

    private void Start()
    {
        if (!PV.IsMine)
        {
            this.enabled = false;
        }
        className = playerClass.ReturnClassName();
    }

    public void StartFiring()
    {
        if (gunScript.isReloading == false && gunScript.firing == false && pauseMenu.escape == false)
        {
            if (((GunInfo)gunScript.itemInfo).fireRate <= 0f)
                StartCoroutine(gunScript.ShootSingle());
            else
            {
                InvokeRepeating("Shoot", 0f, 1f / ((GunInfo)gunScript.itemInfo).fireRate);
            }
        }
    }

    public void StopFiring()
    {
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        gunScript.Shoot();
    }

    private void UpdateRecoil()
    {
          gameObject.transform.rotation = cameraTransform.rotation;
    }

    private void Update()
    {
        if (gunScript == null)
            return;
        if (((GunInfo)gunScript.itemInfo).itemName != currentWeapon || pauseMenu.escape == true)
        {
            StopFiring();
        }
        currentWeapon = ((GunInfo)gunScript.itemInfo).itemName;
        if (Input.GetKeyDown(KeyCode.R) && (className != "Sniper"))
        {
            StopFiring();
            gunScript.Reload();
        }
        UpdateRecoil();
    }
}

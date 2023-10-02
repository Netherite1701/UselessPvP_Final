using Photon.Pun;
using System.Collections;
using UnityEngine;

public class GunScript : Gun
{
    public Variables var;
    int loadedAmmoCount;
    [SerializeField]
    public int ammoCount = 180;
    int maxAmmo;
    public bool isReloading = false;
    public bool firing = false;
    //string _currentWeapon;
    //public bool firing = false;
    //  ((GunInfo)itemInfo)
    public override void Use()
    {
        //Fire();
    }
    private void Start()
    {
        maxAmmo = ((GunInfo)itemInfo).maxAmmo;
        loadedAmmoCount = maxAmmo;
        //_currentWeapon = ((GunInfo)itemInfo).itemName;
    }

    public void Shoot()
    {
        //if (((GunInfo)itemInfo).itemName == _currentWeapon)
        //{
        Debug.Log("1");
        if (loadedAmmoCount <= 0 && ammoCount > 0)
        {
            Reload();
        }
        else if (isReloading == false)
        {
            if (loadedAmmoCount <= 0)
            {
                return;
            }
            if (((GunInfo)itemInfo).isShotgun)
            {
                loadedAmmoCount -= 1;
                for (int i = 0; i < ((GunInfo)itemInfo).bulletsPerShot; i++)
                {

                    Ray ray = var.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                    ray.origin = var.cam.transform.position;
                    RaycastHit hit;
                    if (Physics.Raycast(var.camHolder.transform.position, GetShootingDir(), out hit, ((GunInfo)itemInfo).range))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            var.hitMarker.SetActive(true);
                            StartCoroutine("ShowHitMarker");
                            //UpdateUI();
                        }
                    }
                }
                var.PlayGunSFX(((GunInfo)itemInfo).gunIndex);
                var.CallRPC(((GunInfo)itemInfo).gunIndex);
                var.weaponRecoil.Fire();
            }
            else
            {
                //Debug.Log("zaq");
                loadedAmmoCount -= 1;
                Ray ray = var.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                ray.origin = var.cam.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, ((GunInfo)itemInfo).range))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        var.hitMarker.SetActive(true);
                        StartCoroutine("ShowHitMarker");
                        //UpdateUI();
                    }
                }
                var.PlayGunSFX(((GunInfo)itemInfo).gunIndex);
                var.CallRPC(((GunInfo)itemInfo).gunIndex);
                var.weaponRecoil.Fire();
            }
        }
    }

    public IEnumerator ShootSingle()
    {
        firing = true;
        if (loadedAmmoCount <= 0)
        {
            Reload();
        }
        else if (isReloading == false)
        {
            if (((GunInfo)itemInfo).isShotgun)
            {
                loadedAmmoCount -= 1;
                for (int i = 0; i < ((GunInfo)itemInfo).bulletsPerShot; i++)
                {

                    Ray ray = var.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                    ray.origin = var.cam.transform.position;
                    RaycastHit hit;
                    if (Physics.Raycast(var.camHolder.transform.position, GetShootingDir(), out hit, ((GunInfo)itemInfo).range))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            var.hitMarker.SetActive(true);
                            StartCoroutine("ShowHitMarker");
                            //UpdateUI();
                        }
                    }
                }
                var.PlayGunSFX(((GunInfo)itemInfo).gunIndex);
                var.CallRPC(((GunInfo)itemInfo).gunIndex);
                var.weaponRecoil.Fire();
            }
            else
            {
                loadedAmmoCount -= 1;
                Ray ray = var.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                ray.origin = var.cam.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, ((GunInfo)itemInfo).range))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        var.hitMarker.SetActive(true);
                        StartCoroutine("ShowHitMarker");
                        //UpdateUI();
                    }
                }
                var.PlayGunSFX(((GunInfo)itemInfo).gunIndex);
                var.CallRPC(((GunInfo)itemInfo).gunIndex);
                var.weaponRecoil.Fire();
            }
        }
        yield return new WaitForSeconds(Random.Range((((GunInfo)itemInfo).shootCooldownMin), ((GunInfo)itemInfo).shootCooldownMax));
        firing = false;
    }

    //public void StartFiring()
    //{
    //    if (((GunInfo)itemInfo).fireRate <= 0f)
    //        Shoot();
    //    else
    //    {
    //        InvokeRepeating("Shoot", 0f, 1f / ((GunInfo)itemInfo).fireRate);
    //        //firing = true;
    //    }

    //}

    //public void StopFiring()
    //{
    //    CancelInvoke("Shoot");
    //}

    public IEnumerator ShowHitMarker()
    {
        yield return new WaitForSeconds(((GunInfo)itemInfo).showtime);
        var.hitMarker.SetActive(false);
    }

    public void Reload()
    {
        if (var.className == "Sniper" && var.playerMovement.itemindex == 2)
        {
            var.aimingScript.ZoomOut();
            StartCoroutine(var.abilityScript.UnequipAbilityCoroutine());
            ReloadInstant();
            return;
        }
        if (ammoCount > 0)
        {
            isReloading = true;
            StartCoroutine("ReloadCoroutine");
        }


    }
    public IEnumerator ReloadCoroutine()
    {
        var.animator.SetBool("Aiming", false);
        var.animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(((GunInfo)itemInfo).reloadTime - .25f);
        var.animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        if (ammoCount - (maxAmmo - loadedAmmoCount) >= 0)
        {
            ammoCount -= (maxAmmo - loadedAmmoCount);
            loadedAmmoCount = maxAmmo;
        }
        else
        {
            loadedAmmoCount += ammoCount;
            ammoCount = 0;
        }
        isReloading = false;
    }

    public void ReloadInstant()
    {
        if (ammoCount - (maxAmmo - loadedAmmoCount) >= 0)
        {
            ammoCount -= (maxAmmo - loadedAmmoCount);
            loadedAmmoCount = maxAmmo;
        }
        else
        {
            loadedAmmoCount += ammoCount;
            ammoCount = 0;
        }
        isReloading = false;
    }

    public void UpdateUI()
    {
        var.ammoCountUI.text = loadedAmmoCount + " / " + ammoCount;
    }

    Vector3 GetShootingDir()
    {
        Vector3 targetPos = var.camHolder.gameObject.transform.position + var.camHolder.gameObject.transform.forward * ((GunInfo)itemInfo).range;

        targetPos = new Vector3(
            targetPos.x + Random.Range(-((GunInfo)itemInfo).inaccuracyDist, ((GunInfo)itemInfo).inaccuracyDist),
            targetPos.y + Random.Range(-((GunInfo)itemInfo).inaccuracyDist, ((GunInfo)itemInfo).inaccuracyDist),
            targetPos.z + Random.Range(-((GunInfo)itemInfo).inaccuracyDist, ((GunInfo)itemInfo).inaccuracyDist)
        );

        Vector3 dir = targetPos - var.camHolder.gameObject.transform.position;
        return dir.normalized;
    }


}
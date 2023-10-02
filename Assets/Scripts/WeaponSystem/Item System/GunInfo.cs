using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="FPS/New Gun")]
public class GunInfo : ItemInfo
{
    [Header("General")]
    public float damage;
    public float fireRate = 0f;
    public float showtime = 0.1f;
    public int maxAmmo;
    public float reloadTime;
    public float range=100f;
    public int zoomFOV = 60;
    public float shootCooldownMin;
    public float shootCooldownMax;
    public AudioClip gunSFX;
    public int gunIndex;
    [Header("Recoil")]
    public Vector3 RecoilRot;
    public Vector3 RecoilRotAiming;
    [Header("Shotgun")]
    public bool isShotgun = false;
    public int bulletsPerShot = 6;
    public float inaccuracyDist = 5f;
    [Header("SR")]
    public bool isSR;
}

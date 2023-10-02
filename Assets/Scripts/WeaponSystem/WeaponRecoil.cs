using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] AimingScript aimingScript;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] WallRun wallRun;
    //[HideInInspector] 
    public ItemInfo itemInfo;

    [Header("Recoil Settings")]
    public float rotSpeed = 6;
    public float retSpeed = 25;

    [Header("Hipfire")]
    public Vector3 RecoilRot = new Vector3(2f, 2f, 2f);

    [Header("Aiming")]
    public Vector3 RecoilRotAiming = new Vector3(0.5f, 0.5f, 1.5f);

    [Header("State")]
    public bool aiming;

    private Vector3 currentRot;
    private Vector3 Rot;

    private void Start()
    {
        StartCoroutine(LateStart(0.01f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UpdateRecoil();
    }
    private void FixedUpdate()
    {
        currentRot = Vector3.Lerp(currentRot, Vector3.zero, retSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, currentRot, rotSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Rot);
    }

    public void UpdateRecoil()
    {
        RecoilRot = ((GunInfo)itemInfo).RecoilRot;
        RecoilRotAiming = ((GunInfo)itemInfo).RecoilRotAiming;
    }

    private void Update()
    {
        aiming = aimingScript.aiming;
    }
    public void Fire()
    {
        if (aiming)
        {
            if (wallRun.wallRunning == true)
                currentRot += new Vector3(-RecoilRotAiming.x / 2, Random.Range(-RecoilRotAiming.y / 2, RecoilRotAiming.y / 2), Random.Range(-RecoilRotAiming.z / 2, RecoilRotAiming.z / 2));
            else
                currentRot += new Vector3(-RecoilRotAiming.x, Random.Range(-RecoilRotAiming.y, RecoilRotAiming.y), Random.Range(-RecoilRotAiming.z, RecoilRotAiming.z));
        }
        else
        {
            if (wallRun.wallRunning == true)
                currentRot += new Vector3(-RecoilRotAiming.x / 2, Random.Range(-RecoilRotAiming.y / 2, RecoilRotAiming.y / 2), Random.Range(-RecoilRotAiming.z / 2, RecoilRotAiming.z / 2));
            else
                currentRot += new Vector3(-RecoilRot.x, Random.Range(-RecoilRot.y, RecoilRot.y), Random.Range(-RecoilRot.z, RecoilRot.z));
        }
    }
}

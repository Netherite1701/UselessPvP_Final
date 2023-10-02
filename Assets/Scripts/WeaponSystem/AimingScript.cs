using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AimingScript : MonoBehaviour
{

    public GameObject Gun;

    public bool aiming;
    public Camera cam;
    [SerializeField] int aimFOV;
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement;
    public GunScript gunScript;
    public ItemInfo itemInfo;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        animator = Gun.GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            animator.enabled = false;
            this.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gunScript == null)
            return;
        if (gunScript.isReloading)
            ZoomOut();

        if (Input.GetMouseButtonDown(1))
        {
            ZoomIn();
        }

        if (Input.GetMouseButtonUp(1))
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        //if (((GunInfo)itemInfo).isSR)
        //{
            playerMovement.SR.SetActive(false);
        //}
        animator.SetBool("Aiming", true);
        aiming = true;
        cam.fieldOfView = ((GunInfo)itemInfo).zoomFOV;

    }

    public void ZoomOut()
    {
        //if (((GunInfo)itemInfo).isSR)
        //{
            playerMovement.SR.SetActive(true);
        //}
        //Gun.SetActive(true);
        animator.SetBool("Aiming", false);
        aiming = false;
        cam.fieldOfView = 90;

    }
}
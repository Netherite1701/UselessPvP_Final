using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.Audio;

public class Variables : MonoBehaviour
{
    public Camera cam;
    public GameObject camHolder;
    public AimingScript aimingScript;
    public WeaponRecoil weaponRecoil;
    public GameObject hitMarker;
    public Animator animator;
    public TMP_Text ammoCountUI;
    public AudioSource audioSource;
    public AudioClip[] gunSFX;
    public PlayerClass playerClass;
    [HideInInspector]
    public string className;
    public PlayerMovement playerMovement;
    PhotonView photonView;
    public Abilities abilities_;
    public AbilityScript abilityScript;
    public GameObject healGO;


    private void Start()
    {
        photonView= GetComponent<PhotonView>();
        className = playerClass.ReturnClassName();
    }
    public void CallRPC(int index)
    {
        photonView.RPC("PlayGunSFXRPC", RpcTarget.Others, index);
    }

    [PunRPC]
    public void PlayGunSFXRPC(int index)
    {
        PlayGunSFX(index);
    }

    public void PlayGunSFX(int index)
    {
        audioSource.PlayOneShot(gunSFX[index]);
    }
}

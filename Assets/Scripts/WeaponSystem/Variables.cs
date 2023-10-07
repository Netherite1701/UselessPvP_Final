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
    public MonoBehaviour[] scriptsToDisable;
    public GameObject[] GOToDisable;
    public TMP_Text playerCountText;
    public TMP_Text timeText;
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
        if (!photonView.IsMine)
        {
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                script.enabled = false;
            }
        }
        else {
            foreach (GameObject obj in GOToDisable)
            {
                obj.SetActive(false);
            }
        }
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

    public void Update()
    {
        playerCountText.text = GameManager.Instance.playerCount.ToString();
        timeText.text = GameManager.Instance.time.ToString();
    }
}

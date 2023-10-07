using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class PlayerLook : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] WallRun wallRun;
    [SerializeField] GunScript gunScript;

    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] Transform cam = null;
    [SerializeField] GameObject cameraPos;
    [SerializeField] GameObject weaponObj;
    [SerializeField] Transform orientation = null;
    [SerializeField] GameObject Visual;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] Camera camera_;

    Animator animator1,animator2;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    public bool escape;

    PhotonView PV;

    Scene start;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        animator1 = cameraPos.GetComponent<Animator>();
        animator2 = weaponObj.GetComponent<Animator>(); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!PV.IsMine)
        {
            Debug.Log("df");
            Destroy(camera_.gameObject);
            this.enabled = false;
        }
        else
        {
            Visual.SetActive(false);
        }
        sensX = RoomManager.instance.xSens;
        sensY = RoomManager.instance.ySens;
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
         
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escape = !escape;
            if(escape == true)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if(escape == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = escape;
            pauseMenu.SetActive(escape);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            print(1);
            animator1.SetBool("LeanRight", false);
            animator1.SetBool("LeanLeft", true);
            animator2.SetBool("LeanRight", false);
            animator2.SetBool("LeanLeft", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            print(1);
            animator1.SetBool("LeanLeft", false);
            animator2.SetBool("LeanLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(1);
            animator1.SetBool("LeanLeft", false);
            animator1.SetBool("LeanRight", true);
            animator2.SetBool("LeanLeft", false);
            animator2.SetBool("LeanRight", true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            print(1);
            animator1.SetBool("LeanRight", false);
            animator2.SetBool("LeanRight", false);
        }
    }

    //public void Disconnect() {
    //    Destroy(RoomManager.instance.gameObject);
    //    StartCoroutine(DisconnectAndLoad());
    //}

    //IEnumerator DisconnectAndLoad()
    //{
    //    //PhotonNetwork.Disconnect()
    //    PhotonNetwork.LeaveRoom();
    //    while (PhotonNetwork.InRoom)
    //        yield return null;
    //    SceneManager.LoadScene("Lobby");
    //    //PhotonNetwork.LoadLevel(0);
    //}

    public void LeaveRoom()
    {
        Destroy(RoomManager.instance.gameObject);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
        //StartCoroutine(WaitForDisconnect());
    }

    //private IEnumerator WaitForDisconnect()
    //{
    //    PhotonNetwork.Disconnect();
    //    //while (PhotonNetwork.IsConnected)
    //    //{
    //    //    Debug.Log("In loop");
    //    //    yield return null;
    //    //}
    //    PhotonNetwork.LoadLevel(0);
    //}

    //public override void OnLeftRoom()
    //{
    //    Debug.Log("sadfsdfa");
    //    PhotonNetwork.LoadLevel(0);
    //}
    
}

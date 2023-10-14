using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    float maxInteractionDistance = 5;
    [SerializeField]
    LayerMask interactionMask;

    PlayerMovement playerMovement;

    int index;
    [SerializeField]
    GameObject[] gunObjects;

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerMovement = GetComponent<PlayerMovement>();
        gunObjects = playerMovement.itemGO;
        index = playerMovement.itemindex;
        if (!photonView.IsMine)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        index = playerMovement.itemindex;
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit, maxInteractionDistance, interactionMask)){
                Debug.Log("Detected");
                StartCoroutine(hit.transform.GetComponent<InteractionObject>().Interact(gunObjects[index]));
                //StartCoroutine(CooldownCoroutine());
            }

        }
    }
}

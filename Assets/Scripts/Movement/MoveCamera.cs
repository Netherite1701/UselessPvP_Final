using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveCamera : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    [SerializeField] Transform cameraPosition = null;
    [SerializeField] bool pvenabled = false;
    private void Start()
    {
        if (pvenabled)
        {
            PV = GetComponent<PhotonView>();
            if (!PV.IsMine)
                this.enabled = false;
        }

    }
    void Update()
    {
        transform.rotation = cameraPosition.rotation;
    }
}

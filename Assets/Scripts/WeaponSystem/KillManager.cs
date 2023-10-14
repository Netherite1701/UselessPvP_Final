using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KillManager : MonoBehaviour
{
    public int killCount = 0;
    public PhotonView photonView;

    [PunRPC]
    public void IncrementKillCount(int id)
    {
        Debug.Log("Recieved RPC");
        if (photonView.ViewID == id)
        {
            killCount++;
            photonView.RPC("UpdateKillCount", RpcTarget.AllBuffered, killCount);
        }
    }

    [PunRPC]
    private void UpdateKillCount(int newKillCount)
    {
        killCount = newKillCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;

public enum ClassType 
{
    Sniper,//0
    Healer,//1
    Tanker,//2
    Landmineist,//3
    Phaser
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<ClassData> classDatas;
    PhotonView PV;
    int killCount;
    GameObject controller;
    int classIndex = 0;
    public bool alive = true;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        classIndex = RoomManager.instance.classIndex;
    }

    void Start()
    {
        if (PV.IsMine)
        {
            SpawnPlayer((ClassType)classIndex);
        }
    }

    // public PlayerClass SpawnPlayer(ClassType type)
    // {
    //     var playerPrefab = Path.Combine("PhotonPrefabs","PlayerClasses", type.ToString());
    //     Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
    //     var newPlayer = PhotonNetwork.Instantiate(playerPrefab,spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID }).GetComponent<PlayerClass>();
    //     newPlayer.ClassData = classDatas[(int)type];
    //     //return newZombie;
    // }

    public void SpawnPlayer(ClassType type)
    {
        var playerPrefab = Path.Combine("PhotonPrefabs","PlayerClasses", type.ToString());
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        controller = PhotonNetwork.Instantiate(playerPrefab,spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
    }

    // void CreateController(GameObject playerPrefab)
    // {
    //     Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
    //     controller = PhotonNetwork.Instantiate(playerPrefab, spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
    // }

    public void Die()
    {
        CallRPC();
        StartCoroutine(Respawn());        
    }

    IEnumerator Respawn()
    {
        CameraManager.Instance.toggleAlive();
        alive = false;
        controller.GetComponent<Variables>().abilityScript.abilities.UnequipItem();
        PhotonNetwork.Destroy(controller);
        yield return new WaitForSeconds(15f);
        SpawnPlayer((ClassType)classIndex);
        alive = true;
        CameraManager.Instance.toggleAlive();
    }

    public void CallRPC()
    {
        PV.RPC("UpdatePlayerCount", RpcTarget.All);
    }

    [PunRPC]
    public void UpdatePlayerCount()
    {
        //Debug.Log("UpdatePlayerCount");
        GameManager.Instance.playerCount -= 1;
        //Debug.Log(GameManager.Instance.playerCount);
    }
}
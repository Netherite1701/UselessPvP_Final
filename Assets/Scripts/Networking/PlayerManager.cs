using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;

public enum ClassType 
{
    Sniper,Healer,Tanker,Landmineist,Phaser,PushPull
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<ClassData> classDatas;
    PhotonView PV;
    int killCount;
    GameObject controller;
    int classIndex = 0;
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
        PhotonNetwork.Destroy(controller);
        SpawnPlayer((ClassType)classIndex);
    }

}
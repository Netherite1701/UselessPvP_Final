using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public float xSens = 100f;
    public float ySens = 100f;
    public string temp;
    public int classIndex;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        //if(scene.buildIndex == 1)
        //{
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"),Vector3.zero,Quaternion.identity);
        //}
    }

    #region Sensitivity
    // public void SetXSens(float value)
    // {
    //     xSens = value;
    // }
    public void SetYSens(float value)
    {
        ySens = value;
    }

    public void GetXStr(string str)
    {
        xSens = int.Parse(str);
    }

    public void GetYStr(string str)
    {
        ySens = int.Parse(str);
    }
    #endregion

    public void GetClassIndex(string index)
    {
        classIndex = int.Parse(index);
    }

    public void GetClassIndexInt(int index)
    {
        classIndex = index;
    }

    public void CallRPC(int p)
    {
        GetComponent<PhotonView>().RPC("SyncVars", RpcTarget.Others, p);
    }

    [PunRPC]
    public void SyncVars(int p)
    {
        GameManager.Instance.playerCount = p;
    }
}

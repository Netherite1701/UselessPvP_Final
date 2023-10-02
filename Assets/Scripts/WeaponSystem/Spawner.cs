using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Spawner : MonoBehaviour
{
    GameObject[] HealObjList;
    int index = 0;
    [SerializeField] GameObject HealObj;
    //void Start()
    //{
    //    StartCoroutine(SpawnHeal());
    //}

    IEnumerator SpawnHeal()
    {
        
        index++;
        HealObjList[index] =  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "HealObj"), new Vector3(Random.Range(-10,10),0.75f, Random.Range(-10, 10)),new Quaternion(0,0,0,0));
        if (HealObjList.Length > 9)
            yield return null;
        yield return new WaitForSeconds(Random.Range(3, 9));
    }
}

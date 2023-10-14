using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float time = 0;
    public int playerCount = 0;

    public bool started = false;


    public int score;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Update()
    {
        if (started)
        {
            time += Time.deltaTime;
            if (playerCount <= 1)
            {
                PhotonNetwork.LoadLevel(4);
                started = false;
            }
        }
    }

    public void resetTimer()
    {
        time = 0;
    }
}

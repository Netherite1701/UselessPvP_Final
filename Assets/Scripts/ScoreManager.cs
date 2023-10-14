using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [HideInInspector]
    public int score;

    public TMP_Text scoreText;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        score = GameManager.Instance.score + 1;
        updateScoreBoard();
    }

    void updateScoreBoard()
    {
        scoreText.text = "You Are #" + score.ToString() + "!";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Leave()
    {
        GameManager.Instance.started = false;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
        MenuManager.instance.OpenMenu("Loading");
    }

}

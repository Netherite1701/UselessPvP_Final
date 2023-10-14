using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    private int currentCameraIndex = 0;
    public GameObject[] cameras;
    public bool alive = true;

    void Awake()
    {
        Instance = this;
        foreach (GameObject cam in cameras) {
            cam.SetActive(false);
        }
        cameras[0].SetActive(true);
    }

    private void Update()
    {
        if (!alive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentCameraIndex++;
                if (currentCameraIndex < cameras.Length)
                {
                    cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                    cameras[currentCameraIndex].gameObject.SetActive(true);
                }
                else
                {
                    cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                    currentCameraIndex = 0;
                    cameras[currentCameraIndex].gameObject.SetActive(true);
                }
            }
        }
    }

    public void toggleAlive()
    {
        alive = !alive;
        Debug.Log(alive);
    }
}

// Time Until Respawn: 
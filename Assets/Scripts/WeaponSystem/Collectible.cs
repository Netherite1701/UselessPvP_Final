using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}

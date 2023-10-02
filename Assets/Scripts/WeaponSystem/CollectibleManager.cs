using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] Hitbox hitbox;
    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    void OnCollectibleCollected()
    {
        hitbox.currentHealth += 10;
        hitbox.UpdateHealthbar();
    }
}

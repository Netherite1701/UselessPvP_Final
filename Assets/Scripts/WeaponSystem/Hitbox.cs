using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System;

public class Hitbox : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] Image healthbar;
    [SerializeField] GameObject UI;
    PhotonView PV;
    [SerializeField]
    GameObject player;
    public Variables var;
    [SerializeField]
    AudioClip hitSound;
    

    public float maxHealth = 100f;
    public float currentHealth = 100f;

    PlayerManager playerManager;
    private void Awake()
    {
        PV = player.GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(UI);
        }
    }



    public void TakeDamage(float damage,int id)
    {
        Debug.Log("took damage" + damage);
        PV.RPC("RPC_TakeDamage", PV.Owner, damage,id);
    }

    public void Heal(float amount)
    {
        Debug.Log("took heal" + amount);
        PV.RPC("RPC_Heal", PV.Owner, amount);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage,int id)
    {
        if (!PV.IsMine)
            return;
        if (var.abilities_.shieldUp == true)
            currentHealth -= damage / 2;
        else
            currentHealth -= damage;
        var.audioSource.PlayOneShot(hitSound);
        UpdateHealthbar();
        if (currentHealth <= 0)
        {
            photonView.RPC("IncrementKillCount", RpcTarget.AllBuffered,id);
            Die();
        }
    }

    [PunRPC]
    void RPC_Heal(float amount)
    {
        if (!PV.IsMine)
            return;
        if (currentHealth + amount <= 100)
            currentHealth += amount;
        UpdateHealthbar();
    }

    void Die()
    {
        playerManager.Die();
    }

    public void UpdateHealthbar()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }
}
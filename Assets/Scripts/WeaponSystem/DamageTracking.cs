using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracking : MonoBehaviour
{
    private Dictionary<int, int> damageRecord = new Dictionary<int, int>(); // Maps View ID to total damage inflicted.

    // Inflict damage on a player.
    public void InflictDamage(PhotonView attacker, PhotonView victim, int damageAmount)
    {
        int attackerViewID = attacker.ViewID;
        if (!damageRecord.ContainsKey(attackerViewID))
        {
            damageRecord[attackerViewID] = 0;
        }

        damageRecord[attackerViewID] += damageAmount;

        // Check if the victim's health is depleted.
        if (victim != null && victim.IsMine && victim.GetComponent<Hitbox>().currentHealth <= 0)
        {
            // Player died. Identify the killer using the damage record.
            int killerViewID = GetKillerViewID();
            Debug.Log("Player with View ID " + killerViewID + " killed the victim.");
        }
    }

    // Identify the player who inflicted the most damage (the killer).
    private int GetKillerViewID()
    {
        int killerViewID = -1;
        int maxDamage = 0;

        foreach (var entry in damageRecord)
        {
            if (entry.Value > maxDamage)
            {
                maxDamage = entry.Value;
                killerViewID = entry.Key;
            }
        }

        return killerViewID;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemScript : HealItem
{
    [SerializeField] Hitbox hitbox;
    float health;
    public override void Use()
    {
        Heal();
    }
    public void Heal()
    {
        StartCoroutine("HealCoroutine");
    }

    IEnumerator HealCoroutine()
    {
        print(1);
        yield return new WaitForSeconds(((HealItemInfo)itemInfo).useTime);
        print(1);
        health = hitbox.currentHealth;
        if (health + ((HealItemInfo)itemInfo).healAmount>=100)
        {
            hitbox.currentHealth = 100;
        }
        else
        {
            hitbox.currentHealth += ((HealItemInfo)itemInfo).healAmount;
        }
    }

    // public void UpdateUI()
    // {
    //     var.ammoCountUI.text = loadedAmmoCount + " / " + ammoCount;
    // }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Abilities : MonoBehaviour
{
    #region Setup
    public List<System.Action> abilityList;
    public List<System.Action> abilityReadyList;
    public Variables var;
    PlayerMovement playerMovement;
    AbilityInfo info;
    int radius = 7;
    public int healerHealAmount = 10;
    public int healCount = 5;
    bool healing = false;
    public bool shieldUp = false;

    private void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();

        abilityList = new List<System.Action>();
        abilityList.Add(SniperSkill);
        abilityList.Add(HealerSkill);
        abilityList.Add(TankerSkill);
        abilityList.Add(LandmineistSkill);
        abilityList.Add(PhaserSkill);
        //abilityList.Add(PushPullSkill);

        abilityReadyList = new List<System.Action>();
        abilityReadyList.Add(SniperReady);
        abilityReadyList.Add(HealerReady);
        abilityReadyList.Add(TankerReady);
        abilityReadyList.Add(LandmineistReady);
        abilityReadyList.Add(PhaserReady);
        //abilityReadyList.Add(PushPullReady);
        info = var.abilityScript.GetAbilityInfo();
    }

    public void UnequipItem(){
        var.playerMovement.EquipItem(1);
        var.playerMovement.canMove = true;
        var.aimingScript.aiming = false;
    }

    #endregion

    #region Sniper
    public void SniperSkill()
    {
        var.aimingScript.ZoomOut();
        playerMovement.itemindex = 0;
        playerMovement.previousItemIndex = -1;
        playerMovement.EquipItem(2);
        
    }

    #endregion

    #region Healer
    public void HealerSkill()
    {
        if (healing == false)
            StartCoroutine(HealerCouroutine());
    }

    IEnumerator HealerCouroutine()
    {
        
        healing = true;
        for (int i = 0; i < healCount; i++)
        {
            Collider[] hitColliders = Physics.OverlapSphere(var.healGO.transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Player")
                {
                    hitCollider.gameObject.GetComponent<Hitbox>().Heal(healerHealAmount);
                }
            }
            yield return new WaitForSeconds(info.abilityCoolTime);
        }
        healing = false;
        StartCoroutine(var.abilityScript.UnequipAbilityCoroutine());
    }

    #endregion

    #region Tanker
    public void TankerSkill()
    {
        StartCoroutine(TankerCoroutine());
    }
    IEnumerator TankerCoroutine()
    {
        var.aimingScript.aiming = true;
        var.aimingScript.cam.fieldOfView = 45;
        playerMovement.EquipItem(2);
        shieldUp = true;
        yield return new WaitForSeconds(info.abilityUseTime);
        playerMovement.EquipItem(1);
        shieldUp = false;
        UnequipItem();
    }
    #endregion

    #region Landmineist

    public void LandmineistSkill()
    {
        Debug.Log("Landmineist Skill");
    }

    #endregion

    #region Phaser

    public void PhaserSkill()
    {
        playerMovement.startDash();
        StartCoroutine(var.abilityScript.UnequipAbilityCoroutine());
    }

    #endregion

    #region Ready Functions
    public void SniperReady()
    {
        playerMovement.canMove = false;
        var.aimingScript.aiming = true;
        var.aimingScript.cam.fieldOfView = 45;
    }

    public void HealerReady()
    {
        playerMovement.canMove = false;
    }
    public void TankerReady()
    {
        
    }
    public void LandmineistReady()
    {
        
    }

    public void PhaserReady()
    {
        
    }

    #endregion

}

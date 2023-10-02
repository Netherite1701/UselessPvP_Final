using Photon.Pun;
using System.Collections;
using UnityEngine;

enum AbilityState {
    ready,
    active,
    cooldown
}



public class AbilityScript : AbilityItem
{
    public Abilities abilities;
    AbilityState state = AbilityState.ready;
    public Variables var;
    public bool usingAbility = false;
    [SerializeField]
    AbilityInfo info;


    //  
    public override void Use()
    {
        UseAbility();
    }
    private void Start()
    {
        // Set variables
        AbilityInfo info = ((AbilityInfo)itemInfo);
    }
    public void UseAbility()
    {

        if (state == AbilityState.ready){
            usingAbility = true;
            StartCoroutine(UseAbilityCoroutine());
        }
    }

    public AbilityInfo GetAbilityInfo()
    {
        return info;
    }

    public void UpdateUI()
    {
        var.ammoCountUI.text = "";
    }

    IEnumerator UseAbilityCoroutine()
    {
        abilities.abilityReadyList[((AbilityInfo)itemInfo).abilityIndex].Invoke();
        yield return new WaitForSeconds(((AbilityInfo)itemInfo).abilityCastTime);
        usingAbility = false;
        abilities.abilityList[((AbilityInfo)itemInfo).abilityIndex].Invoke();
        state = AbilityState.active;
        
    }

    public IEnumerator UnequipAbilityCoroutine()
    {
        abilities.UnequipItem();
        state = AbilityState.cooldown;
        yield return new WaitForSeconds(((AbilityInfo)itemInfo).abilityUnequipTime);
        state = AbilityState.ready;
        
    }

}
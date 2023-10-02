using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FPS/New Ability")]
public class AbilityInfo : ItemInfo
{
    [Header("General")]
    public float abilityCoolTime;
    public float abilityCastTime;
    public float abilityUseTime;
    public float abilityUnequipTime;
    public int abilityIndex;

}


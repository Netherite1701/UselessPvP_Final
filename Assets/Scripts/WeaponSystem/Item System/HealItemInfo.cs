using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FPS/New Heal Item")]
public class HealItemInfo : ItemInfo
{
    [Header("General")]
    public int healAmount;
    public float useTime;
}

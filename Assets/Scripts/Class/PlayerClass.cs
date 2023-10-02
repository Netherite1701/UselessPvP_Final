using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    [SerializeField]
    private ClassData classData;
    public ClassData ClassData { set { classData = value; }}

    public string ReturnClassName()
    {
        return classData.ClassName;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView PV;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject mainGo;

    private void Start()
    {
        if (PV.IsMine)
        {
            gameObject.SetActive(false);
            mainGo.name = PV.Owner.NickName;
        }
            

        text.text = PV.Owner.NickName;
    }
}

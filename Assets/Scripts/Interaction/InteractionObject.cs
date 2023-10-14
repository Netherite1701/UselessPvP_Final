using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class InteractionObject : MonoBehaviour
{
    [SerializeField]
    int typeIndex;
    [SerializeField]
    int weaponID;

    public TMP_Text cooltimeText;

    const float WAITTIME = 3f;

    public bool cooldown = false;
    public IEnumerator Interact(GameObject go)
    {
        yield return null;
        switch (typeIndex)
        {
            case 0: //Ammo
                Debug.Log("Ammo");
                if (!cooldown)
                {
                    go.GetComponent<GunScript>().ammoCount += 30;//Random.Range(30, 60);
                    StartCoroutine(CooldownCoroutine());
                }
                
                //PhotonNetwork.Destroy(this.gameObject);
                break;
            case 1: //Heal
                //go.GetComponent<PlayerWeapon>().EquipWeapon(weaponID);
                break;
        }
    }

    IEnumerator CooldownCoroutine()
    {
        cooldown = true;
        int t = 10;
        cooltimeText.text = t.ToString() + "s";
        while (t > 0)
        {
            cooltimeText.text = t.ToString() + "s";
            yield return new WaitForSeconds(1f);
            t--;
        }
        cooltimeText.text = "";
        cooldown = false;
    }
}

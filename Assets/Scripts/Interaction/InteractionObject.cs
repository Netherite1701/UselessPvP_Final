using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InteractionObject : MonoBehaviour
{
    [SerializeField]
    int typeIndex;
    [SerializeField]
    int weaponID;

    const float WAITTIME = 3f;
    public IEnumerator Interact(GameObject go)
    {
        yield return new WaitForSeconds(WAITTIME);
        switch (typeIndex)
        {
            case 0: //Ammo
                Debug.Log("Ammo");
                go.GetComponent<GunScript>().ammoCount += 30;//Random.Range(30, 60);
                //PhotonNetwork.Destroy(this.gameObject);
                break;
            case 1: //Heal
                //go.GetComponent<PlayerWeapon>().EquipWeapon(weaponID);
                break;
        }
    }
}

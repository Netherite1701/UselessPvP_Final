using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerUsernameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;

    public void OnUsernameInputChanged()
    {
        PhotonNetwork.NickName = usernameInput.text;
    }
}

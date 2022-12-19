using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyPanel : MonoBehaviour
{
    public TMP_InputField playerNameField;
    public Button joinButton;

    private NetworkManager networkManager;

    public void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

    }

    public void JoinRoom()
    {
        networkManager.JoinOrCreateRoom();
    }
}

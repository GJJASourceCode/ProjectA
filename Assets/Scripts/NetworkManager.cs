using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    private MenuUIManager uiManager;
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        uiManager = GameObject.Find("UI").GetComponent<MenuUIManager>();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinedRoom()
    {
        var props = PhotonNetwork.LocalPlayer.CustomProperties;
        props["Ready"] = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        uiManager.lobbyPanel.gameObject.SetActive(false);
        uiManager.roomPanel.gameObject.SetActive(true);
        Debug.Log("Joined Room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Join Room Failed");
    }

    public void JoinOrCreateRoom(string roomName)
    {
        RoomOptions roomopt = new RoomOptions
        {
            MaxPlayers = 2,
        };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomopt, TypedLobby.Default);
    }

    public void SetPlayerName(string text)
    {
        PhotonNetwork.LocalPlayer.NickName = text;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        uiManager.roomPanel.UpdatePlayerList();
        Debug.Log("Player " + newPlayer.NickName + " Entered");
    }
    public override void OnPlayerLeftRoom(Player leftPlayer)
    {
        uiManager.roomPanel.UpdatePlayerList();
        Debug.Log("Player " + leftPlayer.NickName + " Left");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("Ready"))
        {
            uiManager.roomPanel.UpdatePlayerList();
        }
    }

}

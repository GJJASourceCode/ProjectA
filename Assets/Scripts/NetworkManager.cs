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
        Screen.SetResolution(1920, 1080, true);
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
        uiManager.lobbyPanel.joinButton.interactable = true;
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
        Debug.Log("Joined Room");
        PhotonNetwork.LoadLevel("MainScene");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Join Room Failed");
    }

    public void JoinOrCreateRoom()
    {
        RoomOptions roomopt = new RoomOptions
        {

        };
        PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: roomopt);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player " + newPlayer.NickName + " Entered");
    }
    public override void OnPlayerLeftRoom(Player leftPlayer)
    {
        Debug.Log("Player " + leftPlayer.NickName + " Left");
    }


}

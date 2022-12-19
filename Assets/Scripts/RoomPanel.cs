using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomPanel : MonoBehaviour
{
    public TMP_Text roomNameText;

    public List<TMP_Text> playerNameList;
    public Button playButton;

    void OnEnable()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }


    public void UpdatePlayerList()
    {
        int i = 0;
        int readyPlayerCount = 0;
        foreach (var (_, player) in PhotonNetwork.CurrentRoom.Players)
        {
            var props = player.CustomProperties;
            playerNameList[i].text = "Player " + (i + 1) + " : " + player.NickName;
            if (props.ContainsKey("Ready") && (bool)props["Ready"])
            {
                playerNameList[i].text += " Ready";
                readyPlayerCount++;

            }
            i++;
        }

        for (; i < playerNameList.Count; i++)
        {
            playerNameList[i].text = "";
        }

        if (readyPlayerCount == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Game Start");
                PhotonNetwork.LoadLevel("MainScene");
            }
        }
    }

    public void ToggleReady()
    {
        var props = PhotonNetwork.LocalPlayer.CustomProperties;
        props["Ready"] = !(bool)props["Ready"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        if ((bool)props["Ready"])
        {
            playButton.GetComponentInChildren<TMP_Text>().text = "Cancel";
        }
        else
        {
            playButton.GetComponentInChildren<TMP_Text>().text = "Ready";
        }

        UpdatePlayerList();
    }




}

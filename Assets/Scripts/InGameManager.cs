using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System;

public class InGameManager : MonoBehaviourPunCallbacks
{

    private CameraController cameraController;

    public void SpawnPlayer()
    {
        int i = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
        var player = PhotonNetwork.Instantiate("Player", getRandomPos(), Quaternion.identity);
        cameraController.targetTransform = player.transform.Find("CameraPos");
    }

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        SpawnPlayer();
    }

    Vector3 getRandomPos()
    {
        var pos = UnityEngine.Random.insideUnitCircle * 25;
        return new Vector3(pos.x, 4.69f, pos.y);
    }
}

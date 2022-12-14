using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System;
using System.IO;

public class InGameManager : MonoBehaviourPunCallbacks
{

    public List<Transform> spawnPoints;

    private CameraController cameraController;

    public void SpawnPlayer()
    {
        int i = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
        var player = PhotonNetwork.Instantiate("Player", spawnPoints[i].position, spawnPoints[i].rotation);
        cameraController.targetTransform = player.transform.Find("CameraPos");
    }

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        SpawnPlayer();
    }
}

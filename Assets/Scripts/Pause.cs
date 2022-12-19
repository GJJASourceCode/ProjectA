using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool mouseEnabled = false;
    public GameObject pausePanel;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mouseEnabled = !mouseEnabled;
            if (mouseEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pausePanel.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pausePanel.SetActive(false);
            }
        }
    }

    public void ReturnToGame()
    {
        mouseEnabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        PhotonNetwork.LeaveRoom();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

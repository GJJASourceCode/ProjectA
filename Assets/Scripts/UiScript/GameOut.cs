using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOut : MonoBehaviour
{
    public GameObject ButtonS;


    public void OnClickQuitButton()
    {
        ButtonS.SetActive(false);
        Application.Quit();
        #if !UNITY_EDITOR
                System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif
    }
}


/*/        if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
/*/

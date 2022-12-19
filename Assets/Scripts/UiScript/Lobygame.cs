using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobygame : MonoBehaviour
{
    public GameObject Button;
    public GameObject FadeLoading;


    public void OnClickButton()
    {
        Button.SetActive(false);
        FadeLoading.SetActive(true);
    }
}

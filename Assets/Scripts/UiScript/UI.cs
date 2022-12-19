using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public GameObject DieUi;
    [SerializeField]
    private Slider HP;


    public int MaxHealth;
    public int NowHealth;


    void Start()
    {
        HP.value = (float)NowHealth / (float)MaxHealth;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NowHealth -= 10;
        }

        HandleHp();
        if(NowHealth <= 0)
        {

            DieUi.SetActive(true);
        }
    }

    private void HandleHp()
    {
        HP.value = (float)NowHealth /(float)MaxHealth;
    }


}

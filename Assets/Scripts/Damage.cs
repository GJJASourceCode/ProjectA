using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviourPunCallbacks
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    private bool canTakeDamage = true;
    private PhotonView pv;
    private Grab grab;
    public Slider hpSlider;
    void Awake()
    {
        pv = GetComponent<PhotonView>();
        hpSlider = GameObject.Find("HpBar").GetComponent<Slider>();
        grab = GetComponent<Grab>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            hpSlider.value = (float)currentHealth / (float)maxHealth;
            if (currentHealth <= 0)
            {
                if (GameObject.Find(grab.grappedWeapon).GetComponent<Weapon>().isAxe)
                    grab.pv.RPC("ReleaseWeapon", RpcTarget.AllBuffered);
                PhotonNetwork.LeaveRoom();
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Take Damage: " + damage + ", Remaining Health: " + currentHealth);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (pv.IsMine)
        {
            if (canTakeDamage && hit.gameObject.TryGetComponent<Weapon>(out Weapon weapon) && weapon.isHeld)
            {
                GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, weapon.damage);
                StartCoroutine(HalfSecondDelay());
            }

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (pv.IsMine)
        {
            if (canTakeDamage && other.gameObject.TryGetComponent<Weapon>(out Weapon weapon) && weapon.isHeld)
            {
                GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, weapon.damage);
                StartCoroutine(HalfSecondDelay());
            }
        }
    }


    IEnumerator HalfSecondDelay()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }
}

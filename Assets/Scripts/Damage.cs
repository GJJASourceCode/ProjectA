using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Damage : MonoBehaviourPunCallbacks
{
    public int health = 100;

    private bool canTakeDamage = true;

    private PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Take Damage: " + damage + ", Remaining Health: " + health);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (pv.IsMine)
        {
            Debug.Log("Collision");
            if (canTakeDamage && hit.gameObject.TryGetComponent<Weapon>(out Weapon weapon))
            {
                Debug.Log("Weapon Collsion");
                GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, weapon.damage);
                StartCoroutine(HalfSecondDelay());
            }

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (pv.IsMine)
        {
            Debug.Log("Collision");
            if (canTakeDamage && other.gameObject.TryGetComponent<Weapon>(out Weapon weapon))
            {
                Debug.Log("Weapon Collsion");
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

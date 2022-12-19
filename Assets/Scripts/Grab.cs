using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grab : MonoBehaviour
{

    [SerializeField] private Transform handPos;
    [SerializeField] private Transform lookAtPos;

    public string grappedWeapon;
    public PhotonView pv;
    private bool canGrab = true;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        grappedWeapon = GetComponentInChildren<Weapon>().gameObject.name;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (pv.IsMine)
        {
            if (canGrab && hit.collider.gameObject.TryGetComponent(out Weapon weapon) && !weapon.isHeld)
            {
                pv.RPC("ReleaseWeapon", RpcTarget.AllBuffered);
                pv.RPC("GrabWeapon", RpcTarget.AllBuffered, weapon.gameObject.name);
            }
        }
    }

    [PunRPC]
    public void GrabWeapon(string weaponName)
    {
        grappedWeapon = weaponName;
        Weapon weapon = GameObject.Find(weaponName).GetComponent<Weapon>();
        weapon.transform.SetParent(handPos);
        weapon.transform.localPosition = weapon.grabPos;
        weapon.transform.localRotation = Quaternion.Euler(weapon.grabRot);
        Destroy(weapon.gameObject.GetComponent<Rigidbody>());
        weapon.isHeld = true;
    }


    [PunRPC]
    public void ReleaseWeapon()
    {
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon.isAxe)
        {
            weapon.transform.SetParent(GameObject.Find("Environment").transform);
            weapon.isHeld = false;
            weapon.transform.rotation = Quaternion.identity;
            var rigid = weapon.gameObject.AddComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            Destroy(weapon.gameObject);
        }
        StartCoroutine(TwoSecondsDelay());
    }

    IEnumerator TwoSecondsDelay()
    {
        canGrab = false;
        yield return new WaitForSeconds(2f);
        canGrab = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grab : MonoBehaviour
{
    private bool isGrabbing = false;

    [SerializeField] private Transform handPos;
    [SerializeField] private Transform lookAtPos;

    private string grappedWeapon;

    private PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            // Grab
            if (Input.GetMouseButton(0))
            {
                if (Physics.SphereCast(lookAtPos.position, 1.0f, lookAtPos.forward, out RaycastHit hit, 3.0f))
                    if (hit.collider.gameObject.TryGetComponent(out Weapon weapon))
                    {
                        if (isGrabbing)
                        {
                            pv.RPC("ReleaseWeapon", RpcTarget.AllBuffered);
                        }
                        pv.RPC("GrabWeapon", RpcTarget.AllBuffered, weapon.gameObject.name);
                        isGrabbing = true;
                    }
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
    }


    [PunRPC]
    public void ReleaseWeapon()
    {
        Weapon weapon = GameObject.Find(grappedWeapon).GetComponent<Weapon>();
        weapon.transform.SetParent(GameObject.Find("Environment").transform);
    }
}

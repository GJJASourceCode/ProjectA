using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool isGrabbing = false;

    [SerializeField] private Transform handPos;
    [SerializeField] private Transform lookAtPos;

    void Update()
    {
        // Grab
        if (Physics.SphereCast(lookAtPos.position, 1.0f, lookAtPos.forward, out RaycastHit hit, 3.0f))
            if (!isGrabbing && Input.GetMouseButton(0) && hit.collider.gameObject.TryGetComponent(out Weapon weapon))
            {
                hit.transform.SetParent(handPos);
                hit.transform.localPosition = weapon.grabPos;
                hit.transform.rotation = Quaternion.Euler(weapon.grabRot);
            }

        // Release
        if (isGrabbing && Input.GetMouseButtonUp(0))
        {
            isGrabbing = false;
            hit.transform.SetParent(GameObject.Find("Environment").transform);
        }
    }
    void OnJointBreak()
    {
        isGrabbing = false;
    }
}

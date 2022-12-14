using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 grabPos;
    public Vector3 grabRot;
    public int damage;
    public bool isHeld;
    public float rotationSpeed;
    public bool isAxe;

    void Update()
    {
        if (!isHeld)
        {
            transform.Rotate(Vector3.up, 360 * Time.deltaTime * rotationSpeed);
        }
    }
}

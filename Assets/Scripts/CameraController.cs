using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;

    void Update()
    {
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;
    private PhotonView pv;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed; // Player Speed
    [SerializeField] private float gravity = -9.81f; // Gravitational Acceleration
    [SerializeField] private float mouseSensitivityX; // Camera X Axis Move Speed
    [SerializeField] private float mouseSensitivityY; // Camera Y Axis Move Speed
    private float rotationX = 0;

    private float velocityY = 0;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            MoveCamera();
            MoveCharacter();
        }
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 35);

        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        move.Normalize();
        animator.SetFloat("Speed X", horizontal);
        animator.SetFloat("Speed Z", vertical);
        move *= speed * Time.deltaTime;
        characterController.Move(move);


        velocityY += gravity;
        characterController.Move(Vector3.up * velocityY * Time.deltaTime);

    }





}

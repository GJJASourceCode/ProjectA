using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool ikActive;
    [SerializeField] private Transform handPos;
    [SerializeField] private Transform lookAtPos;
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void OnAnimatorIK(int layerIndex)
    {
        if (ikActive)
        {
            if (lookAtPos != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookAtPos.position);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.7f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.7f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, handPos.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, handPos.rotation);
            }
        }
        else
        {
            animator.SetLookAtWeight(0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}

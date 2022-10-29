using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool ikActive;
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
            }
        }
        else
        {
            animator.SetLookAtWeight(0);
        }
    }
}

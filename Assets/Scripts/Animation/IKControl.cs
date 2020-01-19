using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    public bool IKActive { get; set; }
    public Transform RightHand { get; set; }
    public Transform LookObject { get; set; }

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (_animator)
        {
            if (IKActive)
            {
                if (LookObject != null)
                {
                    _animator.SetLookAtWeight(1);
                    _animator.SetLookAtPosition(LookObject.position);
                }
                if (RightHand != null)
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.25f);
                    //_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHand.position);
                    //_animator.SetIKRotation(AvatarIKGoal.RightHand, RightHand.rotation);
                }

            }
            else
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetLookAtWeight(0);
            }
        }
    }
}

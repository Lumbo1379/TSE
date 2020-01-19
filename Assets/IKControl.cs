using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    public bool IKActive;
    public Transform RightHand;
    public Transform LookObject;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (_animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (IKActive)
            {

                // Set the look target position, if one has been assigned
                if (LookObject != null)
                {
                    _animator.SetLookAtWeight(1);
                    _animator.SetLookAtPosition(LookObject.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (RightHand != null)
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.25f);
                    //_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHand.position);
                    //_animator.SetIKRotation(AvatarIKGoal.RightHand, RightHand.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetLookAtWeight(0);
            }
        }
    }
}

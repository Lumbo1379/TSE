using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMeter : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            _animator.SetBool("isChecking", true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            _animator.SetBool("isChecking", false);
    }
}

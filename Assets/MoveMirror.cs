using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMirror : MonoBehaviour, IInteractable
{
    private const float RotateIncrement = 15f;

    public bool RotateInX;
    [Range(0, 10)] public float RotationSpeed;

    private Quaternion _rotateTo;

    private void Start()
    {
        _rotateTo = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotateTo, RotationSpeed * Time.deltaTime);
    }

    public void Interact()
    {
        if (RotateInX)
            _rotateTo *= Quaternion.Euler(RotateIncrement, 0, 0);
        else
            _rotateTo *= Quaternion.Euler(0, 0, RotateIncrement);
    }
}

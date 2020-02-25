using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(0, 1000)] public float MouseSensitivity;
    [Range(0, 180)] public float YRotationAmount;

    private Transform _playerBody;
    private float _xRotation;

    private void Start()
    {
        _playerBody = transform.parent.transform;
        _xRotation = 0;

        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY; // Decrease so rotation isn't flipped
        _xRotation = Mathf.Clamp(_xRotation, -YRotationAmount / 2, YRotationAmount / 2); // Clamp rotation so player can only move there head about 180 degrees

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // Move camera
        _playerBody.Rotate(Vector3.up * mouseX); // Move player

        
    }
}

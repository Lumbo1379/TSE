using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerHitDetection : MonoBehaviour
{
    [Range(0, 10)] public float PushPower;

    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3)
            return;

        var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * PushPower;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("RespawnZone"))
        {
            _controller.enabled = false; // So character controller doesn't override transform
            transform.position = other.transform.GetComponent<RespawnZoneController>().RespawnPosition;
            _controller.enabled = true;
        }
    }
}

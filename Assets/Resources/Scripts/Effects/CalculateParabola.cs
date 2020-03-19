// ADAPTED FROM https://vilbeyli.github.io/Projectile-Motion-Tutorial-for-Arrows-and-Missiles-in-Unity3D/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateParabola : MonoBehaviour
{
    [Range(0, 90)] public float LaunchAngle;
    [Range(0, 360)] public float DirectionRotation;
    [Range(0, 10)] public float Distance;

    private Rigidbody _rb;
    private Quaternion _initialRotation;
    private Vector3 _endPos;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _initialRotation = transform.rotation;
        _endPos = transform.position + Quaternion.AngleAxis(DirectionRotation, Vector3.up) * Vector3.right * Distance;
        Launch();
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_rb.velocity) * _initialRotation;
    }

    private void Launch()
    {
        Vector3 posXZ = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetPosXZ = new Vector3(_endPos.x, 0.0f, _endPos.z);

        transform.LookAt(targetPosXZ);

        // V0x = √ GR2 / 2(H - R tan(α))    V0y = V0x tan(α)

        float r = Vector3.Distance(posXZ, targetPosXZ);
        float g = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float h = _endPos.y - transform.position.y;

        float vZ = Mathf.Sqrt(g * r * r / (2.0f * (h - r * tanAlpha)));
        float vY = tanAlpha * vZ;

        Vector3 localVelocity = new Vector3(0.0f, vY, vZ);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        _rb.velocity = globalVelocity;
    }
}

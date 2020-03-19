﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThrustForce : MonoBehaviour
{
    [Range(0, 10)] public float Thrust;
    [Range(0, 100)] public float Range;
    [Range(0, 3)] public float ThrustForceArea;
    public Camera Camera;

    private Animator _animator;
    private IKControl _iKControl;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _iKControl = GetComponent<IKControl>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // Left click
            DrawRay();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _iKControl.IKActive = false;
            _iKControl.LookObject = null;
            _iKControl.RightHand = null;
            _animator.SetBool("isForcing", false);
        }
    }

    private void DrawRay()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Range))
        {
            _iKControl.IKActive = true;
            _iKControl.LookObject = hit.transform;
            _iKControl.RightHand = hit.point;

            _animator.SetBool("isForcing", true);

            foreach (var collider in ScanForColliders(hit.point))
            {
                if (collider.transform.CompareTag("Moveable") || collider.transform.CompareTag("OSourceLink") || collider.transform.CompareTag("EnergyOrb"))
                    ThrustObject(hit.point, collider.gameObject, hit.distance);
            }
        }
        else
        {
            _iKControl.IKActive = false;
            _iKControl.LookObject = null;
            _iKControl.RightHand = null;

            _animator.SetBool("isForcing", false);
        }
    }

    private void ThrustObject(Vector3 hitPosition, GameObject obj, float distanceAway)
    {
        FindObjectOfType<AudioManager>().PlayAudio("forcePower");
        var fallOff = (Range - distanceAway) / Range;
        var rb = obj.GetComponent<Rigidbody>();
        var direction = obj.transform.position - transform.position;

        rb.AddForce(direction * Thrust * fallOff);
    }

    private Collider[] ScanForColliders(Vector3 hit)
    {
        return Physics.OverlapSphere(hit, ThrustForceArea);
    }
}

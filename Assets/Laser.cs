﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int MaxReflections;
    public float MaxLength;

    private LineRenderer _lineRenderer;
    private bool _conduitActivated;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _conduitActivated = false;
    }

    private void Update()
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, transform.forward);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainingLength = MaxLength;

        for (int i = 0; i < MaxReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.direction, hit.normal);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.CompareTag("LaserConduit") && !_conduitActivated)
                {
                    hit.transform.GetComponent<ActivateLaserConduit>().SetActive();
                    _conduitActivated = true;
                    break;
                }

                if (!hit.collider.CompareTag("Reflectable") && !hit.collider.CompareTag("StaticReflectable"))
                    break;
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}

using System.Collections;
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
        _lineRenderer.SetPosition(0, transform.position); // Set initial position

        float remainingLength = MaxLength;

        for (int i = 0; i < MaxReflections; i++) // While can still make more reflections
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength)) // If collides with an object, if doesn't laser drawn until length = MaxLength
            {
                _lineRenderer.positionCount += 1; // New reflection/impact
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point); // Set point of impact
                remainingLength -= Vector3.Distance(ray.direction, hit.normal); // Minus distance from last position
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal)); // Reflect a new laser from point of impact

                if (hit.collider.CompareTag("LaserConduit") && !_conduitActivated)
                {
                    hit.transform.GetComponent<ActivateLaserConduit>().SetActive();
                    _conduitActivated = true;
                    break;
                }

                if (!hit.collider.CompareTag("Reflectable") && !hit.collider.CompareTag("StaticReflectable")) // If laser impacts on something which isn't reflectable stop trying to reflect
                    break;
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength); // Draw laser until total length = MaxLength
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustForce : MonoBehaviour
{
    [Range(0, 10)] public float Thrust;
    [Range(0, 100)] public float Range;
    public Camera Camera;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // Left click
            DrawRay();
    }

    private void DrawRay()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Range))
        {
            if (hit.transform.CompareTag("Moveable")) // CompareTag is quicker than == "Some tag"
                ThrustObject(hit.point, hit.transform.gameObject, hit.distance);
        }
    }

    private void ThrustObject(Vector3 hitPosition, GameObject obj, float distanceAway)
    {
        var fallOff = (Range - distanceAway) / Range;
        var rb = obj.GetComponent<Rigidbody>();
        var direction = obj.transform.position - transform.position;

        rb.AddForce(direction * Thrust * fallOff);
    }
}

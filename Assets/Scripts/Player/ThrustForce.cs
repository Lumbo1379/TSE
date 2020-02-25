using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustForce : MonoBehaviour
{
    [Range(0, 10)] public float Thrust;
    [Range(0, 100)] public float Range;
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
            if (hit.transform.CompareTag("Moveable")) // CompareTag is quicker than == "Some tag"
            {
                _iKControl.IKActive = true;
                _iKControl.LookObject = hit.transform;
                _iKControl.RightHand = hit.transform;

                _animator.SetBool("isForcing", true);
                ThrustObject(hit.point, hit.transform.gameObject, hit.distance);
            }
            else
            {
                _iKControl.IKActive = false;
                _iKControl.LookObject = null;
                _iKControl.RightHand = null;

                _animator.SetBool("isForcing", false);
            }
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

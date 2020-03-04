using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractCloseObjects : MonoBehaviour
{
    [Range(0, 100000)] public float PullForce;
    [Range(0, 10)] public float PullRadius;
    [Range(0, 100000)] public float ForceReducation;
    public bool ConstantAttract;
    public float DisableAttractDelay;

    private bool _attract;
    private float _runningForce; // Running as in while the game is running

    private void Start()
    {
        _attract = true;
        _runningForce = PullForce;
    }

    private void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, PullRadius))
        {
            if (collider.CompareTag("EnergyOrb"))
            {
                Vector3 direction = transform.position - collider.transform.position;
                collider.GetComponent<Rigidbody>().AddForce(direction.normalized * _runningForce * Time.fixedDeltaTime);
            }
        }

        if (!_attract)
        {
            if (_runningForce > 0)
            {
                _runningForce -= ForceReducation * Time.deltaTime;
                _runningForce = Mathf.Clamp(_runningForce, 0, PullForce);
            }
        }
    }

    public void OnActiveDisableAttract()
    {
        if (!ConstantAttract)
            Invoke("DisableAttract", DisableAttractDelay);
    }

    private void DisableAttract()
    {
        _attract = false;
    }

    public void OnLeaveEnableAttract()
    {
        _attract = true;
        _runningForce = PullForce;
    }
}

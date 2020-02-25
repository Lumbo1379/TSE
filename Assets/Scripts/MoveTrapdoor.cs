using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrapdoor : MonoBehaviour
{
    [Range(0, 25)] public float MoveSpeed;
    [Range(0, 10)] public float ResetTime;
    public GameObject Trapdoor;
    public Vector3 TargetPosition;
    public Vector3 ResetPosition;

    private Vector3 _currentTarget;

    private void Start()
    {
        _currentTarget = ResetPosition;
    }

    private void Update()
    {
        Trapdoor.transform.localPosition = Vector3.MoveTowards(Trapdoor.transform.localPosition, _currentTarget, MoveSpeed * Time.deltaTime);
    }

    public void PressButton()
    {
        _currentTarget = TargetPosition;

        CancelInvoke("ResetTrapdoor");
        Invoke("ResetTrapdoor", ResetTime);
    }

    private void ResetTrapdoor()
    {
        _currentTarget = ResetPosition;
    }
}

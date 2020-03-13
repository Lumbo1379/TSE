using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    [Range(0, 0.5f)] public float LevitateStrength;
    [Range(0, 5)] public float Speed;

    private float _startY;

    private void Start()
    {
        _startY = transform.localPosition.y;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _startY + (float)Math.Sin(Time.time * Speed) * LevitateStrength, transform.localPosition.z);
    }
}

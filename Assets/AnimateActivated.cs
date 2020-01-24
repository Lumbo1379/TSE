using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateActivated : MonoBehaviour
{
    [Range(0, 5)] public float Speed;
    [ColorUsage(true, true)] public Color StartColour;
    [ColorUsage(true, true)] public Color EndColour;
    public Material LightMaterial;

    private float _startTime;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        LightMaterial.SetColor("_EmissionColor", StartColour);
    }

    private void Update()
    {
        if (_isActive)
        {
            float time = (Time.time - _startTime) * Speed;
            LightMaterial.SetColor("_EmissionColor", Color.Lerp(StartColour, EndColour, time));
        }
    }

    public void SetActive()
    {
        _isActive = true;
        _startTime = Time.time;
    }
}

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
    private Color _currentColour;
    private AttractCloseObjects _attractObjects;

    private void Start()
    {
        _isActive = false;
        _attractObjects = GetComponent<AttractCloseObjects>();
        LightMaterial.SetColor("_EmissionColor", StartColour);
    }

    private void Update()
    {
        if (_isActive)
        {
            float time = (Time.time - _startTime) * Speed;
            LightMaterial.SetColor("_EmissionColor", Color.Lerp(StartColour, EndColour, time));
        }
        else
        {
            float time = (Time.time - _startTime) * Speed;
            LightMaterial.SetColor("_EmissionColor", Color.Lerp(_currentColour, StartColour, time));
        }
    }

    private void SetActive()
    {
        _isActive = true;
        _startTime = Time.time;
        _attractObjects.OnActiveDisableAttract();
    }

    private void SetInactive()
    {
        _isActive = false;
        _startTime = Time.time;
        _currentColour = LightMaterial.GetColor("_EmissionColor");
        _attractObjects.OnLeaveEnableAttract();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable") && !_isActive)
            SetActive();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moveable") && _isActive)
            SetInactive();
    }
}

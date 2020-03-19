using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLaserConduit : MonoBehaviour
{
    [Range(0, 5)] public float Speed;
    [ColorUsage(true, true)] public Color StartColour;
    [ColorUsage(true, true)] public Color EndColour;
    public DoubleSlidingDoorController DoubleSlidingDoorController;

    private float _startTime;
    private bool _isActive;
    private Color _currentColour;
    private Material _newMaterialInstance;

    private void Start()
    {
        _isActive = false;
        _newMaterialInstance = GetComponent<Renderer>().materials[1]; // Create a new instance of the material so doesn't affect main material
        _newMaterialInstance.SetColor("_EmissiveColor", StartColour);
    }

    private void Update() // Similar to pressure pad but opens door when fully activated, no attraction as well
    {
        if (_isActive)
        {
            float time = (Time.time - _startTime) * Speed;
            _newMaterialInstance.SetColor("_EmissiveColor", Color.Lerp(StartColour, EndColour, time));

            if (_newMaterialInstance.GetColor("_EmissiveColor") == EndColour && !DoubleSlidingDoorController.Open)
                DoubleSlidingDoorController.Open = true;
        }
        else
        {
            float time = (Time.time - _startTime) * Speed;
            _newMaterialInstance.SetColor("_EmissiveColor", Color.Lerp(_currentColour, StartColour, time));
        }
    }

    public void SetActive()
    {
        _isActive = true;
        _startTime = Time.time;
    }
}

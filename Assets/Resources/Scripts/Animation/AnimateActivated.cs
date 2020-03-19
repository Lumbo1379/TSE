using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateActivated : MonoBehaviour
{
    [Range(0, 5)] public float Speed;
    [ColorUsage(true, true)] public Color StartColour;
    [ColorUsage(true, true)] public Color EndColour;
    public GameObject CodeNumber;
    public Vector3 CodeNumberOffset;
    public Vector3 CodeNumberRotationOffset;

    private float _startTime;
    private bool _isActive;
    private Color _currentColour;
    private AttractCloseObjects _attractObjects;
    private int _activatedObjectInstanceID;
    private bool _codeRevealed;
    private Material _newMaterialInstance;

    private void Start()
    {
        _isActive = false;
        _attractObjects = GetComponent<AttractCloseObjects>();
        _codeRevealed = false;
        _newMaterialInstance = GetComponent<Renderer>().materials[1]; // Create a new instance of the material so doesn't affect main material
        _newMaterialInstance.SetColor("_EmissiveColor", StartColour); // Strings to communicate with shader properties
    }

    private void Update()
    {
        if (_isActive)
        {
            float time = (Time.time - _startTime) * Speed;
            _newMaterialInstance.SetColor("_EmissiveColor", Color.Lerp(StartColour, EndColour, time)); // Change colour from start to end using the difference in time from the first change/update when isActive

            if (_newMaterialInstance.GetColor("_EmissiveColor") == EndColour && !_codeRevealed) // If fully activated
            {
                _codeRevealed = true;

                ShowCodeNumber();
            }
        }
        else // If orb moved off, revert back 
        {
            float time = (Time.time - _startTime) * Speed;
            _newMaterialInstance.SetColor("_EmissiveColor", Color.Lerp(_currentColour, StartColour, time));
        }
    }

    private void SetActive() // When orb enters
    {
        FindObjectOfType<AudioManager>().PlayAudio("pressurePadActivated");

        _isActive = true;
        _startTime = Time.time;
        _attractObjects.OnActiveDisableAttract();
    }

    private void SetInactive() // When orb leaves
    {
        _isActive = false;
        _startTime = Time.time;
        _currentColour = _newMaterialInstance.GetColor("_EmissiveColor");
        _attractObjects.OnLeaveEnableAttract();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyOrb") && !_isActive)
        {
            _activatedObjectInstanceID = other.gameObject.GetInstanceID();
            SetActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnergyOrb") && _isActive && other.gameObject.GetInstanceID() == _activatedObjectInstanceID)
            SetInactive();
    }

    private void ShowCodeNumber() // When pressure pad fully activated
    {
        var codeNumber = Instantiate(CodeNumber, transform.position, CodeNumber.transform.rotation);
        codeNumber.transform.position += CodeNumberOffset;
        codeNumber.transform.rotation = Quaternion.Euler(codeNumber.transform.rotation.eulerAngles + CodeNumberRotationOffset);
        codeNumber.transform.GetChild(0).GetComponent<HologramFadeIn>().enabled = true;
    }
}

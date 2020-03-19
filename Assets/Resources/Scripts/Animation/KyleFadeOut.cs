using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyleFadeOut : MonoBehaviour
{
    [Range(0, 0.05f)] public float FadeInSpeed;

    private float _currentFadeInValue;
    private Material _hologramMaterialInstance;
    private bool _fadeOut;

    private void Start()
    {
        _fadeOut = false;
        _currentFadeInValue = 0f;
        _hologramMaterialInstance = GetComponent<Renderer>().material;
    }

    private void Update() // Opposite of code number pressure pad fade in
    {
        if (FadeInSpeed > 0 && _fadeOut)
        {
            _currentFadeInValue += FadeInSpeed;
            _currentFadeInValue = Mathf.Clamp(_currentFadeInValue, 0, 0.6f); // Max is 0.6f (invisible)


            _hologramMaterialInstance.SetFloat("Vector1_112413B2", _currentFadeInValue);
        }

        if (_currentFadeInValue >= 0.6f) // Destroy when invisible
            Destroy(transform.parent.gameObject);
    }

    public void FadeOut() // Called when tutorial finished
    {
        _fadeOut = true;
    }
}

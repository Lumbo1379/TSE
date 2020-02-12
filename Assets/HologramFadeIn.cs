using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramFadeIn : MonoBehaviour
{
    public Material HologramMaterial;
    [Range(0, 0.05f)] public float FadeInSpeed;

    private float _currentFadeInValue;

    private void Start()
    {
        _currentFadeInValue = 0.55f;
    }

    private void Update()
    {
        if (FadeInSpeed > 0)
        {
            _currentFadeInValue -= FadeInSpeed;
            _currentFadeInValue = Mathf.Clamp(_currentFadeInValue, 0, 0.55f);


            HologramMaterial.SetFloat("Vector1_112413B2", _currentFadeInValue);
        }
    }
}

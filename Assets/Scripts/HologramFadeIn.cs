using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramFadeIn : MonoBehaviour
{
    [Range(0, 0.05f)] public float FadeInSpeed;

    private float _currentFadeInValue;
    private Material _hologramMaterialInstance;

    private void Start()
    {
        _currentFadeInValue = 0.55f;
        _hologramMaterialInstance = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (FadeInSpeed > 0)
        {
            _currentFadeInValue -= FadeInSpeed;
            _currentFadeInValue = Mathf.Clamp(_currentFadeInValue, 0, 0.55f);


            _hologramMaterialInstance.SetFloat("Vector1_112413B2", _currentFadeInValue);
        }
    }
}

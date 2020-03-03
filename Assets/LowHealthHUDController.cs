using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHealthHUDController : MonoBehaviour
{
    public OxygenMeter OxygenMeter;
    [ColorUsage(true, true)] public Color32 StartColour;

    private Image _panelHUD;

    private void Start()
    {
        _panelHUD = GetComponent<Image>();
    }

    private void Update()
    {
        _panelHUD.color = new Color32(Convert.ToByte(StartColour.r), Convert.ToByte(StartColour.g), Convert.ToByte(StartColour.b), Convert.ToByte(Mathf.Abs(OxygenMeter.Oxygen - 100) / 10));
    }
}

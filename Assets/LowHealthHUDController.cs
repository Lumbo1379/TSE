using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHealthHUDController : MonoBehaviour
{
    public OxygenMeter OxygenMeter;

    private Image _panelHUD;

    private void Start()
    {
        _panelHUD = GetComponent<Image>();
    }

    private void Update()
    {
        _panelHUD.color = new Color32(Convert.ToByte(_panelHUD.color.r), Convert.ToByte(_panelHUD.color.g), Convert.ToByte(_panelHUD.color.b), Convert.ToByte(Mathf.Abs(OxygenMeter.Oxygen - 100)));
    }
}

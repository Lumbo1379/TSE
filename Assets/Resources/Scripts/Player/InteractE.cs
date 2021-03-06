﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractE : MonoBehaviour
{
    [Range(0, 100)] public float InteractRange;
    public Camera Camera;
    [ColorUsage(true, true)] public Color RegularColour;
    [ColorUsage(true, true)] public Color InteractableInRangeColour;
    public GameObject CrossHair;

    private bool _interactableInRange;

    private void Start()
    {
        _interactableInRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ShootRay();

        CheckForInteractable();
    }

    private void ShootRay() // Check if interactable in range
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Interactable") || hit.transform.CompareTag("Reflectable"))
                hit.transform.gameObject.GetComponent<IInteractable>().Interact(); // Implementation of how interacts will be different, interface means this does not become an issue
        }
    }

    private void CheckForInteractable()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if ((hit.transform.CompareTag("Interactable") || hit.transform.CompareTag("Reflectable")) && !_interactableInRange) // Change crosshair
            {
                _interactableInRange = true;
                CrossHair.transform.localScale *= 3;
                CrossHair.GetComponent<Image>().color = InteractableInRangeColour;
            }
            else if (!hit.transform.CompareTag("Interactable") && !hit.transform.CompareTag("Reflectable") && _interactableInRange) // Reset crosshair
            {
                _interactableInRange = false;
                CrossHair.transform.localScale /= 3;
                CrossHair.GetComponent<Image>().color = RegularColour;
            }
        }
    }
}

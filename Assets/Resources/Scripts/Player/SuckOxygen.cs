﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckOxygen : MonoBehaviour
{
    public LayerMask OSuckMask;
    public Camera Camera;
    [Range(0, 1000)] public float OSuckRange;
    public GameObject LifeForceSuckEffect;
    public GameObject EffectOrigin;
    [Range(0, 5)] public float EffectDelay;
    public GameObject BloodExplosionEffect;
    public GameObject DeathBloodSplat;
    [Range(0, 100)] public int OxygenGain;
    public CompleteGame RabbitDeathCounter;

    private GameObject _currentSelection;
    private Animator _playerAnimator;
    private IKControl _iKControl;
    private OxygenMeter _oxygenMeter;

    private void Start()
    {
        _currentSelection = null;
        _playerAnimator = GetComponent<Animator>();
        _iKControl = GetComponent<IKControl>();
        _oxygenMeter = GetComponent<OxygenMeter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click 
            DrawRay();

        if (Input.GetMouseButtonUp(1))
        {
            if (_currentSelection != null)
                DeselectObject(false);
        }

    }

    private void DrawRay()
    {
        
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, OSuckRange, OSuckMask)) // Check for rabbits only on specific layers
            HandleCollision(hit.transform);
        else if (_currentSelection != null) // If moved off target
            DeselectObject(false);
            

    }

    private void HandleCollision(Transform collision)
    {
        if (collision.gameObject != _currentSelection)
        {
            if (collision.CompareTag("OSourceLink")) // If rabbit in cage
            {
                var rabbit = collision.GetComponent<OSourceLink>().Rabbit;
                
                if (rabbit != null)
                    collision = rabbit.transform;
                else
                {
                    collision.gameObject.layer = 0; // If rabbit in cage exploded, make cage not suckable anymore
                    return;
                }
            }

            Invoke("ShowEffect", EffectDelay);

            if (_currentSelection != null)
                DeselectObject(true); // If moved off rabbit onto another
            else
                FindObjectOfType<AudioManager>().PlayAudio("Laser");

            _playerAnimator.SetBool("isSucking", true);


            _currentSelection = collision.gameObject;

            _iKControl.IKActive = true; // Move hand to centre on rabbit
            _iKControl.LookObject = _currentSelection.transform;
            _iKControl.RightHand = _currentSelection.transform.position;

            var animator = collision.parent.GetComponent<Animator>();
            animator.SetBool("Balloon", true);
        }
    }

    private void DeselectObject(bool switchSeamless) // Seamless = moving from rabbit to another
    {
        if (_currentSelection != null)
        {
            _currentSelection.transform.parent.GetComponent<Animator>().SetBool("Balloon", false);
            _currentSelection = null; // Set null to work for when use holds right click but moves off rabbit
        }

        _iKControl.IKActive = false;
        _iKControl.LookObject = null;
        _iKControl.RightHand = null;

        _playerAnimator.SetBool("isSucking", switchSeamless);
        LifeForceSuckEffect.SetActive(switchSeamless);
        EffectOrigin.SetActive(switchSeamless);
    }

    private void ShowEffect()
    {
        if (_currentSelection != null)
        {
            EffectOrigin.transform.parent = _currentSelection.transform.parent.transform;

            var localPivotPoint = _currentSelection.GetComponent<SkinnedMeshRenderer>().bounds.center.normalized; // Suck effect from on hand
            EffectOrigin.transform.position = _currentSelection.transform.parent.transform.position;
            EffectOrigin.transform.position += localPivotPoint;
            LifeForceSuckEffect.SetActive(true);
            EffectOrigin.SetActive(true);
        }

    }

    public void BlowUpAnimalEvent()
    {
        FindObjectOfType<AudioManager>().PlayAudio("rabbitExplosion");
        var selectionPos = _currentSelection.GetComponent<SkinnedMeshRenderer>().bounds.center;

        Instantiate(BloodExplosionEffect,
            selectionPos,
            BloodExplosionEffect.transform.rotation);

        EffectOrigin.transform.parent = transform;
        Destroy(_currentSelection.transform.parent.gameObject);
        DeselectObject(false);

        _oxygenMeter.UpdateMeterOnSuccessfulSuck(OxygenGain);
        RabbitDeathCounter.RabbitsKilled++;
    }
}

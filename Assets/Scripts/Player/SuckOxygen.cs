using System.Collections;
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

    private GameObject _currentSelection;
    private Animator _playerAnimator;

    private void Start()
    {
        _currentSelection = null;
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) // Right click
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

        if (Physics.Raycast(ray, out hit, OSuckRange, OSuckMask))
            HandleCollision(hit.transform);
        else if (_currentSelection != null)
            DeselectObject(false);
            

    }

    private void HandleCollision(Transform collision)
    {
        if (collision.gameObject != _currentSelection)
        {
            Invoke("ShowEffect", EffectDelay);

            if (_currentSelection != null)
                DeselectObject(true);
            else
                _playerAnimator.SetBool("isSucking", true);

            _currentSelection = collision.gameObject;

            var animator = collision.parent.GetComponent<Animator>();
            animator.SetBool("Balloon", true);
        }
    }

    private void DeselectObject(bool switchSeamless)
    {
        if (_currentSelection != null)
        {
            _currentSelection.transform.parent.GetComponent<Animator>().SetBool("Balloon", false);
            _currentSelection = null; // Set null to work for when use holds right click but hovers off rabbit
        }

        _playerAnimator.SetBool("isSucking", switchSeamless);
        LifeForceSuckEffect.SetActive(switchSeamless);
        EffectOrigin.SetActive(switchSeamless);
    }

    private void ShowEffect()
    {
        if (_currentSelection != null)
        {
            EffectOrigin.transform.parent = _currentSelection.transform.parent.transform;

            var localPivotPoint = _currentSelection.GetComponent<SkinnedMeshRenderer>().bounds.center.normalized;
            EffectOrigin.transform.position = _currentSelection.transform.parent.transform.position;
            EffectOrigin.transform.position += localPivotPoint;
            LifeForceSuckEffect.SetActive(true);
            EffectOrigin.SetActive(true);
        }

    }

    public void BlowUpAnimalEvent()
    {
        Instantiate(BloodExplosionEffect,
            _currentSelection.transform.parent.transform.position,
            BloodExplosionEffect.transform.rotation);

        EffectOrigin.transform.parent = transform;
        Destroy(_currentSelection.transform.parent.gameObject);
        DeselectObject(false);
    }
}

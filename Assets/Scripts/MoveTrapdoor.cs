using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrapdoor : MonoBehaviour, IInteractable
{
    [Range(0, 25)] public float MoveSpeed;
    [Range(0, 10)] public float ResetTime;
    public GameObject Trapdoor;
    public Vector3 TargetPosition;
    public Vector3 ResetPosition;
    public Vector3 PressedPos;

    private Vector3 _currentTarget;
    private Vector3 _buttonTarget;
    private Vector3 _originalPos;

    private void Start()
    {
        _currentTarget = ResetPosition;
        _buttonTarget = transform.localPosition;
        _originalPos = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _buttonTarget, MoveSpeed / 10 * Time.deltaTime);

        Trapdoor.transform.localPosition = Vector3.MoveTowards(Trapdoor.transform.localPosition, _currentTarget, MoveSpeed * Time.deltaTime);
    }

    public void Interact()
    {
        _currentTarget = TargetPosition;
        FindObjectOfType<AudioManager>().PlayAudio("ButtonPress");
        AnimateButtonPress();
        CancelInvoke("ResetTrapdoor");
        Invoke("ResetTrapdoor", ResetTime);
    }

    private void ResetTrapdoor()
    {
        _currentTarget = ResetPosition;
    }

    private void AnimateButtonPress()
    {
        _buttonTarget = PressedPos;

        CancelInvoke("ResetPressedButton");
        Invoke("ResetPressedButton", 0.5f);
    }

    private void ResetPressedButton()
    {
        _buttonTarget = _originalPos;
    }
}

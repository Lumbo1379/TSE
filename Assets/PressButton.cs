using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public string Code;

    private ButtonScreenController _buttonScreenController;

    private void Start()
    {
        _buttonScreenController = transform.parent.GetComponent<ButtonScreenController>();
    }

    public void Press()
    {
        _buttonScreenController.ButtonPressed(gameObject, Code);
    }
}

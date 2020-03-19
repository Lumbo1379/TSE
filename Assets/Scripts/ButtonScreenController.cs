using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScreenController : MonoBehaviour
{
    public Transform Key1;
    public Transform Key2;
    public Transform Key3;
    public string Code;
    public Material DoorOpenMaterial;
    public DoubleSlidingDoorController DoubleSlidingDoorController;
    public Renderer PanelMeshRenderer;

    private int _key;
    private bool _blockInput;
    private string _inputCode;

    private void Start()
    {
        _key = 0;
        _inputCode = "";
        _blockInput = false;
    }

    public void ButtonPressed(GameObject pressed, string code)
    {
        //button noise
        FindObjectOfType<AudioManager>().PlayAudio("ButtonPress");

        if (_blockInput)
            return;

        _key++;

        Vector3 instantiatePos;

        instantiatePos = code == "1" ? new Vector3(-0.0419f, 0, 0.05f) : Vector3.zero;

        switch (_key)
        {
            case 1:
            {
                var button = Instantiate(pressed, Key1);
                button.transform.localPosition = instantiatePos;
                _inputCode += code;
                break;
            }
            case 2:
            {
                var button = Instantiate(pressed, Key2);
                button.transform.localPosition = instantiatePos;
                _inputCode += code;
                break;
            }
            case 3:
            {
                var button = Instantiate(pressed, Key3);
                button.transform.localPosition = instantiatePos;
                _inputCode += code;
                CheckCode();
                break;
            }
        }
    }

    private void CheckCode()
    {
        if (_inputCode == Code)
        {
            _blockInput = true;
            DoubleSlidingDoorController.Open = true;
            var materials = PanelMeshRenderer.materials;
            materials[1] = DoorOpenMaterial;
            PanelMeshRenderer.materials = materials;

        }
        else
            ResetKeyPad();

    }

    private void ResetKeyPad()
    {
        _blockInput = true;
        _inputCode = "";
        _key = 0;

        InvokeRepeating("FlickerKeyPad", 0, 0.5f);
        Invoke("UnblockKeyPad", 2f);
    }

    private void FlickerKeyPad()
    {
        if (Key1.gameObject.activeSelf)
        {
            Key1.gameObject.SetActive(false);
            Key2.gameObject.SetActive(false);
            Key3.gameObject.SetActive(false);
        }
        else
        {
            Key1.gameObject.SetActive(true);
            Key2.gameObject.SetActive(true);
            Key3.gameObject.SetActive(true);
        }
    }

    private void UnblockKeyPad()
    {
        CancelInvoke("FlickerKeyPad");

        Destroy(Key1.GetChild(0).gameObject);
        Destroy(Key2.GetChild(0).gameObject);
        Destroy(Key3.GetChild(0).gameObject);

        Key1.gameObject.SetActive(true);
        Key2.gameObject.SetActive(true);
        Key3.gameObject.SetActive(true);

        _blockInput = false;
    }
}

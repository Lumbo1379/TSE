using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public AudioSource[] AudioSource;
    public GameObject[] Text;
    public KeyCode[] KeyToContinue;
    public PlayerMovement Player;
    public OxygenMeter OxygenMeter;
    public GameObject Kyle;

    private int _incrementer;
    private float _playerSpeed;
    private float _playerJumpHeight;

    private void Awake()
    {
        _incrementer = 0;

        _playerSpeed = Player.Speed;
        _playerJumpHeight = Player.JumpHeight;

        Player.Speed = 0;
        Player.JumpHeight = 0;

        AudioSource[0].Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyToContinue[_incrementer]))
        {
            AudioSource[_incrementer].Stop();
            Text[_incrementer].SetActive(false);

            _incrementer++;

            if (_incrementer != AudioSource.Length)
            {
                AudioSource[_incrementer].Play();
                Text[_incrementer].SetActive(true);
            }
            else
            {
                Player.Speed = _playerSpeed;
                Player.StartSpeed = _playerSpeed;
                Player.JumpHeight = _playerJumpHeight;
                OxygenMeter.BeginDepletingOxygen();
                Kyle.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}

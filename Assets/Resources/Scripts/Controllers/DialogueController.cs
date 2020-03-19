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
    public KyleFadeOut Kyle;

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

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlayAudio("MainTheme"); // Play main theme when tutorial begins (level starts)
    }

    private void Update()
    {
        if (_incrementer < AudioSource.Length) // While there is more text to display
        {
            if (Input.GetKeyDown(KeyToContinue[_incrementer]))
            {
                AudioSource[_incrementer].Stop(); // Stop previous audio if skipped before finished
                Text[_incrementer].SetActive(false); 

                _incrementer++;

                if (_incrementer != AudioSource.Length)
                    Text[_incrementer].SetActive(true);
                else
                {
                    Player.Speed = _playerSpeed; // Allow player to move and jump
                    Player.StartSpeed = _playerSpeed;
                    Player.JumpHeight = _playerJumpHeight;
                    OxygenMeter.BeginDepletingOxygen(); // Start losing oxygen
                    Kyle.FadeOut();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

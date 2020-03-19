using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteGame : MonoBehaviour
{
    public int RabbitsKilled { get; set; }

    public Transform[] RabbitContainers;
    [Range(0, 3)] public float TimeBetweenScoreDisplayUpdate;
    public TextMeshProUGUI RabbitText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI MultiplierText;
    public TextMeshProUGUI ScoreText;

    private float _time;
    private float _rabbits;
    private int _rabbitCounter;
    private decimal _multiplier;

    private void Start()
    {
        _time = 0;
        _rabbits = 0;
        _rabbitCounter = -1;
        RabbitsKilled = 0;

        foreach (Transform rabbitContainer in RabbitContainers)
            _rabbits += rabbitContainer.childCount;
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    public void CalculateScore()
    {
        Invoke("AddRabbit", TimeBetweenScoreDisplayUpdate);
    }

    private void AddRabbit()
    {
        _rabbitCounter++;

        RabbitText.text = "Innocent Rabbits Killed: " + (_rabbitCounter + "/" + _rabbits);

        FindObjectOfType<AudioManager>().PlayAudio("bloodSplat");

        Invoke(_rabbitCounter == RabbitsKilled ? "DisplayTime" : "AddRabbit", TimeBetweenScoreDisplayUpdate);
    }

    private void DisplayTime()
    {
        FindObjectOfType<AudioManager>().PlayAudio("bloodSplat");
        TimeText.text += Mathf.RoundToInt(_time) + " seconds";

        Invoke("DisplayMultiplier", TimeBetweenScoreDisplayUpdate);
    }

    private void DisplayMultiplier()
    {
        FindObjectOfType<AudioManager>().PlayAudio("bloodSplat");

        _multiplier = decimal.Round((decimal) ((_rabbits - RabbitsKilled) / _rabbits) + 1, 1);

        MultiplierText.text += _multiplier + "x";

        Invoke("DisplayScore", TimeBetweenScoreDisplayUpdate);
    }

    private void DisplayScore()
    {
        FindObjectOfType<AudioManager>().PlayAudio("bloodSplat");
        ScoreText.text += Mathf.Round((1000 - _time) * (float)_multiplier);

        Cursor.lockState = CursorLockMode.None; // Show and unlock cursor
        Cursor.visible = true;
    }
}

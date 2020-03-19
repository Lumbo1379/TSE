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

        foreach (Transform rabbitContainer in RabbitContainers) // Get number of rabbits before any killed
            _rabbits += rabbitContainer.childCount;
    }

    private void Update()
    {
        _time += Time.deltaTime; // Time in level from entry, including tutorial
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

        Invoke(_rabbitCounter == RabbitsKilled ? "DisplayTime" : "AddRabbit", TimeBetweenScoreDisplayUpdate); // If counted all rabbits move on to next score section
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

        _multiplier = decimal.Round((decimal) ((_rabbits - RabbitsKilled) / _rabbits) + 1, 1); // Percentage out of 12

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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;
    //allows reference from other scripts

    public GameObject PauseMenuUI;

    private void Start()
    {
        
        GamePaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
                Resume(); //resume game as pause has already been pressed - therefore already in pause screen
            else
                Pause(); //game not already paused so needs to now pause as player input dictates
        }
    }

    //public void so can be triggered from the button itself
    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<AudioManager>().Resume("MainTheme");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //sets game time to normal rate
        GamePaused = false;
    }

    private void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        //mute music when game paused
        FindObjectOfType<AudioManager>().Stop("MainTheme");
        PauseMenuUI.SetActive(true);
        //ensures mouse cursor is seen when navigating the menu       
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; //freezes game for pause menu
        GamePaused = true;
    }

    public void LoadMenu()
    {
        //ensures user always goes back to menu when they click
        SceneManager.LoadScene("MainMenu"); ;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

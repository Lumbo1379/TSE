using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    //allows reference from other scripts

    public GameObject pauseMenu;

    

   


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (GamePaused)
            {
                //resume game as pause has already been pressed - therefore already in pause screen
                Resume();
            }
            else
            {
                //game not already paused so needs to now pause as player input dictates
                Pause();
            }
        }
    }

    //public void so can be triggered from the button itself
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //sets game time to normal rate
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        //ensures mouse cursor is seen when navigating the menu       
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; //freezes game for pause menu
        GamePaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); ;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

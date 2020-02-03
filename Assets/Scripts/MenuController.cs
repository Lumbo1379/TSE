using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{

    public void PlayGame()
    {
        //The next scene loaded is the next item in sequence so the game is played in order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //once built, game will close
        Debug.Log("Quit"); //shows us button works inside editor
        Application.Quit();
    }




}
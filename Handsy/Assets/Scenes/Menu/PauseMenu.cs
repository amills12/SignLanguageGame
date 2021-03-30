using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Tells you if the game is paused, this is what you should import
    public static bool GameIsPaused = false;
    public static bool GameIsRestarted = false;
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject introMenu;


    // On awake pause the game and turn on the intro screen
    void Awake()
    {
        if (GameIsRestarted)
        {
            //If the game has ended and is being replayed, un-pause the game and disable intro menu
            Time.timeScale = 1f;
            GameIsPaused = false;
            GameIsRestarted = false;
            introMenu.SetActive(false);
        }
        else
        {
            //If the game isn't a replay, pause the game and turn on the intro menu
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Run the pause menu off of escape button, This might need a logic rework depending on how many menus we have
        if (Input.GetKeyDown(KeyCode.Escape) && !GameIsRestarted)
        {
            if (GameIsPaused && !introMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Disable pause menu
        pauseMenuUI.SetActive(false);

        // Enable time again
        Time.timeScale = 1f;

        GameIsPaused = false;
    }

    public void EndGame()
    {
        // Stops the games by disabling time. (Pretty metal)
        Time.timeScale = 0f;
    }

    void Pause()
    {
        // If the intro menu is active during a pause, we need to close it
        if (introMenu.activeSelf)
        {
            introMenu.SetActive(false);
            gameUI.SetActive(true);
        }

        // Turns on the pause menu
        pauseMenuUI.SetActive(true);


        // Stops the games by disabling time. (Pretty metal)
        Time.timeScale = 0f;

        GameIsPaused = true;
    }

    public void LoadMainMenu(string level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    public void gameRestart()
    {
        //Set game is restarted to true
        GameIsRestarted = true;

        //Get current scene name
        string scene = SceneManager.GetActiveScene().name;

        //Load it
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}

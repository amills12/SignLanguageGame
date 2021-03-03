using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{    
    // Tells you if the game is paused, this is what you should import
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Run the pause menu off of escape button
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
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

    void Pause()
    {
        // Turns on the pause menu
        pauseMenuUI.SetActive(true);

        // Stops the games by disabling time. (Pretty metal)
        Time.timeScale = 0f;

        GameIsPaused = true;
    }

    public void LoadMainMenu(string level)
    {
        Resume();
        SceneManager.LoadScene(level);
    }
}

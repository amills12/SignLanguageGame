using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   void Awake()
   {
      Time.timeScale = 1f;
   }

   public void PlayGame(string level)
   {
      SceneManager.LoadScene(level);
   }

   public void QuitGame()
   {
      Debug.Log("Application has quit.");
      Application.Quit();
   }
}

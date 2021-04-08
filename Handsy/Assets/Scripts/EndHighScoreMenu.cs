using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndHighScoreMenu : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject addScoreMenu, endGameMenu, highScoreMenu;
    private int score;

    private void Awake()
    {
        score = PlayerPrefs.GetInt("Score");
        
        // If score is a highscore value
        if(highScoreMenu.GetComponent<HighScoreTable>().checkIfHighscore(score))
        {
            addScoreMenu.SetActive(true);
            endGameMenu.SetActive(false);
        }
        else
        {
            addScoreMenu.SetActive(false);
            endGameMenu.SetActive(true);
        }
    }

    public void enterNameButton()
    {
        // Don't change the menus if name blank
        if(inputField.text.Length > 0)
        {
            // Input the name along with score into highscore menu
            highScoreMenu.GetComponent<HighScoreTable>().addScoreEntry(score, inputField.text);
            highScoreMenu.SetActive(true);
            addScoreMenu.SetActive(false);
        }
    }
}

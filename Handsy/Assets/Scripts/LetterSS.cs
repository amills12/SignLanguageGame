using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LetterSS : MonoBehaviour
{
    public bool isActivated = true;//bool to determine if the letter is activated
    public KeyCode key; //assign keycode to specific letter
    public int mastery, numMastered; 
    private string mapKey;
    private bool mastered = false;
    void Start(){
        mapKey = key.ToString(); //convert the key pressed to a string for mapping
        mastery = PlayerPrefs.GetInt(mapKey);
        numMastered = PlayerPrefs.GetInt("NumMastered"); 
    }
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false; //remove font visibility
            GetComponent<SpriteRenderer>().enabled = false; //remove hand model visibility
            isActivated = false; //deactivate letter
            mastery++;
            if(mastery == 2)
            {
                addToProgress();
            }

            PlayerPrefs.SetInt(mapKey, mastery);
        }
    }

    void addToProgress()
    {
        if (!mastered)
        {
            numMastered++;
            PlayerPrefs.SetInt("NumMastered", numMastered);
            mastered = true;
        }
    }



}


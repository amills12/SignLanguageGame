using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LetterSS : MonoBehaviour
{
    public bool isActivated = true;//bool to determine if the letter is activated
    public bool isLetter; //use to separate digits and numbers
    public KeyCode key; //assign keycode to specific letter
    public int mastery, numMastered, numNumMastered, masteryLevel; 
    private string mapKey;
    private bool mastered = false;
    void Start(){
        mapKey = key.ToString(); //convert the key pressed to a string for mapping

        //initialize specs for sign mastery
        mastery = PlayerPrefs.GetInt(mapKey);
        numMastered = PlayerPrefs.GetInt("NumMastered"); 
        numNumMastered = PlayerPrefs.GetInt("NumNumMastered");
    }
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false; //remove font visibility
            GetComponent<SpriteRenderer>().enabled = false; //remove hand model visibility
            isActivated = false; //deactivate letter

            mastery++;//if successfully signed, mastery increases one level
            if(mastery == masteryLevel) //if mastery reaches set level, then add to progress
            {
                addToProgress();
            }

            PlayerPrefs.SetInt(mapKey, mastery); //set the mastery level within the local database for the specific letter
        }
    }

    void addToProgress()
    {
        if (!mastered)
        {
            if(isLetter) //if the sign is a letter, then add to alphabet mastery progress
            {
                numMastered++; //increment the number of mastered letters by one
                PlayerPrefs.SetInt("NumMastered", numMastered); //store in local database
            }

            else //if it is a number, add to number mastery progress
            {
                numNumMastered++; //increment the number of mastered numbers by one
                PlayerPrefs.SetInt("NumNumMastered", numNumMastered); //store in local database
            }

            mastered = true; //set sign to succesfully mastered
        }
    }



}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LetterSS : MonoBehaviour
{
    GameObject hands;

    Leap.Unity.HandsyDetector leftHand_nums, rightHand_nums;

    Handsy_Distance_Detector_Right rightHand;
    Handsy_Distance_Detector_Left leftHand;

    SpawnerStatic ss;

    public bool isActivated = true;//bool to determine if the letter is activated
    public bool isLetter; //use to separate digits and numbers
    public KeyCode key; //assign keycode to specific letter
    public string cleanedKey;
    private string signID;
    public int mastery, numMastered, numNumMastered, masteryLevel; 
    private bool mastered = false;
    void Start(){

        hands = GameObject.FindGameObjectWithTag("Player");

        leftHand = hands.transform.GetChild(2).GetComponent<Handsy_Distance_Detector_Left>();
        rightHand = hands.transform.GetChild(3).GetComponent<Handsy_Distance_Detector_Right>();

        // Capture RigidRoundHand_L and RigidRoundHand_R for numbers
        leftHand_nums = hands.transform.GetChild(2).GetComponent<Leap.Unity.HandsyDetector>();
        rightHand_nums = hands.transform.GetChild(3).GetComponent<Leap.Unity.HandsyDetector>();

        //capture spawner for animation boolean
        ss = GameObject.FindGameObjectWithTag("RAMSpawner").GetComponent<SpawnerStatic>();

        //initialize specs for sign mastery
        signID = this.name;
        mastery = PlayerPrefs.GetInt(signID);
        numMastered = PlayerPrefs.GetInt("NumMastered"); 
        numNumMastered = PlayerPrefs.GetInt("NumNumMastered");
    }
    void Update()
    {
        cleanKey(key);
        if(ss.timeDone && (Input.GetKeyDown(key)  || leftHand.activated || rightHand.activated || leftHand_nums.activated || rightHand_nums.activated)
            && ((leftHand != null) || (rightHand != null) || (leftHand_nums != null) || (rightHand_nums != null)) )
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false; //remove font visibility
            GetComponent<SpriteRenderer>().enabled = false; //remove hand model visibility
            isActivated = false; //deactivate letter

            mastery++;//if successfully signed, mastery increases one level
            if(mastery == masteryLevel) //if mastery reaches set level, then add to progress
            {
                addToProgress();
            }

            PlayerPrefs.SetInt(signID, mastery); //set the mastery level within the local database for the specific letter
            
            //set all activations to false to prevented re-enabling
            leftHand.activated = false; 
            rightHand.activated = false;
            leftHand_nums.activated = false;
            rightHand_nums.activated = false;
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

    string cleanKey(KeyCode curKey){
        if (curKey.ToString().Contains("Alpha")){
            return curKey.ToString()[curKey.ToString().Length - 1].ToString();
        }
        return curKey.ToString();
    }

}


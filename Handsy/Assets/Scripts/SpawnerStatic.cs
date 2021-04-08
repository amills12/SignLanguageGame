using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnerStatic : MonoBehaviour
{
    //~~~~~~~~~~~~~~~~~~ Variables ~~~~~~~~~~~~~~~~~~//
    public Transform spawnLocation; //empty object that just contains a location
    public Vector3 location; // for holding xyz location of asset spawn
    public GameObject[] prefab; //array that holds all the hand models
    public GameObject[] clonePrefab; //used to retrieve models from prefab array and display them
    public AudioSource pencil; //sound source for a pencil drawing sound
    public Animator animated; //for fade in animation
    public GameObject sprMask; //object that contains animation
    public int size, sizeAlpha, sizeNum; //number of letters/numbers
    public bool timeDone = false; //used for checking if animation complete
    public int letIndex, score = 0; //array index and score tracker
    public int amtMasteredAlpha, amtMasteredNum;
    public float  progressAlpha, progressNum;
    public string progressStr;
    public GameObject endScreen, endScreenObject; //handles menu slides

    public Text tempProgress; //remember to delete later.

    public char characterKey;

    //~~~~~~~~~~~~~~~~~~~~~~~~~ Game Logic ~~~~~~~~~~~~~~~~~~~~~~~~~//
    void Start()
    {
        //spawn the first randomized object
        animated = sprMask.GetComponent<Animator>();

        //wait to show letter for animation to show properly
        StartCoroutine(waitToStart(0.1f));

        //get random index in the prefab array
        letIndex = Random.Range(0, size);

        //set the pencil drawing sound
        pencil = GetComponent<AudioSource>();
    }

    void Update()
    {
        //end the game after 30 successful signs
        if (score == 30)
        {
            endScreen.GetComponent<PauseMenu>().EndGame();
            endScreenObject.SetActive(true);
        }

        /*if the current letter was signed correctly, it will be deactivated. 
        If deactivated, the game will destroy the current clone, reactivate the original object,
        generate a new random object from the array, then spawn it*/
        else if (timeDone && !clonePrefab[letIndex].GetComponent<LetterSS>().isActivated)
        {
            
            getProgressPct();
            Destroy(clonePrefab[letIndex]);
            //increment the score on successful sign
            score++;
            timeDone = false;

            //reactivate the letter for later use
            prefab[letIndex].GetComponent<LetterSS>().isActivated = true;

            //retrieve another random letter
            letIndex = Random.Range(0, size);

            //wait for animation - changed this to avoid repetitive accepts
            StartCoroutine(waitToStart(1.5f));
        }

        //feed character to recognition
        characterKey = clonePrefab[letIndex].name.ToCharArray()[0];
    }

    void Spawn()
    {

        //get the location of the desired letter spawn   
        location = spawnLocation.transform.position;

        //clone the desired prefab object to spawn at the desired location
        clonePrefab[letIndex] = Instantiate(prefab[letIndex], location, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    IEnumerator waitToStart(float time)
    {
        //set a timer
        yield return new WaitForSeconds(time);

        //start animation
        animated.Play("AnPractice");
        Spawn();

        //ensure the time has finished before spawning the next letter
        timeDone = true;

        //set a short timer for the drawing sound to play in conjunction with the animation
        yield return new WaitForSeconds(0.5f);
        pencil.Play();

    }
   
    /*
    This function gets the percentage of progress between all the numbers 
    and letters succesfully mastered. 
    To find the percentage, it finds the public numMastered variable from the LetterSS object,
    then divides
    */
    public void getProgressPct()
    {
            amtMasteredAlpha = clonePrefab[letIndex].GetComponent<LetterSS>().numMastered;
            amtMasteredNum = clonePrefab[letIndex].GetComponent<LetterSS>().numNumMastered;

            if (amtMasteredAlpha > 0)
            {
                progressAlpha = (((float)amtMasteredAlpha/sizeAlpha)*100f);
                PlayerPrefs.SetFloat("ProgressPercentAlpha", progressAlpha);
            }

            if (amtMasteredNum > 0)
            {
                progressNum = (((float)amtMasteredNum/sizeNum)*100f);
                PlayerPrefs.SetFloat("ProgressPercentNum", progressNum);
            }

            progressStr =  progressAlpha.ToString("0") + "%";
            PlayerPrefs.SetString("PercentDisplayAlpha", progressStr);
            progressStr =  progressNum.ToString("0") + "%";
            PlayerPrefs.SetString("PercentDisplayNum", progressStr);   
    }

    public void reset()
    {
        PlayerPrefs.DeleteAll();
    }

}

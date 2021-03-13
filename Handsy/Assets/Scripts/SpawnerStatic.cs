using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnerStatic : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject[] prefab;
    public AudioSource pencil;
    public GameObject[] clonePrefab; 
    public Vector3 location; // for location of asset spawn
    public Animator animated; //for fade in animation
    public GameObject sprMask;
    //number of letters/numbers in the prefab array
    public int size = 36;
    public bool timeDone = false;

    //use for determining which letter will be spawned from the arrary
    public int letIndex, score = 0; 
    public GameObject endScreen, endScreenObject, intro;
    void Start()
    {
        //spawn the first randomized object
        animated = sprMask.GetComponent<Animator>();

        //wait to show letter for animation to show properly
        StartCoroutine(waitToStart(0.1f));

        //get random index in the prefab array
        letIndex = Random.Range(0,size);

        //set the pencil drawin sound
        pencil = GetComponent<AudioSource>();

    }

    void Update()
    {
        //end the game after 30 succesful signs
        if(score == 5) //FIXME
        {
            endScreen.GetComponent<PauseMenu>().EndGame();
            endScreenObject.SetActive(true);
        }  

        /*if the current letter was signed correctly, it will be deactivated. 
        If deactivated, the game will destroy the current clone, reactivate the original object,
        generate a new random object from the array, then spawn it*/
        else if(timeDone && !clonePrefab[letIndex].GetComponent<LetterSS>().isActivated)
        {

            Destroy(clonePrefab[letIndex]);
            //increment the score on successful sign
            score++; 
            timeDone = false;

            //reactivate the letter for later use
            prefab[letIndex].GetComponent<LetterSS>().isActivated = true;

            //retrieve another random letter
            letIndex = Random.Range(0,size);

            //wait for animation
            StartCoroutine(waitToStart(0.1f));
        }

    }

    void Spawn()
    {

        //get the location of the desired letter spawn   
        location = spawnLocation.transform.position;

        //clone the desired prefab object to spawn at the desired location
        clonePrefab[letIndex] = Instantiate(prefab[letIndex], location, Quaternion.Euler(0,0,0)) as GameObject;
    }

    IEnumerator waitToStart(float n){
        //set a timer
        yield return new WaitForSeconds(n);
        
        //start animation
        animated.Play("AnPractice");
        Spawn();

        //ensure the time has finished before spawning the next letter
        timeDone = true;

        //set a short timer for the drawing sound to play in conjunction with the animation
        yield return new WaitForSeconds(0.5f);
        pencil.Play();

    }

}

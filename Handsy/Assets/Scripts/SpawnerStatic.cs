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
    public GameObject endScreen;
    public GameObject endScreenObject;
    void Start()
    {
        //spawn the first randomized object
        animated = sprMask.GetComponent<Animator>();
        StartCoroutine(waitToStart(0.1f));
        letIndex = Random.Range(0,size);
        pencil = GetComponent<AudioSource>();
    }

    void Update()
    {
        /*if the current letter was signed correctly, it will be deactivated. 
        If deactivated, the game will destroy the current clone, reactivate the original object,
        generate a new random object from the array, then spawn it*/
        if(score == 5)
        {
            endScreen.GetComponent<PauseMenu>().EndGame();
            endScreenObject.SetActive(true);
        }  

        else if(timeDone && !clonePrefab[letIndex].GetComponent<LetterSS>().isActivated)
        {
            score++; //increment the score
            Destroy(clonePrefab[letIndex]);
            timeDone = false;
            prefab[letIndex].GetComponent<LetterSS>().isActivated = true;
            letIndex = Random.Range(0,size);
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
        
        yield return new WaitForSeconds(n);
        animated.Play("AnPractice");
        Spawn();
        timeDone = true;
        yield return new WaitForSeconds(0.5f);
        pencil.Play();

    }


}

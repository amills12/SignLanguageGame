﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerStatic : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject[] prefab;
    public GameObject[] clonePrefab;
    public Vector3 location;

    //number of letters/numbers in the prefab array
    public int size = 36;

    //use for determining which letter will be spawned from the arrary
    public int letIndex = 0; 
    
    void Start()
    {
        //spawn the first randomized object
        letIndex = Random.Range(0,size);
        Spawn();
    }

    void Update()
    {
        /*if the current letter was signed correctly, it will be deactivated. 
        If deactivated, the game will destroy the current clone, reactivate the original object,
        generate a new random object from the array, then spawn it*/

        if(!clonePrefab[letIndex].GetComponent<LetterSS>().isActivated)
        {
            Destroy(clonePrefab[letIndex]);
            prefab[letIndex].GetComponent<LetterSS>().isActivated = true;
            letIndex = Random.Range(0,size);
            Spawn();
        }  
    }

    void Spawn()
    {
        //get the location of the desired letter spawn   
        location = spawnLocation.transform.position;

        //clone the desired prefab object to spawn at the desired location
        clonePrefab[letIndex] = Instantiate(prefab[letIndex], location, Quaternion.Euler(0,0,0)) as GameObject;
    }


}

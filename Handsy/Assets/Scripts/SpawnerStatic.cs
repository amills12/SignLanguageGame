using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerStatic : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject[] prefab;
    public GameObject[] clonePrefab;
    public Vector3 location;
    LetterSS currLetter;
    int letterIndex = 0; //keep track of the current letter.
    void Start()
    {
        Spawn(letterIndex);
    }

    void Update()
    {
        
    }

    void Spawn(int i)
    {
        location = spawnLocation.transform.position;
        clonePrefab[i] = Instantiate(prefab[i], location, Quaternion.Euler(0,0,0)) as GameObject;
        // //Generate a random character
        // prefab_num = Random.Range(0,9);
        // //Get a random radius and rotation for the spawn location
        // spawn_location = RandomRadius(center, spawn_distance);
        // Instantiate(prefab[prefab_num], spawn_location, transform.rotation);
    }


}

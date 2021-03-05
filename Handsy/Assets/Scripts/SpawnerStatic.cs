using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerStatic : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject[] prefab;
    public GameObject[] clonePrefab;
    public Vector3 location;

    public int size = 36;
    public int letIndex = 0; //keep track of the current letter.
    
    void Start()
    {
        letIndex = Random.Range(0,size);
        Spawn();
    }

    void Update()
    {
        if(!clonePrefab[letIndex].GetComponent<LetterSS>().isActivated)
        {
            Destroy(clonePrefab[letIndex]);
            clonePrefab[letIndex].GetComponent<LetterSS>().isActivated = true;
            letIndex = Random.Range(0,size);
            Spawn();
        }  
    }

    void Spawn()
    {
        
        location = spawnLocation.transform.position;       
        clonePrefab[letIndex] = Instantiate(prefab[letIndex], location, Quaternion.Euler(0,0,0)) as GameObject;
        
        // //Generate a random character
        // prefab_num = Random.Range(0,9);
    }


}

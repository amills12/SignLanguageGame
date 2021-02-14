using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    public Transform[] spawns;
    public float repeatTime;
    int spawn_num, prefab_num;

    void Start(){
        InvokeRepeating("Spawn", 0, repeatTime);
    }

    void Spawn(){
        prefab_num = Random.Range(0,9);
        spawn_num = Random.Range(0,7);
        Instantiate(prefab[prefab_num], spawns[spawn_num].position, spawns[spawn_num].rotation);
    }

}

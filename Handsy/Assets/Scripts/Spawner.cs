using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    //public Transform[] spawns;
    public float repeatTime, spawn_distance;
    int /*spawn_num,*/ prefab_num;
    Vector3 spawn_location, center;
    Quaternion rotation;

    void Start(){
        InvokeRepeating("Spawn", 0, repeatTime);
        center = transform.position;
    }

    void Spawn(){
        //Generate a random character
        prefab_num = Random.Range(0,9);
        //Get a random radius and rotation for the spawn location
        spawn_location = RandomRadius(center, spawn_distance);
        rotation = Quaternion.FromToRotation(Vector3.up, center-spawn_location);
        Instantiate(prefab[prefab_num], spawn_location, transform.rotation);
    }

    Vector3 RandomRadius(Vector3 vector, float radius){
        Vector3 temp;
        /*  get a random 360 degree angle and convert to radians    */
        float angle = (Random.value * 360) * Mathf.Deg2Rad;   
        /*  create a vector using sine and cosine from a random angle and the radius, leave z alone */
        temp.x = vector.x + radius * Mathf.Sin(angle);
        temp.y = vector.y + radius * Mathf.Cos(angle);
        temp.z = vector.z;
        return temp;
    }

}

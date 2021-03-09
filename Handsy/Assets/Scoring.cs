using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public Text scoreText;
    public GameObject getStatus;
    int myStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myStatus = getStatus.GetComponent<SpawnerStatic>().score;
        scoreText.text = myStatus + "/30";
    }
}

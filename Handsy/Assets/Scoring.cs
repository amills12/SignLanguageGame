using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public Text scoreText;
    public GameObject getStatus;
    int myStatus;

    // Update is called once per frame
    void Update()
    {
        myStatus = getStatus.GetComponent<SpawnerStatic>().score;
        scoreText.text = myStatus + "/30";
    }
}

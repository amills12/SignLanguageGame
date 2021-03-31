using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScore : MonoBehaviour
{
    void Awake()
    {
        //Set the current score on awake
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score") + "";
    }
}

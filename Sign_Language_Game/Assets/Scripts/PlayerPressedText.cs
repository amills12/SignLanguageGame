using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPressedText : MonoBehaviour
{

    public string value;

    void Update()
    {
        GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt(value) + "";
    }
}

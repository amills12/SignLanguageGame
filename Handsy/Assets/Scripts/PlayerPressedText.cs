using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPressedText : MonoBehaviour
{

    public string value;

    void Update()
    {
        if(this.gameObject.name == "Score")
            GetComponent<Text>().text = PlayerPrefs.GetInt(value) + "";
        else if(this.gameObject.name == "Streak")
            GetComponent<Text>().text = PlayerPrefs.GetInt(value) + "";
        else if(this.gameObject.name == "Multiplier")
            GetComponent<Text>().text = PlayerPrefs.GetInt(value) + "x";
    }
}

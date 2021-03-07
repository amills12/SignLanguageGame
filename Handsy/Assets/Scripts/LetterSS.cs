using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LetterSS : MonoBehaviour
{
    public bool isActivated = true;//bool to determine if the letter is activated
    public KeyCode key; //assign keycode to specific letter
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false; //remove font visibility
            GetComponent<SpriteRenderer>().enabled = false; //remove hand model visibility
            isActivated = false; //deactivate letter
        }
    }

}


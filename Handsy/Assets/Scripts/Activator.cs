﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode key;
    bool active = false;
    GameObject note;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key) && active)
            Destroy(note);

    }

    void OnTriggerEnter2D(Collider2D other) {
        active = true;
        if(other.gameObject.tag == "Note")
            note = other.gameObject;
               
    }

    void OnTriggerExit2D(Collider2D other) {
        active = false;
    }
}
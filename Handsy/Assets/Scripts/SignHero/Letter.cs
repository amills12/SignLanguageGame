﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    Rigidbody rb;
    public float speed, curve;
    Transform target;
    Vector3 target_vector;
    public bool hit = false, active = false;

    //RGB variables
    [Range(0f, 1f)] public float lerpTime;
    public Color[] colors;
    int length, colorIndex = 0;
    MeshRenderer rend;
    float time = 0f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();  //capture rigidbody to maybe use later
        target = GameObject.FindWithTag("Activator").transform; //find the activator to move towards
        target_vector.x = target.position.x - 0.2f;
        target_vector.y = target.position.y - 0.35f;
        target_vector.z = target.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        length = colors.Length;
        curve = Random.Range(-1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hit){
            /*  Color.Lerp is a function that allows for colors to smoothly transistion, the time that this
                occurs over is dictated by the lerpTime variable, changable within the Inspector of the letters.    
            */
            rend.material.color = Color.Lerp(rend.material.color, colors[colorIndex], lerpTime*Time.deltaTime);
            time = Mathf.Lerp(time, 1f, lerpTime*Time.deltaTime);
            //Check the timeframe and if greater than 0.9, increment the colorIndex as long as the array isn't finished
            if(time > 0.9f){
                time = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= length) ? (length-1) : colorIndex;
            }

            float step = speed * Time.deltaTime;    //calculate the distance to move
            transform.position = Vector3.MoveTowards(transform.position, target_vector, step);    //move towards activator

            /*  This is attempt at adding curvature the character movements, might bring back in future.
                if(Vector3.Distance(transform.position, target.position) > 1.0f)
                    rb.velocity = new Vector3(curve, 0, 0);  //add some curve for funzies
            */
        }

        //check if the distance between the letter and activator is approximately equal
        if(Vector3.Distance(transform.position, target_vector) <= 0.1f){
            //swap the position to stay centered.
            hit = true;
            target_vector *= -1.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    Rigidbody rb;
    public float speed, curve;
    Transform target;
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
            rend.material.color = Color.Lerp(rend.material.color, colors[colorIndex], lerpTime*Time.deltaTime);
            time = Mathf.Lerp(time, 1f, lerpTime*Time.deltaTime);
            if(time > 0.9f){
                time = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= length) ? (length-1) : colorIndex;
            }

            float step = speed * Time.deltaTime;    //calculate the distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);    //move towards activator
            // if(Vector3.Distance(transform.position, target.position) > 1.0f)
            //     rb.velocity = new Vector3(curve, 0, 0);  //add some curve for funzies

        }

        //check if the distance between the letter and activator is approximately equal
        if(Vector3.Distance(transform.position, target.position) == 0){
            //swap the position
            hit = true;
            target.position *= -1.0f;
        }
    }
}

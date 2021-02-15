using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    Rigidbody rb;
    public float speed, curve;
    Transform target;
    public bool hit = false, active = false;

    private void Awake() {
        rb = GetComponent<Rigidbody>();  //capture rigidbody to maybe use later
        target = GameObject.FindWithTag("Activator").transform; //find the activator to move towards
    }

    // Start is called before the first frame update
    void Start()
    {
        //curve = Random.Range(0.7f, -0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!hit){
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

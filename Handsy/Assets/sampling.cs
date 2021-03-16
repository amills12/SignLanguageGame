using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
public class sampling : MonoBehaviour
{
    //Attach hand model to this
    public HandModelBase HandModel = null;
    public int counter = 0;
    public int limit = 2500;
    string character = null;
    bool enable = false;
    
    float[] thumbVal = {0f,0f,0f}, indexVal = {0f,0f,0f}, middleVal = {0f,0f,0f}, ringVal = {0f,0f,0f}, pinkyVal = {0f,0f,0f};

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        string old_character = character;
        //check keys
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode)){
                character = kcode.ToString();
            }
        }

        //if a new key has been pressed
        if(character != old_character){
            enable = true;
            counter = 0;
            for(int i = 0; i < thumbVal.Length; i++){
                thumbVal[i] = 0f;
                indexVal[i] = 0f;
                middleVal[i] = 0f;
                ringVal[i] = 0f;
                pinkyVal[i] = 0f;
            }
            Debug.Log("Character: " + character);
        }

        if(enable){
            StartCoroutine("Sample");
        }

        if(enable && (counter == limit)){
            Print();
            enable = false;
        }
    }

    IEnumerator Sample(){
        Hand hand;

        //print to console
        if(HandModel != null && HandModel.IsTracked){
            hand = HandModel.GetLeapHand();
            if(hand != null){
                //capture 100 samples for all 5 fingers
                thumbVal[0] += Vector3.Distance(hand.Fingers[0].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                indexVal[0] += Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                middleVal[0] += Vector3.Distance(hand.Fingers[2].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                ringVal[0] += Vector3.Distance(hand.Fingers[3].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                pinkyVal[0] += Vector3.Distance(hand.Fingers[4].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                //Add in the min/maxes here
            }
        }
        counter++;
        yield return new WaitForSeconds(0.01f);
    }

    void Print(){
        //print to console the averages
        Debug.Log("Thumb Position Average: " + (thumbVal[0]/(float)counter));
        Debug.Log("Index Position Average: " + (indexVal[0]/(float)counter));
        Debug.Log("Middel Position Average: " + (middleVal[0]/(float)counter));
        Debug.Log("Ring Position Average: " + (ringVal[0]/(float)counter));
        Debug.Log("Pinky Position Average: " + (pinkyVal[0]/(float)counter));
    }
}

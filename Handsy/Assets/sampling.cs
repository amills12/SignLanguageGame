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
    
    // First value is sum (used for average), second is min and last is max
    float[] thumbVal = {0f,0f,0f}, indexVal = {0f,0f,0f}, middleVal = {0f,0f,0f}, ringVal = {0f,0f,0f}, pinkyVal = {0f,0f,0f};

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        string old_character = character;
        // Check keys
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode)){
                character = kcode.ToString();
            }
        }

        // If a new key has been pressed
        if(character != old_character){
            enable = true;
            counter = 0;
            for(int i = 0; i < thumbVal.Length; i++){
                if(i == 1){
                    thumbVal[i] = 100f;
                    indexVal[i] = 100f;
                    middleVal[i] = 100f;
                    ringVal[i] = 100f;
                    pinkyVal[i] = 100f;
                }else{
                    thumbVal[i] = 0f;
                    indexVal[i] = 0f;
                    middleVal[i] = 0f;
                    ringVal[i] = 0f;
                    pinkyVal[i] = 0f;
                }
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
        float curThumbVal = 0f;
        float curIndexVal = 0f;
        float curMiddleVal = 0f;
        float curRingVal = 0f;
        float curPinkyVal = 0f;

        // Print to console
        if(HandModel != null && HandModel.IsTracked){
            hand = HandModel.GetLeapHand();
            if(hand != null){
                // Capture 100 samples for all 5 fingers
                curThumbVal = Vector3.Distance(hand.Fingers[0].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                curIndexVal = Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                curMiddleVal = Vector3.Distance(hand.Fingers[2].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                curRingVal = Vector3.Distance(hand.Fingers[3].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                curPinkyVal = Vector3.Distance(hand.Fingers[4].TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
                
                // Add to sum of values
                thumbVal[0] += curThumbVal;
                indexVal[0] += curIndexVal;
                middleVal[0] += curMiddleVal;
                ringVal[0] += curRingVal;
                pinkyVal[0] += curPinkyVal;

                // Add in the min/maxes
                if (curThumbVal < thumbVal[1]){
                    thumbVal[1] = curThumbVal;
                }
                if (curThumbVal > thumbVal[2]){
                    thumbVal[2] = curThumbVal;
                }

                if (curIndexVal < indexVal[1]){
                    indexVal[1] = curIndexVal;
                }
                if (curIndexVal > indexVal[2]){
                    indexVal[2] = curIndexVal;
                }

                if (curMiddleVal < middleVal[1]){
                    middleVal[1] = curMiddleVal;
                }
                if (curMiddleVal > middleVal[2]){
                    middleVal[2] = curMiddleVal;
                }

                if (curRingVal < ringVal[1]){
                    ringVal[1] = curRingVal;
                }
                if (curRingVal > ringVal[2]){
                    ringVal[2] = curRingVal;
                }

                if (curPinkyVal < pinkyVal[1]){
                    pinkyVal[1] = curPinkyVal;
                }
                if (curPinkyVal > pinkyVal[2]){
                    pinkyVal[2] = curPinkyVal;
                }
                
            }
        }
        counter++;
        yield return new WaitForSeconds(0.01f);
    }

    void Print(){
        // Print to console the averages
        Debug.Log("Thumb Position Average: " + (thumbVal[0]/(float)counter));
        Debug.Log("Thumb Position Minimum: " + thumbVal[1]);
        Debug.Log("Thumb Position Maximum: " + thumbVal[2]);
        Debug.Log("Index Position Average: " + (indexVal[0]/(float)counter));
        Debug.Log("Index Position Minimum: " + indexVal[1]);
        Debug.Log("Index Position Maximum: " + indexVal[2]);
        Debug.Log("Middle Position Average: " + (middleVal[0]/(float)counter));
        Debug.Log("Middle Position Minimum: " + middleVal[1]);
        Debug.Log("Middle Position Maximum: " + middleVal[2]);
        Debug.Log("Ring Position Average: " + (ringVal[0]/(float)counter));
        Debug.Log("Ring Position Minimum: " + ringVal[1]);
        Debug.Log("Ring Position Maximum: " + ringVal[2]);
        Debug.Log("Pinky Position Average: " + (pinkyVal[0]/(float)counter));
        Debug.Log("Pinky Position Minimum: " + pinkyVal[1]);
        Debug.Log("Pinky Position Maximum: " + pinkyVal[2]);
    }
}

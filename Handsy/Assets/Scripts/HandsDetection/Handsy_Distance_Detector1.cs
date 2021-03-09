using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

  public class Handsy_Distance_Detector1 : Detector {

    /* Sign Hero Connection Variables */
    ActivatorSphere activatorSphere;
    public bool activated = false;
    public float Period = .1f; //seconds
    private IEnumerator watcherCoroutine;
    private bool distanceWatcherState = false;

    //Attach hand model to this
    public HandModelBase HandModel = null;

    //structure for finger distance comparisons
    struct FingerDistances{
      public float[] Thumb;
      public float[] Index;
      public float[] Middle;
      public float[] Ring;
      public float[] Pinky;
    }
    
    //fingerDistances.Thumb[(int)fing.min] = 1;
    FingerDistances fingerDistances = new FingerDistances();

    //enums for min/max
    enum fing{ min, max }

    DistanceScript distanceScript;

    void Awake(){
      watcherCoroutine = watcher();
      //Find and awake the library
      distanceScript = GameObject.FindGameObjectWithTag("Distances").GetComponent<DistanceScript>();
      distanceScript.Awake();
      //Find activator for object passing
      activatorSphere = GameObject.FindGameObjectWithTag("Activator").GetComponent<ActivatorSphere>();
  
    }

    void OnEnable () {
      StopCoroutine(watcherCoroutine);
      StartCoroutine(watcherCoroutine);
    }

    void OnDisable () {
      StopCoroutine(watcherCoroutine);
      distanceWatcherState = false;
      activated = false;
    }

    private bool matchFingerState(Finger finger, Hand hand, float min, float max){
      Debug.Log("In matchFingerState: min = " + min + ", max = " + max);
      float comparison = Vector3.Distance(finger.TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
      Debug.Log("Distance calculation: " + comparison);
      return (comparison >= min) && (comparison <= max);
    }

    IEnumerator watcher(){
      Hand hand;
      while(true){
          //Your logic to compute or check the current watchedValue goes here
        bool fingerState = false;
        if(HandModel != null && HandModel.IsTracked){
          hand = HandModel.GetLeapHand();
          if(hand != null){
            Debug.Log("Thumb Position - Palm: " + Vector3.Distance(hand.Fingers[0].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            Debug.Log("Index Position - Palm: " + Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            Debug.Log("Middel Position - Palm: " + Vector3.Distance(hand.Fingers[2].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            Debug.Log("Ring Position - Palm: " + Vector3.Distance(hand.Fingers[3].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            Debug.Log("Pinky Position - Palm: " + Vector3.Distance(hand.Fingers[4].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            if(fingerDistances.Thumb != null){
              Debug.Log("Going into matchFingerState");
              fingerState = matchFingerState(hand.Fingers[0], hand, fingerDistances.Thumb[(int)fing.min], fingerDistances.Thumb[(int)fing.max])
                && matchFingerState(hand.Fingers[1], hand, fingerDistances.Index[(int)fing.min], fingerDistances.Index[(int)fing.max])
                && matchFingerState(hand.Fingers[2], hand, fingerDistances.Middle[(int)fing.min], fingerDistances.Middle[(int)fing.max])
                && matchFingerState(hand.Fingers[3], hand, fingerDistances.Ring[(int)fing.min], fingerDistances.Ring[(int)fing.max])
                && matchFingerState(hand.Fingers[4], hand, fingerDistances.Pinky[(int)fing.min], fingerDistances.Pinky[(int)fing.max]);
            }
          }
          Debug.Log("fingerState = " + fingerState);

          if(HandModel.IsTracked && fingerState){
            Debug.Log("Activating");
            distanceWatcherState = true;
            Activate();
          }
          else if(!HandModel.IsTracked || !fingerState){
            Debug.Log("Deactivating");
            distanceWatcherState = false;
            Deactivate();
          }
        } else if (IsActive){
          distanceWatcherState = false;
        }
        yield return new WaitForSeconds(Period);
      }
    }

    void SetCurrentCharacter(char curChar){
      HandsyDistances newCurentCharacter;

      if (Char.IsDigit(curChar)){
        Debug.Log(curChar);
        newCurentCharacter = distanceScript.GetCharacter(curChar);
      } else if (Char.IsLetter(curChar)) {
        Debug.Log(curChar);
        newCurentCharacter = distanceScript.GetCharacter(curChar);
      } else{
        Debug.Log("Nullified");
        newCurentCharacter = null;
      }
      
      fingerDistances.Thumb = newCurentCharacter.getThumbArray();
      fingerDistances.Index = newCurentCharacter.getIndexArray();
      fingerDistances.Middle = newCurentCharacter.getMiddleArray();
      fingerDistances.Ring = newCurentCharacter.getRingArray();
      fingerDistances.Pinky = newCurentCharacter.getPinkyArray();
    }

    public void Update(){
      if(distanceWatcherState){
        activated = true;
      }else{
        activated = false;
      }
      SetCurrentCharacter(activatorSphere.key.ToCharArray()[0]);
    }
  }
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

  public class Handsy_Distance_Detector_Right : Detector {

    /* Sign Hero Connection Variables */
    ActivatorSphere activatorSphere;
    public bool activated = false;
    public float Period = .001f; //seconds
    private IEnumerator watcherCoroutine, watcherCoroutinePalmWatcher;
    private bool distanceWatcherState = false, palmWatcherState = false;

    //Angle variables for palm direction checks
    private float OnAngle = 45; // degrees
    private float OffAngle = 65; //degrees
    /* The palm direction state. */
    private PointingType PointingType;
    private Transform TargetObject;
    private Vector3 PointingDirection;

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

    RightDistanceScript rightDistanceScript;

    void Awake(){
      watcherCoroutine = watcher();
      watcherCoroutinePalmWatcher = palmWatcher();
      //Find and awake the library
      rightDistanceScript = GameObject.FindGameObjectWithTag("RightDistances").GetComponent<RightDistanceScript>();
      rightDistanceScript.Awake();
      //Find activator for object passing
      activatorSphere = GameObject.FindGameObjectWithTag("Activator").GetComponent<ActivatorSphere>();
  
    }

    void OnEnable () {
      StopCoroutine(watcherCoroutine);
      StartCoroutine(watcherCoroutine);
      StopCoroutine(watcherCoroutinePalmWatcher);
      StartCoroutine(watcherCoroutinePalmWatcher);
    }

    void OnDisable () {
      StopCoroutine(watcherCoroutine);
      StopCoroutine(watcherCoroutinePalmWatcher);
      palmWatcherState = false;
      distanceWatcherState = false;
      activated = false;
    }

    /*  Return true/false depending on if the fingers are within the vector bounds from map */
    private bool matchFingerState(Finger finger, Hand hand, float min, float max){
      //Debug.Log("In matchFingerState: min = " + min + ", max = " + max);
      float comparison = Vector3.Distance(finger.TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
      //Debug.Log("Distance calculation: " + comparison);
      return (comparison >= min) && (comparison <= max);
    }

    /*  Watcher function for detection of characters  */
    IEnumerator watcher(){
      Hand hand;
      while(true){
          //Your logic to compute or check the current watchedValue goes here
        bool fingerState = false;
        if(HandModel != null && HandModel.IsTracked){
          hand = HandModel.GetLeapHand();
          if(hand != null){
            // Debug.Log("Thumb Position - Palm: " + Vector3.Distance(hand.Fingers[0].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            // Debug.Log("Index Position - Palm: " + Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            // Debug.Log("Middel Position - Palm: " + Vector3.Distance(hand.Fingers[2].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            // Debug.Log("Ring Position - Palm: " + Vector3.Distance(hand.Fingers[3].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            // Debug.Log("Pinky Position - Palm: " + Vector3.Distance(hand.Fingers[4].TipPosition.ToVector3(), hand.PalmPosition.ToVector3()));
            if(fingerDistances.Thumb != null){
              fingerState = matchFingerState(hand.Fingers[0], hand, fingerDistances.Thumb[(int)fing.min], fingerDistances.Thumb[(int)fing.max])
                && matchFingerState(hand.Fingers[1], hand, fingerDistances.Index[(int)fing.min], fingerDistances.Index[(int)fing.max])
                && matchFingerState(hand.Fingers[2], hand, fingerDistances.Middle[(int)fing.min], fingerDistances.Middle[(int)fing.max])
                && matchFingerState(hand.Fingers[3], hand, fingerDistances.Ring[(int)fing.min], fingerDistances.Ring[(int)fing.max])
                && matchFingerState(hand.Fingers[4], hand, fingerDistances.Pinky[(int)fing.min], fingerDistances.Pinky[(int)fing.max]);
            }
          }

          if(HandModel.IsTracked && fingerState){
            // Debug.Log("Activating");
            distanceWatcherState = true;
            Activate();
          }
          else if(!HandModel.IsTracked || !fingerState){
            // Debug.Log("Deactivating");
            distanceWatcherState = false;
            Deactivate();
          }
        } else if (IsActive){
          distanceWatcherState = false;
        }
        yield return new WaitForSeconds(Period);
      }
    }

    /*  Palm direction functions  */
    private IEnumerator palmWatcher() {
      Hand hand;
      Vector3 normal;
      while(true){
        if(HandModel != null){
          hand = HandModel.GetLeapHand();
          if(hand != null){
            normal = hand.PalmNormal.ToVector3();
            float angleTo = Vector3.Angle(normal, selectedDirection(hand.PalmPosition.ToVector3()));
            //Debug.Log("angleTo: " + angleTo);
            if(angleTo <= OnAngle){
              palmWatcherState = true;
            } else if(angleTo > OffAngle) {
              palmWatcherState = false;
            }
          }
        }
        yield return new WaitForSeconds(Period);
      }
    }

    private Vector3 selectedDirection (Vector3 tipPosition) {
        switch (PointingType) {
            case PointingType.RelativeToHorizon:
                Quaternion cameraRot = Camera.main.transform.rotation;
                float cameraYaw = cameraRot.eulerAngles.y;
                Quaternion rotator = Quaternion.AngleAxis(cameraYaw, Vector3.up);
                return rotator * PointingDirection;
            case PointingType.RelativeToCamera:
                return Camera.main.transform.TransformDirection(PointingDirection);
            case PointingType.RelativeToWorld:
               return PointingDirection;
            case PointingType.AtTarget:
                if (TargetObject != null)
                   return TargetObject.position - tipPosition;
                else return Vector3.zero;
            default:
                return PointingDirection;
        }
    }

    // Function for setting a new character, called in update
    public void SetCurrentCharacter(char curChar){
      HandsyDistances newCurentCharacter;

      if (Char.IsDigit(curChar)){
 //       Debug.Log(curChar);
        newCurentCharacter = rightDistanceScript.GetCharacter(curChar);
      } else if (Char.IsLetter(curChar)) {
 //       Debug.Log(curChar);
        newCurentCharacter = rightDistanceScript.GetCharacter(curChar);
      } else{
 //       Debug.Log("Nullified");
        newCurentCharacter = null;
      }
      
      fingerDistances.Thumb = newCurentCharacter.getThumbArray();
      fingerDistances.Index = newCurentCharacter.getIndexArray();
      fingerDistances.Middle = newCurentCharacter.getMiddleArray();
      fingerDistances.Ring = newCurentCharacter.getRingArray();
      fingerDistances.Pinky = newCurentCharacter.getPinkyArray();

      PointingType = newCurentCharacter.getPointingType();
      TargetObject = newCurentCharacter.getTargetTransform();
      PointingDirection = newCurentCharacter.getPointingDirection();
      Debug.Log("Pointing Direction is " + PointingDirection);
    }

    public void Update(){
      if(distanceWatcherState && palmWatcherState){
        activated = true;
      }else{
        activated = false;
      }
      Debug.Log("distanceWatcherState: " + distanceWatcherState);
      Debug.Log("palmWatcherState: " + palmWatcherState);
      Debug.Log("activated: " + activated);
      SetCurrentCharacter(activatorSphere.key.ToCharArray()[0]);
    }
  }
  

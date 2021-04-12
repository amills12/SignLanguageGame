using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;
using UnityEngine.SceneManagement;

  public class Handsy_Distance_Detector_Left : Detector {

    /* Sign Hero Connection Variables */
    ActivatorSphere activatorSphere;
    SpawnerStatic spawnerRAM;
    char currentCharacter;
    string scene;

    public bool activated = false;
    public float Period = .001f; //seconds
    private IEnumerator watcherCoroutine, watcherCoroutinePalmWatcher, watcherCoroutineVelocity;
    private bool distanceWatcherState = false, palmWatcherState = false, velocityWatcherState = false;

    //Angle variables for palm direction checks
    private float OnAngle = 45; // degrees
    private float OffAngle = 65; //degrees
    /** The palm direction state. */
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
    
    FingerDistances fingerDistances = new FingerDistances();

    //enums for min/max
    enum fing{ min, max }

    LeftDistanceScript leftDistanceScript;

    void Awake(){
       //Capture scene name
      scene = SceneManager.GetActiveScene().name;

      watcherCoroutine = watcher();
      watcherCoroutinePalmWatcher = palmWatcher();
      watcherCoroutineVelocity = velocityWatcher();
      //Find and awake the library
      leftDistanceScript = GameObject.FindGameObjectWithTag("LeftDistances").GetComponent<LeftDistanceScript>();
      leftDistanceScript.Awake();
      //Find activator for object passing
      if(scene == "SignHero")
          activatorSphere = GameObject.FindGameObjectWithTag("Activator").GetComponent<ActivatorSphere>();
        else if (scene == "Repeat-After-Me")
          spawnerRAM = GameObject.FindGameObjectWithTag("RAMSpawner").GetComponent<SpawnerStatic>(); 
    } 

    void OnEnable () {
      StopCoroutine(watcherCoroutine);
      StartCoroutine(watcherCoroutine);
      StopCoroutine(watcherCoroutinePalmWatcher);
      StartCoroutine(watcherCoroutinePalmWatcher);
      StopCoroutine(watcherCoroutineVelocity);
      StartCoroutine(watcherCoroutineVelocity);
    }

    void OnDisable () {
      StopCoroutine(watcherCoroutine);
      StopCoroutine(watcherCoroutinePalmWatcher);
      StopCoroutine(watcherCoroutineVelocity);
      palmWatcherState = false;
      distanceWatcherState = false;
      velocityWatcherState = false;
      activated = false;
    }

    /*  Return true/false depending on if the fingers are within the vector bounds from map */
    private bool matchFingerState(Finger finger, Hand hand, float min, float max){
      float comparison = Vector3.Distance(finger.TipPosition.ToVector3(), hand.PalmPosition.ToVector3());
      return (comparison >= min) && (comparison <= max);
    }

    /*  Watcher function for detection of characters  */
    IEnumerator watcher(){
      Hand hand;
      while(true){
        bool fingerState = false;
        if(HandModel != null && HandModel.IsTracked){
          hand = HandModel.GetLeapHand();
          if(hand != null){
            if(fingerDistances.Thumb != null){
              fingerState = matchFingerState(hand.Fingers[0], hand, fingerDistances.Thumb[(int)fing.min], fingerDistances.Thumb[(int)fing.max])
                && matchFingerState(hand.Fingers[1], hand, fingerDistances.Index[(int)fing.min], fingerDistances.Index[(int)fing.max])
                && matchFingerState(hand.Fingers[2], hand, fingerDistances.Middle[(int)fing.min], fingerDistances.Middle[(int)fing.max])
                && matchFingerState(hand.Fingers[3], hand, fingerDistances.Ring[(int)fing.min], fingerDistances.Ring[(int)fing.max])
                && matchFingerState(hand.Fingers[4], hand, fingerDistances.Pinky[(int)fing.min], fingerDistances.Pinky[(int)fing.max]);
            }
          }

          if(HandModel.IsTracked && fingerState){
            distanceWatcherState = true;
            Activate();
          }
          else if(!HandModel.IsTracked || !fingerState){
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

    private IEnumerator velocityWatcher(){
        Hand hand;
        while(true){
            if(HandModel != null){
                hand = HandModel.GetLeapHand();
                if(hand != null){
                    velocityWatcherState = false;
                    if(currentCharacter == 'z'){
                        float timer = 0f;
                        Vector vel = hand.PalmVelocity;
                        WaitForSeconds wait = new WaitForSeconds(0.001f);
                        while(vel.x < 0.50){
                            // Debug.Log("Z Checkpoint 1");
                            // Debug.Log("x velocity: " + vel.x);
                            vel = hand.PalmVelocity;
                            yield return wait;
                            timer = timer + Time.deltaTime;
                            if(timer >= 1.4f)
                              break;
                        }
                        yield return wait;
                        vel = hand.PalmVelocity;
                        while((vel.y > -0.40) && (vel.x > -0.40)){
                            // Debug.Log("Z Checkpoint 2");
                            // Debug.Log("x velocity: " + vel.x);
                            // Debug.Log("y velocity: " + vel.y);
                            vel = hand.PalmVelocity;
                            yield return wait;
                            timer = timer + Time.deltaTime;
                            if(timer >= 1.4f)
                              break;
                        }
                        yield return wait;
                        vel = hand.PalmVelocity;
                        while(vel.x < 0.50){
                            // Debug.Log("Z Checkpoint 3");
                            // Debug.Log("x velocity: " + vel.x);
                            vel = hand.PalmVelocity;
                            yield return wait;
                            timer = timer + Time.deltaTime;
                            if(timer >= 1.4f)
                              break;
                        }
                        if(timer < 1.4f)
                          velocityWatcherState = true;
                    }else if (currentCharacter == 'j'){
                        float timer = 0f;
                        Vector vel = hand.PalmVelocity;
                        WaitForSeconds wait = new WaitForSeconds(0.001f);
                        while(vel.y > -0.50){
                            // Debug.Log("J Checkpoint 1");
                            // Debug.Log("y velocity: " + vel.y);
                            vel = hand.PalmVelocity;
                            yield return wait;
                            timer = timer + Time.deltaTime;
                            if(timer >= 1.4f)
                              break;
                        }
                        yield return wait;
                        vel = hand.PalmVelocity;
                        while(vel.x > -0.50){
                            // Debug.Log("J Checkpoint 2");
                            // Debug.Log("x velocity: " + vel.x);
                            vel = hand.PalmVelocity;
                            yield return wait;
                            timer = timer + Time.deltaTime;
                            if(timer >= 1.4f)
                              break;
                        }
                        if(timer < 1.4f)
                          velocityWatcherState = true;                    
                    }else{
                        velocityWatcherState = true;
                    }
                }
            }
            yield return new WaitForSeconds(Period);
        }
    }

    // Function for setting a new character, called in update
   public void SetCurrentCharacter(char curChar){
      HandsyDistances newCurentCharacter;

      if (Char.IsLetter(curChar) || (curChar == '0')){
          newCurentCharacter = leftDistanceScript.GetCharacter(curChar);
          fingerDistances.Thumb = newCurentCharacter.getThumbArray();
          fingerDistances.Index = newCurentCharacter.getIndexArray();
          fingerDistances.Middle = newCurentCharacter.getMiddleArray();
          fingerDistances.Ring = newCurentCharacter.getRingArray();
          fingerDistances.Pinky = newCurentCharacter.getPinkyArray();

          PointingType = newCurentCharacter.getPointingType();
          TargetObject = newCurentCharacter.getTargetTransform();
          PointingDirection = newCurentCharacter.getPointingDirection();
      } else{
          newCurentCharacter = null;
      }
    }

    public void Update(){
      //Get current scene name
      if(scene == "SignHero")
        currentCharacter = activatorSphere.key.ToCharArray()[0];
      else if (scene == "Repeat-After-Me")
        currentCharacter = spawnerRAM.characterKey;

      if(distanceWatcherState && palmWatcherState && velocityWatcherState){
        activated = true;
        velocityWatcherState = false;
      }else{
        activated = false;
      }
    
      SetCurrentCharacter(currentCharacter);
    }
  }

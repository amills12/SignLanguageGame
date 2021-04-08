/******************************************************************************
 * Copyright (C) Ultraleap, Inc. 2011-2020.                                   *
 *                                                                            *
 * Use subject to the terms of the Apache License 2.0 available at            *
 * http://www.apache.org/licenses/LICENSE-2.0, or another agreement           *
 * between Ultraleap and you, your company or other organization.             *
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap.Unity.Attributes;
using UnityEngine.SceneManagement;

namespace Leap.Unity {

  public class HandsyDetector : Detector {

    /* Sign Hero Connection Variables */
    ActivatorSphere activatorSphere;
    SpawnerStatic spawnerRAM;
    public bool activated = false;
    char currCharacter;
    
    GameObject Numbers;
    NumberScript numberScript;
    GameObject Letters;
    LetterScript letterScript;

    private HandsyCharacter currentCharacter;

    private bool extendedFingerWatcherState = false;
    private bool palmWatcherState = false;


    [Tooltip("The interval in seconds at which to check this detector's conditions.")]
    [Units("seconds")]
    [MinValue(0)]
    private float Period = .05f; //seconds

    [Tooltip("The hand model to watch. Set automatically if detector is on a hand.")]
    public HandModelBase HandModel = null;
  
    /** The extended finger states. */
    private PointingState Thumb;
    private PointingState Index;
    private PointingState Middle;
    private PointingState Ring;
    private PointingState Pinky;

    /** The palm direction state. */
    private PointingType PointingType;
    private Transform TargetObject;
    private Vector3 PointingDirection;


    private float OnAngle = 45; // degrees
    private float OffAngle = 65; //degrees

    private int MinimumExtendedCount = 0;
    private int MaximumExtendedCount = 5;
    
    /** Whether to draw the detector's Gizmos for debugging. (Not every detector provides gizmos.)
     * @since 4.1.2 
     */
    [Header("")]
    [Tooltip("Draw this detector's Gizmos, if any. (Gizmos must be on in Unity edtor, too.)")]
    private bool ShowGizmos = true;

    private IEnumerator watcherCoroutineExtendedFingerWatcher;
    private IEnumerator watcherCoroutinePalmWatcher;

    string scene;

    void OnValidate() {

      // Extended Finger
      int required = 0, forbidden = 0;
      PointingState[] stateArray = { Thumb, Index, Middle, Ring, Pinky };
      for(int i=0; i<stateArray.Length; i++) {
        var state = stateArray[i];
        switch (state) {
          case PointingState.Extended:
            required++;
            break;
          case PointingState.NotExtended:
            forbidden++;
            break;
          default:
            break;
        }
        MinimumExtendedCount = 0;
        MaximumExtendedCount = 5;
      }

      // Palm Direction
      if( OffAngle < OnAngle){
        OffAngle = OnAngle;
      }
    
    }

    void SetCurrentCharacter(char curChar){
      HandsyCharacter newCurentCharacter;

      if (Char.IsDigit(curChar)){
        newCurentCharacter = numberScript.GetNumber(curChar);
        Thumb = newCurentCharacter.getThumbExtension();
        Index = newCurentCharacter.getIndexExtension();
        Middle = newCurentCharacter.getMiddleExtension();
        Ring = newCurentCharacter.getRingExtension();
        Pinky = newCurentCharacter.getPinkyExtension();

        PointingType = newCurentCharacter.getPointingType();
        TargetObject = newCurentCharacter.getTargetTransform();
        PointingDirection = newCurentCharacter.getPointingDirection();

        currentCharacter = newCurentCharacter;
      } else{
        newCurentCharacter = null;
      }
    }

    char getNiceNumKey(KeyCode keycode){
      if (keycode.ToString().Contains("Alpha")){
        return keycode.ToString().Substring(5)[0];
      }else{
        return '0';
      }
    }

    void Awake () {
      //Capture scene name
      scene = SceneManager.GetActiveScene().name;
      
      watcherCoroutineExtendedFingerWatcher = extendedFingerWatcher();
      watcherCoroutinePalmWatcher = palmWatcher();

      Numbers = GameObject.FindGameObjectWithTag("Numbers");
      numberScript = Numbers.GetComponent<NumberScript>();
      numberScript.Awake();

      Letters = GameObject.FindGameObjectWithTag("Letters");
      letterScript = Letters.GetComponent<LetterScript>();
      letterScript.Awake();

      //SetCurrentCharacter('1');
      
      if(scene == "SignHero")
        activatorSphere = GameObject.FindGameObjectWithTag("Activator").GetComponent<ActivatorSphere>();
      else if (scene == "Repeat-After-Me")
        spawnerRAM = GameObject.FindGameObjectWithTag("RAMSpawner").GetComponent<SpawnerStatic>();
    }
  
    void OnEnable () {
      StartCoroutine(watcherCoroutineExtendedFingerWatcher);
      StartCoroutine(watcherCoroutinePalmWatcher);
    }
  
    void OnDisable () {
      StopCoroutine(watcherCoroutineExtendedFingerWatcher);
      StopCoroutine(watcherCoroutinePalmWatcher);
      extendedFingerWatcherState = false;
      palmWatcherState = false;
    }
  
    IEnumerator extendedFingerWatcher() {
      Hand hand;
      while(true){
        bool fingerState = false;
        if(HandModel != null && HandModel.IsTracked){
          hand = HandModel.GetLeapHand();
          if(hand != null){
            fingerState = matchFingerState(hand.Fingers[0], Thumb)
              && matchFingerState(hand.Fingers[1], Index)
              && matchFingerState(hand.Fingers[2], Middle)
              && matchFingerState(hand.Fingers[3], Ring)
              && matchFingerState(hand.Fingers[4], Pinky);

            int extendedCount = 0;
            for (int f = 0; f < 5; f++) {
              if (hand.Fingers[f].IsExtended) {
                extendedCount++;
              }
            }
            fingerState = fingerState && 
                         (extendedCount <= MaximumExtendedCount) && 
                         (extendedCount >= MinimumExtendedCount);
            if(HandModel.IsTracked && fingerState){
              extendedFingerWatcherState = true;

            } else if(!HandModel.IsTracked || !fingerState) {
              extendedFingerWatcherState = false;
            }
          }
        } else if(IsActive){
          extendedFingerWatcherState = false;
        }
        yield return new WaitForSeconds(Period);
      }
    }

    private bool matchFingerState (Finger finger, PointingState requiredState) {
      return (requiredState == PointingState.Either) ||
             (requiredState == PointingState.Extended && finger.IsExtended) ||
             (requiredState == PointingState.NotExtended && !finger.IsExtended);
    }

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

    #if UNITY_EDITOR
    void OnDrawGizmos () {
      if (ShowGizmos && HandModel != null && HandModel.IsTracked) {
        PointingState[] state = { Thumb, Index, Middle, Ring, Pinky };
        Hand hand = HandModel.GetLeapHand();
        int extendedCount = 0;
        int notExtendedCount = 0;
        for (int f = 0; f < 5; f++) {
          Finger finger = hand.Fingers[f];
          if (finger.IsExtended) extendedCount++;
          else notExtendedCount++;
          if (matchFingerState(finger, state[f]) && 
             (extendedCount <= MaximumExtendedCount) && 
             (extendedCount >= MinimumExtendedCount)) {
            Gizmos.color = OnColor;
          } else {
            Gizmos.color = OffColor;
          }
          Gizmos.DrawWireSphere(finger.TipPosition.ToVector3(), finger.Width);
        }
      }
    }
    #endif

    public void Update ()
    {
      //Get current scene name
      if(scene == "SignHero")
        currCharacter = activatorSphere.key.ToCharArray()[0];
      else if (scene == "Repeat-After-Me")
        currCharacter = spawnerRAM.characterKey;

      //What does this do?
      if (extendedFingerWatcherState && palmWatcherState){
        Activate();
        activated = true;
      }else{
        Deactivate();
        activated = false;
      }
      // foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
      // {
      //   if (Input.GetKeyDown(kcode)){
      //     if (kcode.ToString().Contains("Alpha")){
      //       Debug.Log("Number");
      //       SetCurrentCharacter(getNiceNumKey(kcode));
      //     }else if(kcode.ToString().Length == 1){
      //       Debug.Log("Letter");
      //       Debug.Log(kcode.ToString().ToLower()[0]);
      //       SetCurrentCharacter(kcode.ToString().ToLower()[0]);
      //     }
      //     Debug.Log(currentCharacter.id);
      //   }
      // }
      // Debug.Log("Character from ActivatorSphere: " + activatorSphere.key.ToCharArray()[0]);
      SetCurrentCharacter(currCharacter);
    }
  }
}

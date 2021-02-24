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

namespace Leap.Unity {

  public class NumberOneDetector : Detector {
    GameObject Numbers;
    NumberScript numberScript;

    public string currentKey;
    public Number currentNumber;  

    public bool extendedFingerWatcherState = false;
    public bool palmWatcherState = false;


    [Tooltip("The interval in seconds at which to check this detector's conditions.")]
    [Units("seconds")]
    [MinValue(0)]
    public float Period = .1f; //seconds

    [Tooltip("The hand model to watch. Set automatically if detector is on a hand.")]
    public HandModelBase HandModel = null;
  
    /** The extended finger states. */
    public PointingState Thumb;
    public PointingState Index;
    public PointingState Middle;
    public PointingState Ring;
    public PointingState Pinky;

    /** The palm direction state. */
    private PointingType PointingType;
    private Vector3 PointingDirection;


    [Tooltip("The angle in degrees from the target direction at which to turn on.")]
    [Range(0, 180)]
    public float OnAngle = 45; // degrees

    [Tooltip("The angle in degrees from the target direction at which to turn off.")]
    [Range(0, 180)]
    public float OffAngle = 65; //degrees

    /** How many fingers must be extended for the detector to activate. */
    [Header("Min and Max Finger Counts")]
    [Range(0,5)]
    [Tooltip("The minimum number of fingers extended.")]
    public int MinimumExtendedCount = 0;
    /** The most fingers allowed to be extended for the detector to activate. */
    [Range(0, 5)]
    [Tooltip("The maximum number of fingers extended.")]
    public int MaximumExtendedCount = 5;
    /** Whether to draw the detector's Gizmos for debugging. (Not every detector provides gizmos.)
     * @since 4.1.2 
     */
    [Header("")]
    [Tooltip("Draw this detector's Gizmos, if any. (Gizmos must be on in Unity edtor, too.)")]
    public bool ShowGizmos = true;

    private IEnumerator watcherCoroutineExtendedFingerWatcher;
    private IEnumerator watcherCoroutinePalmWatcher;

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

    void SetNumber(Number num){
      Thumb = num.getThumbExtension();
      Index = num.getIndexExtension();
      Middle = num.getMiddleExtension();
      Ring = num.getRingExtension();
      Pinky = num.getPinkyExtension();

      PointingType = num.getPointingType();
      PointingDirection = num.getPointingDirection();

      Debug.Log(PointingType);
      Debug.Log(PointingDirection);
    }

      char getNiceNumKey(KeyCode keycode){
        if (keycode.ToString().Contains("Alpha")){
          return keycode.ToString().Substring(5)[0];
        }else{
          return '0';
        }
      }

    void Awake () {
      watcherCoroutineExtendedFingerWatcher = extendedFingerWatcher();
      watcherCoroutinePalmWatcher = palmWatcher();

      List<GameObject> characters = new List<GameObject>();
      Numbers = GameObject.FindGameObjectWithTag("Numbers");
      numberScript = Numbers.GetComponent<NumberScript>();
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
      if (extendedFingerWatcherState && palmWatcherState){
        Debug.Log("Activated");
        Activate();
      }else{
        Deactivate();
      }
      foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
      {
        if (Input.GetKeyDown(kcode)){
          currentKey = kcode.ToString();
          currentNumber = numberScript.GetNumber(getNiceNumKey(kcode));
          Debug.Log(getNiceNumKey(kcode));
          SetNumber(currentNumber);
        }
      }
    }
  }
}

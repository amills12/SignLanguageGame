/******************************************************************************
 * Copyright (C) Ultraleap, Inc. 2011-2020.                                   *
 *                                                                            *
 * Use subject to the terms of the Apache License 2.0 available at            *
 * http://www.apache.org/licenses/LICENSE-2.0, or another agreement           *
 * between Ultraleap and you, your company or other organization.             *
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using Leap.Unity.Attributes;

namespace Leap.Unity {

  public class NumberOneDetector : Detector {

    public static GameObject Number1;
    public static Number1ScriptNew Number1Script;
    public static GameObject CharacterObjects;

    private bool extendedFingerWatcherState = false;
    private bool palmWatcherState = false;


    [Tooltip("The interval in seconds at which to check this detector's conditions.")]
    [Units("seconds")]
    [MinValue(0)]
    public float Period = .1f; //seconds

    [Tooltip("The hand model to watch. Set automatically if detector is on a hand.")]
    public HandModelBase HandModel = null;
  
    /** The required thumb state. */
    [Header("Finger States")]
    [Tooltip("Required state of the thumb.")]
    public PointingState Thumb = Number1Script.Thumb;
    /** The required index finger state. */
    [Tooltip("Required state of the index finger.")]
    public PointingState Index = Number1Script.Index;
    /** The required middle finger state. */
    [Tooltip("Required state of the middle finger.")]
    public PointingState Middle = Number1Script.Middle;
    /** The required ring finger state. */
    [Tooltip("Required state of the ring finger.")]
    public PointingState Ring = Number1Script.Ring;
    /** The required pinky finger state. */
    [Tooltip("Required state of the little finger.")]
    public PointingState Pinky = Number1Script.Pinky;


    [Header("Direction Settings")]
    [Tooltip("How to treat the target direction.")]
    public PointingType PointingType = PointingType.RelativeToHorizon;


    [Tooltip("The target direction.")]
    public Vector3 PointingDirection = Vector3.forward;


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
        MinimumExtendedCount = Math.Max(required, MinimumExtendedCount);
        MaximumExtendedCount = Math.Min(5 - forbidden, MaximumExtendedCount);
        MaximumExtendedCount = Math.Max(required, MaximumExtendedCount);
      }

      // Palm Direction

      if( OffAngle < OnAngle){
        OffAngle = OnAngle;
      }
    
    }

    void Awake () {
      watcherCoroutineExtendedFingerWatcher = extendedFingerWatcher();
      watcherCoroutinePalmWatcher = palmWatcher();
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
              Debug.Log("in efd watcher state true");

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
              Debug.Log("in palm watcher state true");
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
        Activate();
      }else{
        Deactivate();
      }
    }

        void Start()
    {
        Number1Script = Number1.GetComponent<Number1ScriptNew>();
    }

  }
}

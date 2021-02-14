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

  /**
   * Detects when specified fingers are in an extended or non-extended state.
   * 
   * You can specify whether each finger is extended, not extended, or in either state.
   * This detector activates when every finger on the observed hand meets these conditions.
   * 
   * If added to a HandModelBase instance or one of its children, this detector checks the
   * finger state at the interval specified by the Period variable. You can also specify
   * which hand model to observe explicitly by setting handModel in the Unity editor or 
   * in code.
   * 
   * @since 4.1.2
   */
  public class NumberOneDetector : Detector {

private bool extendedFingerWatcherState = false;
private bool palmWatcherState = false;

    /**
     * The interval at which to check finger state.
     * @since 4.1.2
     */
    [Tooltip("The interval in seconds at which to check this detector's conditions.")]
    [Units("seconds")]
    [MinValue(0)]
    public float Period = .1f; //seconds

    /**
     * The HandModelBase instance to observe. 
     * Set automatically if not explicitly set in the editor.
     * @since 4.1.2
     */
    [Tooltip("The hand model to watch. Set automatically if detector is on a hand.")]
    public HandModelBase HandModel = null;
  
    /** The required thumb state. */
    [Header("Finger States")]
    [Tooltip("Required state of the thumb.")]
    public PointingState Thumb = PointingState.Either;
    /** The required index finger state. */
    [Tooltip("Required state of the index finger.")]
    public PointingState Index = PointingState.Either;
    /** The required middle finger state. */
    [Tooltip("Required state of the middle finger.")]
    public PointingState Middle = PointingState.Either;
    /** The required ring finger state. */
    [Tooltip("Required state of the ring finger.")]
    public PointingState Ring = PointingState.Either;
    /** The required pinky finger state. */
    [Tooltip("Required state of the little finger.")]
    public PointingState Pinky = PointingState.Either;

    /**
     * Specifies how to interprete the direction specified by PointingDirection.
     * 
     * - RelativeToCamera -- the target direction is defined relative to the camera's forward vector, i.e. (0, 0, 1) is the cmaera's 
     *                       local forward direction.
     * - RelativeToHorizon -- the target direction is defined relative to the camera's forward vector, 
     *                        except that it does not change with pitch.
     * - RelativeToWorld -- the target direction is defined as a global direction that does not change with camera movement. For example,
     *                      (0, 1, 0) is always world up, no matter which way the camera is pointing.
     * - AtTarget -- a target object is used as the pointing direction (The specified PointingDirection is ignored).
     * 
     * In VR scenes, RelativeToHorizon with a direction of (0, 0, 1) for camera forward and RelativeToWorld with a direction
     * of (0, 1, 0) for absolute up, are often the most useful settings.
     * @since 4.1.2
     */
    [Header("Direction Settings")]
    [Tooltip("How to treat the target direction.")]
    public PointingType PointingType = PointingType.RelativeToHorizon;

    /**
    * The target direction as interpreted by the PointingType setting.
    * Ignored when Pointingtype is "AtTarget."
    * @since 4.1.2
    */
    [Tooltip("The target direction.")]
    [DisableIf("PointingType", isEqualTo: PointingType.AtTarget)]
    public Vector3 PointingDirection = Vector3.forward;

    /**
     * The object to point at when the PointingType is "AtTarget." Ignored otherwise.
     */
    [Tooltip("A target object(optional). Use PointingType.AtTarget")]
    [DisableIf("PointingType", isNotEqualTo: PointingType.AtTarget)]
    public Transform TargetObject = null;

    [Tooltip("The angle in degrees from the target direction at which to turn on.")]
    [Range(0, 180)]
    public float OnAngle = 45; // degrees

    /**
    * The turn-off angle. The detector deactivates when the palm points more than this
    * many degrees away from the target direction. The off angle must be larger than the on angle.
    * @since 4.1.2
    */
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

    private IEnumerator watcherCoroutine;

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
      watcherCoroutine = extendedFingerWatcher();
    }
  
    void OnEnable () {
      StartCoroutine(watcherCoroutine);
    }
  
    void OnDisable () {
      StopCoroutine(watcherCoroutine);
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

    public virtual void Activate(){
      if (!IsActive) {
        _isActive = true;
        OnActivate.Invoke();
      }
    }

    /**
    * Invoked when this detector deactivates.
    * Subclasses must call this function when the detector's conditions change from true to false.
    * @since 4.1.2
    */
    public virtual void Deactivate(){
      if (IsActive) {
        _isActive = false;
        OnDeactivate.Invoke();
      }
    }

    public void Update ()
    {
      if (extendedFingerWatcherState && palmWatcherState){
        Activate();
      }else{
        Deactivate();
      }
    }

  }
  
  /** Defines the settings for comparing extended finger states */
  public enum PointingState{Extended, NotExtended, Either}
}

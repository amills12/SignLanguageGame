using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number1ScriptNew : MonoBehaviour
{
    /** The required thumb state. */
    [Header("Finger States")]
    [Tooltip("Required state of the thumb.")]
    public Leap.Unity.PointingState Thumb = Leap.Unity.PointingState.NotExtended;
    /** The required index finger state. */
    [Tooltip("Required state of the index finger.")]
    public Leap.Unity.PointingState Index = Leap.Unity.PointingState.Extended;
    /** The required middle finger state. */
    [Tooltip("Required state of the middle finger.")]
    public Leap.Unity.PointingState Middle = Leap.Unity.PointingState.NotExtended;
    /** The required ring finger state. */
    [Tooltip("Required state of the ring finger.")]
    public Leap.Unity.PointingState Ring = Leap.Unity.PointingState.NotExtended;
    /** The required pinky finger state. */
    [Tooltip("Required state of the little finger.")]
    public Leap.Unity.PointingState Pinky = Leap.Unity.PointingState.NotExtended;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

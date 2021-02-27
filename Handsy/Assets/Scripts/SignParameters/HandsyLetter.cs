using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsyLetter : HandsyCharacter
{
    // Extra letter specific attributes can be intilized here.
    // Makse sure to add appropriate Detectors/Variables if new attributes are added here.

    public HandsyLetter(
        char id,
        Leap.Unity.PointingState Thumb, 
        Leap.Unity.PointingState Index, 
        Leap.Unity.PointingState Middle, 
        Leap.Unity.PointingState Ring, 
        Leap.Unity.PointingState Pinky,
        Leap.Unity.PointingType PointingType,
        Vector3 PointingDirection)
    {
        this.id = id;
        this.ThumbExtension = Thumb;
        this.IndexExtension = Index;
        this.MiddleExtension = Middle;
        this.RingExtension = Ring;
        this.PinkyExtension = Pinky;

        this.PointingType = PointingType;
        this.PointingDirection = PointingDirection;
    }
}

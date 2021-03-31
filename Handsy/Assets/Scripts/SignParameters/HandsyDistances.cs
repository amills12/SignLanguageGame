using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsyDistances
{
    // Default Constructor
    public HandsyDistances(
        char id,
        float[] Thumb,
        float[] Index,
        float[] Middle,
        float[] Ring,
        float[] Pinky,
        Leap.Unity.PointingType PointingType,
        Transform TargetTransform,
        Vector3 PointingDirection
    ){
        this.id = id;
        this.Thumb = Thumb;
        this.Index = Index;
        this.Middle = Middle;
        this.Ring = Ring;
        this.Pinky = Pinky;

        this.PointingType = Leap.Unity.PointingType.RelativeToHorizon;
        this.TargetTransform = null;
        this.PointingDirection = PointingDirection;
    }

    // Protected Variables
    public char id;
    protected float[] Thumb;
    protected float[] Index;
    protected float[] Middle;
    protected float[] Ring;
    protected float[] Pinky;    
    protected Leap.Unity.PointingType PointingType = Leap.Unity.PointingType.RelativeToHorizon;
    protected Transform TargetTransform = null;
    protected Vector3 PointingDirection = Vector3.forward;

    // Getter Functions
    public float[] getThumbArray() { return Thumb; }
    public float[] getIndexArray() { return Index; }
    public float[] getMiddleArray() { return Middle; }
    public float[] getRingArray() { return Ring; }
    public float[] getPinkyArray() { return Pinky; }
    public Leap.Unity.PointingType getPointingType() { return PointingType; }
    public Transform getTargetTransform() { return TargetTransform; }
    public Vector3 getPointingDirection() { return PointingDirection; }
}

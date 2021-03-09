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
        float[] Pinky
    ){
        this.id = id;
        this.Thumb = Thumb;
        this.Index = Index;
        this.Middle = Middle;
        this.Ring = Ring;
        this.Pinky = Pinky;
    }

    // Protected Variables
    public char id;
    protected float[] Thumb;
    protected float[] Index;
    protected float[] Middle;
    protected float[] Ring;
    protected float[] Pinky;

    // Getter Functions
    public float[] getThumbArray() { return Thumb; }
    public float[] getIndexArray() { return Index; }
    public float[] getMiddleArray() { return Middle; }
    public float[] getRingArray() { return Ring; }
    public float[] getPinkyArray() { return Pinky; }
}

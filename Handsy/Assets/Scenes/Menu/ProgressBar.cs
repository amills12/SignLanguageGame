using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // Save the slider ref as a private variable
    private Slider slider;

    // Hold the value of the progress
    private float targetProgress = 0;

    // Grab the slider instance
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Temp function for adding progress
    public void IncrementProgress(float newProgress)
    {
        targetProgress += newProgress;
    }
}

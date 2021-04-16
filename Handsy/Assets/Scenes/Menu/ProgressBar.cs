using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    // Save the slider ref as a private variable
    private Slider slider;
    public TMP_Text percentVal;
    public string alphaOrNumStr, alphaOrNumFl;
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
        if(PlayerPrefs.GetFloat(alphaOrNumFl)> 0)
        {
            targetProgress = PlayerPrefs.GetFloat(alphaOrNumFl);
            slider.value = targetProgress/100f;
            percentVal.text = PlayerPrefs.GetString(alphaOrNumStr);
        }

        else
        {
            slider.value = 0;
            percentVal.text = "0%";
        }
    
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

        // Start is called before the first frame update
    public void resetProgress()
    {
        slider.value = 0;
        percentVal.text = "0%";
        PlayerPrefs.DeleteAll();
    }
}

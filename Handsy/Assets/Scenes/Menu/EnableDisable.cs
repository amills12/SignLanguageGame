using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public GameObject Scores;

    public void EnableScores()
    {
        Scores.SetActive(true);
    }

    public void DisableScores()
    {
        Scores.SetActive(false);
    }
}

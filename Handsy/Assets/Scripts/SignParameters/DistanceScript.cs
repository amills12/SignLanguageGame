using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceScript : MonoBehaviour
{    
    public SortedDictionary<char, HandsyDistances> rightLetters, leftLetters;
    public HandsyDistances GetCharacter(char character){
        if (rightLetters.ContainsKey(character)) {
            return rightLetters[character];
        }
        return null;
    }
    // Number data initialization 
    public void Awake(){
        rightLetters = new SortedDictionary<char, HandsyDistances>();

        // Letter 1
        rightLetters['a'] = new HandsyDistances(
            'a',
            new float[2] {0.06f, 0.07f},
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f}
        );
    } 
}

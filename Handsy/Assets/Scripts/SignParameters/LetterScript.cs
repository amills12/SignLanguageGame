using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity
{
    public class LetterScript : MonoBehaviour
    {
        public SortedDictionary<char, HandsyLetter> allLetters;

        public HandsyLetter GetLetter(char letter){
            if (allLetters.ContainsKey(letter)) {
                return allLetters[letter];
            }
            return null;
        }

        // Number data initialization 
        public void Awake(){
            allLetters = new SortedDictionary<char, HandsyLetter>();
            GameObject target = GameObject.FindGameObjectWithTag("LMC");
            Transform targetTransform = target.GetComponent<Transform>();

            // Letter 1
            allLetters['a'] = new HandsyLetter(
                'a',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.AtTarget,
                targetTransform,
                Vector3.up
            );
        }
    }
}
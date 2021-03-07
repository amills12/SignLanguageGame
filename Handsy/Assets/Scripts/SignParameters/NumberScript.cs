using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity
{
    public class NumberScript : MonoBehaviour
    {
        public SortedDictionary<char, HandsyNumber> allNumbers;

        public HandsyNumber GetNumber(char num){
            if (allNumbers.ContainsKey(num)) {
                return allNumbers[num];
            }
            return null;
        }

        // Number data initialization 
        public void Awake(){
            allNumbers = new SortedDictionary<char, HandsyNumber>();
            GameObject target = GameObject.FindGameObjectWithTag("LMC");
            Transform targetTransform = target.GetComponent<Transform>();

            // Number 0 - FAKE 0
            allNumbers['0'] = new HandsyNumber(
                '0',
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                Vector3.up
            );

            // Number 1
            allNumbers['1'] = new HandsyNumber(
                '1',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.back
            );

            // Number 2
            allNumbers['2'] = new HandsyNumber(
                '2',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.back
            );

            // Number 3
            allNumbers['3'] = new HandsyNumber(
                '3',
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.back
            );

            // Number 4
            allNumbers['4'] = new HandsyNumber(
                '4',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.back
            );

            // Number 5
            allNumbers['5'] = new HandsyNumber(
                '5',
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.back
            );

            // Number 6
            allNumbers['6'] = new HandsyNumber(
                '6',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.forward
            );
            
            // Number 7
            allNumbers['7'] = new HandsyNumber(
                '7',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.forward
            );

            // Number 8
            allNumbers['8'] = new HandsyNumber(
                '8',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.forward
            );

            // Number 9
            allNumbers['9'] = new HandsyNumber(
                '9',
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                targetTransform,
                Vector3.forward
            );
        }
    }
}
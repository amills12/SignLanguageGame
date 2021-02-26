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
                Debug.Log("FOUND");
                return allNumbers[num];
            }
            return null;
        }

        // Number data initialization 
        public void Awake(){
            allNumbers = new SortedDictionary<char, HandsyNumber>();

            // Number 1
            allNumbers['1'] = new HandsyNumber(
                '1',
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                Vector3.up
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
                Vector3.up
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
                Vector3.up
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
                Vector3.up
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
                Vector3.up
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
                Vector3.down
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
                Vector3.down
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
                Vector3.down
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
                Vector3.down
            );
        }
    }
}
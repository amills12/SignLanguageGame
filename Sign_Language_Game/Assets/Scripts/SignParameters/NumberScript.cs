using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity
{
    public class NumberScript : MonoBehaviour
    {
        // public List<Number> allNumbers;
        public SortedDictionary<char, Number> allNumbers;

        public Number GetNumber(char num){
            if (allNumbers.ContainsKey(num)) {
                return allNumbers[num];
            }
            return null;
        }

        void Awake(){
            allNumbers = new SortedDictionary<char, Number>();

            // Number 1
            allNumbers['1'] = new Number(
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                Vector3.up
            );

            // Number 2
            allNumbers['2'] = new Number(
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                Vector3.up
            );

            // Number 3
            allNumbers['3'] = new Number(
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                Vector3.up
            );

            // Number 4
            allNumbers['4'] = new Number(
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                Vector3.up
            );

            // Number 5
            allNumbers['5'] = new Number(
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                Vector3.up
            );

            // Number 6
            allNumbers['6'] = new Number(
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended,
                PointingType.RelativeToHorizon,
                Vector3.down
            );
            
            // Number 7
            allNumbers['7'] = new Number(
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                Vector3.down
            );

            // Number 8
            allNumbers['8'] = new Number(
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.NotExtended, 
                PointingState.Extended, 
                PointingState.Extended,
                PointingType.RelativeToHorizon,
                Vector3.down
            );

            // Number 9
            allNumbers['9'] = new Number(
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
    public class Number
    {
        // Constructor
        public Number(
            Leap.Unity.PointingState Thumb, 
            Leap.Unity.PointingState Index, 
            Leap.Unity.PointingState Middle, 
            Leap.Unity.PointingState Ring, 
            Leap.Unity.PointingState Pinky,
            Leap.Unity.PointingType PointingType,
            Vector3 PointingDirection)
        {
            this.ThumbExtension = Thumb;
            this.IndexExtension = Index;
            this.MiddleExtension = Middle;
            this.RingExtension = Ring;
            this.PinkyExtension = Pinky;

            this.PointingType = PointingType;
            this.PointingDirection = PointingDirection;
        }

        public int id;

        // Main parameters
        private Leap.Unity.PointingState ThumbExtension = Leap.Unity.PointingState.Either;
        private Leap.Unity.PointingState IndexExtension = Leap.Unity.PointingState.Either;
        private Leap.Unity.PointingState MiddleExtension = Leap.Unity.PointingState.Either;
        private Leap.Unity.PointingState RingExtension = Leap.Unity.PointingState.Either;
        private Leap.Unity.PointingState PinkyExtension = Leap.Unity.PointingState.Either;

        private Leap.Unity.PointingType PointingType = Leap.Unity.PointingType.RelativeToHorizon;
        private Vector3 PointingDirection = Vector3.forward;

        // Getters
        public Leap.Unity.PointingState getThumbExtension() { return ThumbExtension; }
        public Leap.Unity.PointingState getIndexExtension() { return IndexExtension; }
        public Leap.Unity.PointingState getMiddleExtension() { return MiddleExtension; }
        public Leap.Unity.PointingState getRingExtension() { return RingExtension; }
        public Leap.Unity.PointingState getPinkyExtension() { return PinkyExtension; }

        public Leap.Unity.PointingType getPointingType() { return PointingType; }
        public Vector3 getPointingDirection() { return PointingDirection; }
        
        //Setters
        public void setThumbExtension(Leap.Unity.PointingState newThumbExtension) { 
            ThumbExtension = newThumbExtension;
        }
        public void setIndexExtension(Leap.Unity.PointingState newIndexExtension) { 
            IndexExtension = newIndexExtension;
        }
        public void setMiddleExtension(Leap.Unity.PointingState newMiddleExtension) { 
            MiddleExtension = newMiddleExtension;
        }
        public void setRingExtension(Leap.Unity.PointingState newRingExtension) { 
            RingExtension = newRingExtension;
        }
        public void setPinkyExtension(Leap.Unity.PointingState newPinkyExtension) { 
            PinkyExtension = newPinkyExtension;
        }

        public void setPointingType(Leap.Unity.PointingType newPointingType) { 
            PointingType = newPointingType;
        }
        public void setPointingDirection(Vector3 newPointingDirection) { 
            PointingDirection = newPointingDirection;
        }
    }
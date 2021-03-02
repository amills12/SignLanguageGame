using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsyCharacter
    {
        // Default Constructor
        public HandsyCharacter()
        {
            this.ThumbExtension = Leap.Unity.PointingState.Either;
            this.IndexExtension = Leap.Unity.PointingState.Either;
            this.MiddleExtension = Leap.Unity.PointingState.Either;
            this.RingExtension = Leap.Unity.PointingState.Either;
            this.PinkyExtension = Leap.Unity.PointingState.Either;

            this.PointingType = Leap.Unity.PointingType.RelativeToHorizon;
            this.PointingDirection = Vector3.forward;
        }

        public char id;

        // Main parameters
        protected Leap.Unity.PointingState ThumbExtension = Leap.Unity.PointingState.Either;
        protected Leap.Unity.PointingState IndexExtension = Leap.Unity.PointingState.Either;
        protected Leap.Unity.PointingState MiddleExtension = Leap.Unity.PointingState.Either;
        protected Leap.Unity.PointingState RingExtension = Leap.Unity.PointingState.Either;
        protected Leap.Unity.PointingState PinkyExtension = Leap.Unity.PointingState.Either;

        protected Leap.Unity.PointingType PointingType = Leap.Unity.PointingType.RelativeToHorizon;
        protected Vector3 PointingDirection = Vector3.forward;

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
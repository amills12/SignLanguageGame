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
        GameObject target = GameObject.FindGameObjectWithTag("LMC");
        Transform targetTransform = target.GetComponent<Transform>();

        rightLetters['a'] = new HandsyDistances(
            'a',
            new float[2] {0.058f, 0.07f}, 
            new float[2] {0.035f, 0.047f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['b'] = new HandsyDistances(
            'b',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['c'] = new HandsyDistances(
            'c',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.left
        );
        rightLetters['d'] = new HandsyDistances(
            'd',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['e'] = new HandsyDistances(
            'e',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['f'] = new HandsyDistances(
            'f',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['g'] = new HandsyDistances(
            'g',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        rightLetters['h'] = new HandsyDistances(
            'h',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        rightLetters['i'] = new HandsyDistances(
            'i',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['j'] = new HandsyDistances(
            'j',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['k'] = new HandsyDistances(
            'k',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['l'] = new HandsyDistances(
            'l',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['m'] = new HandsyDistances(
            'm',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['n'] = new HandsyDistances(
            'n',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['o'] = new HandsyDistances(
            'o',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        rightLetters['p'] = new HandsyDistances(
            'p',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        rightLetters['q'] = new HandsyDistances(
            'q',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        rightLetters['r'] = new HandsyDistances(
            'r',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['s'] = new HandsyDistances(
            's',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['t'] = new HandsyDistances(
            't',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['u'] = new HandsyDistances(
            'u',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['v'] = new HandsyDistances(
            'v',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['w'] = new HandsyDistances(
            'w',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['x'] = new HandsyDistances(
            'x',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['y'] = new HandsyDistances(
            'y',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        rightLetters['z'] = new HandsyDistances(
            'z',
            new float[2] {0.06f, 0.07f}, 
            new float[2] {0.04f, 0.05f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            new float[2] {0.03f, 0.04f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
    } 
}

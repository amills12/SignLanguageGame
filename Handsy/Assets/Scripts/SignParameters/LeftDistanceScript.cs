using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDistanceScript : MonoBehaviour
{    
    public SortedDictionary<char, HandsyDistances> leftLetters;
    public HandsyDistances GetCharacter(char character){
        if (leftLetters.ContainsKey(character)) {
            return leftLetters[character];
        }
        return null;
    }
    // Number data initialization 
    public void Awake(){
        leftLetters = new SortedDictionary<char, HandsyDistances>();
        GameObject target = GameObject.FindGameObjectWithTag("LMC");
        Transform targetTransform = target.GetComponent<Transform>();

        leftLetters['a'] = new HandsyDistances(
            'a',
            new float[2] {0.05302392f, 0.06509027f}, 
            new float[2] {0.03698144f, 0.0534002f},
            new float[2] {0.03210405f, 0.03821216f},
            new float[2] {0.03163531f, 0.03679072f},
            new float[2] {0.0322264f, 0.03707606f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['b'] = new HandsyDistances(
            'b',
            new float[2] {0.03577588f, 0.05016045f}, 
            new float[2] {0.08093867f, 0.1048017f},
            new float[2] {0.09416926f, 0.1149734f},
            new float[2] {0.09147123f, 0.1067778f},
            new float[2] {0.06724428f, 0.08601554f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['c'] = new HandsyDistances(
            'c',
            new float[2] {0.06913237f, 0.07650723f}, 
            new float[2] {0.07579578f, 0.09538127f},
            new float[2] {0.07689285f, 0.09836242f},
            new float[2] {0.06791493f, 0.08725875f},
            new float[2] {0.05716303f, 0.06957792f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        leftLetters['d'] = new HandsyDistances(
            'd',
            new float[2] {0.05289517f, 0.06408359f}, 
            new float[2] {0.0885051f, 0.1012573f},
            new float[2] {0.05277849f, 0.07133035f},
            new float[2] {0.04639483f, 0.06289832f},
            new float[2] {0.03905994f, 0.05344276f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['e'] = new HandsyDistances(
            'e',
            new float[2] {0.03484658f, 0.05435661f}, 
            new float[2] {0.03375252f, 0.04446055f},
            new float[2] {0.03388847f, 0.04607131f},
            new float[2] {0.03250794f, 0.04514745f},
            new float[2] {0.03292542f, 0.04322043f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['f'] = new HandsyDistances(
            'f',
            new float[2] {0.05968376f, 0.08186876f}, 
            new float[2] {0.04610675f, 0.09317052f},
            new float[2] {0.08181529f, 0.1044967f},
            new float[2] {0.09172894f, 0.1005816f},
            new float[2] {0.07591204f, 0.08870451f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['g'] = new HandsyDistances(
            'g',
            new float[2] {0.058918f, 0.07161678f}, 
            new float[2] {0.07474881f, 0.111706f},
            new float[2] {0.03027427f, 0.0462727f},
            new float[2] {0.03022749f, 0.04249761f},
            new float[2] {0.03127758f, 0.0419693f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.down
        );
        leftLetters['h'] = new HandsyDistances(
            'h',
            new float[2] {0.05273569f, 0.0697866f}, 
            new float[2] {0.04306256f, 0.1044702f},
            new float[2] {0.09267862f, 0.1167784f},
            new float[2] {0.03190611f, 0.03980615f},
            new float[2] {0.03449216f, 0.04108002f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.back
        );
        leftLetters['i'] = new HandsyDistances(
            'i',
            new float[2] {0.04820498f, 0.062192f}, 
            new float[2] {0.03543192f, 0.04601194f},
            new float[2] {0.03732169f, 0.04880385f},
            new float[2] {0.03538175f, 0.04578407f},
            new float[2] {0.07211512f, 0.08976289f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['j'] = new HandsyDistances(
            'j',
            new float[2] {0.03823614f, 0.07968663f}, 
            new float[2] {0.03426574f, 0.08320998f},
            new float[2] {0.03114417f, 0.05244818f},
            new float[2] {0.03176742f, 0.04548024f},
            new float[2] {0.04263401f, 0.08512638f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['k'] = new HandsyDistances(
            'k',
            new float[2] {0.05066939f, 0.05985822f}, 
            new float[2] {0.09455667f, 0.104059f},
            new float[2] {0.08793048f, 0.1092215f},
            new float[2] {0.04073466f, 0.05013521f},
            new float[2] {0.03720642f, 0.04454974f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['l'] = new HandsyDistances(
            'l',
            new float[2] {0.08313951f, 0.103685f}, 
            new float[2] {0.0922984f, 0.1090637f},
            new float[2] {0.03765897f, 0.04470721f},
            new float[2] {0.03713861f, 0.04352567f},
            new float[2] {0.03524299f, 0.04120598f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['m'] = new HandsyDistances(
            'm',
            new float[2] {0.04584f, 0.05462674f}, 
            new float[2] {0.03401458f, 0.0393761f},
            new float[2] {0.03368248f, 0.03917431f},
            new float[2] {0.03299359f, 0.03913261f},
            new float[2] {0.03216263f, 0.03589288f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['n'] = new HandsyDistances(
            'n',
            new float[2] {0.04668895f, 0.05911779f}, 
            new float[2] {0.03339854f, 0.04635527f},
            new float[2] {0.02665133f, 0.04922039f},
            new float[2] {0.02791065f, 0.046744f},
            new float[2] {0.02816563f, 0.04033361f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['o'] = new HandsyDistances(
            'o',
            new float[2] {0.06191183f, 0.07558662f}, 
            new float[2] {0.05576184f, 0.101362f},
            new float[2] {0.05214009f, 0.08414167f},
            new float[2] {0.04959784f, 0.07814287f},
            new float[2] {0.04555092f, 0.06689141f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        leftLetters['p'] = new HandsyDistances(
            'p',
            new float[2] {0.03852916f, 0.06715829f}, 
            new float[2] {0.07084803f, 0.08989941f},
            new float[2] {0.03795689f, 0.08799083f},
            new float[2] {0.03206284f, 0.04715325f},
            new float[2] {0.03407437f, 0.04441945f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        leftLetters['q'] = new HandsyDistances(
            'q',
            new float[2] {0.05601886f, 0.07462818f}, 
            new float[2] {0.07407044f, 0.095404f},
            new float[2] {0.03371277f, 0.03925059f},
            new float[2] {0.03294385f, 0.0365252f},
            new float[2] {0.03358416f, 0.03730578f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.right
        );
        leftLetters['r'] = new HandsyDistances(
            'r',
            new float[2] {0.03616431f, 0.0556614f}, 
            new float[2] {0.07663275f, 0.1011888f},
            new float[2] {0.03244827f, 0.09655226f},
            new float[2] {0.02786258f, 0.04580658f},
            new float[2] {0.03218005f, 0.04111487f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['s'] = new HandsyDistances(
            's',
            new float[2] {0.04896477f, 0.06068249f}, 
            new float[2] {0.03604595f, 0.04549649f},
            new float[2] {0.03177527f, 0.03703211f},
            new float[2] {0.03063347f, 0.0356322f},
            new float[2] {0.03120028f, 0.03593484f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['t'] = new HandsyDistances(
            't',
            new float[2] {0.04668656f, 0.05917827f}, 
            new float[2] {0.03579377f, 0.0659878f},
            new float[2] {0.03518923f, 0.04569025f},
            new float[2] {0.03287937f, 0.0423105f},
            new float[2] {0.03212645f, 0.03812776f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['u'] = new HandsyDistances(
            'u',
            new float[2] {0.05121143f, 0.05535289f}, 
            new float[2] {0.09221103f, 0.1044509f},
            new float[2] {0.0988422f, 0.1120573f},
            new float[2] {0.04194023f, 0.0519545f},
            new float[2] {0.03950682f, 0.04459706f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['v'] = new HandsyDistances(
            'v',
            new float[2] {0.05310545f, 0.06221106f}, 
            new float[2] {0.09715706f, 0.1083183f},
            new float[2] {0.0957875f, 0.11387f},
            new float[2] {0.04450767f, 0.05508086f},
            new float[2] {0.03885769f, 0.04767102f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['w'] = new HandsyDistances(
            'w',
            new float[2] {0.0491581f, 0.0617906f}, 
            new float[2] {0.09632107f, 0.1094918f},
            new float[2] {0.09953889f, 0.110219f},
            new float[2] {0.0920761f, 0.1007827f},
            new float[2] {0.04256057f, 0.06068439f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['x'] = new HandsyDistances(
            'x',
            new float[2] {0.05263569f, 0.06712335f}, 
            new float[2] {0.06247689f, 0.0915189f},
            new float[2] {0.04095905f, 0.04975506f},
            new float[2] {0.03923364f, 0.04336984f},
            new float[2] {0.03751209f, 0.04302613f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['y'] = new HandsyDistances(
            'y',
            new float[2] {0.07360166f, 0.08453292f}, 
            new float[2] {0.03763456f, 0.04436734f},
            new float[2] {0.03494919f, 0.04307127f},
            new float[2] {0.03552056f, 0.04039225f},
            new float[2] {0.08049787f, 0.09208344f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
        leftLetters['z'] = new HandsyDistances(
            'z',
            new float[2] {0.04784695f, 0.05894352f}, 
            new float[2] {0.08917838f, 0.101395f},
            new float[2] {0.03717768f, 0.04660995f},
            new float[2] {0.03401138f, 0.04203515f},
            new float[2] {0.03213888f, 0.03736935f},
            Leap.Unity.PointingType.RelativeToHorizon,
            targetTransform,
            Vector3.forward
        );
    } 
}

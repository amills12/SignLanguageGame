using UnityEngine;
using UnityEngine.UI;

public class reset : MonoBehaviour

{
    private string[] keys = new string[] {"a","b","c", "d", "e", "f", "g", "h", "i", "j", "k",
        "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", 
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", 
        "ProgressPercentAlpha","ProgressPercentNum", "PercentDisplayAlpha", "PercentDisplayNum"}; 
    public bool isReset = false;

    // Start is called before the first frame update
    public void resetProgress()
    {
        isReset = true;
        for(int i = 0; i < keys.Length; i++)
            PlayerPrefs.DeleteKey(keys[i]);
    }

}

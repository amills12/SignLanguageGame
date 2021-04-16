using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatorSphere : MonoBehaviour
{
    GameObject hands;

    Leap.Unity.HandsyDetector leftHand_nums, rightHand_nums;
    Handsy_Distance_Detector_Right rightHand;
    Handsy_Distance_Detector_Left leftHand;
    MeshRenderer meshRenderer;
    public string key;
    Letter letter;
    Color old;
    Transform currentLetter;
    GameObject closestLetter;

    public AudioSource audioSuccess;
    public AudioSource audioFail;

    int multiplier = 1, streak = 0;
    public int streakVal = 2;

    float standardRadius;

    // Timer Objects
    public Text timeCounter; //counter text display
    private bool timerRunning = false; //determines whether or not timer is running
    private float currentTime = 60f; //starts at 2 minutes

    //Menu Objects
    public GameObject endScreen, endScreenObject; //handles menu slides

    //hand objects
    public GameObject[] prefab;
    GameObject fadeGO;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        hands = GameObject.FindGameObjectWithTag("Player");

        // Capture RigidRoundHand_L and RigidRoundHand_R for alphabet
        leftHand = hands.transform.GetChild(2).GetComponent<Handsy_Distance_Detector_Left>();
        rightHand = hands.transform.GetChild(3).GetComponent<Handsy_Distance_Detector_Right>();

        // Capture RigidRoundHand_L and RigidRoundHand_R for numbers
        leftHand_nums = hands.transform.GetChild(2).GetComponent<Leap.Unity.HandsyDetector>();
        rightHand_nums = hands.transform.GetChild(3).GetComponent<Leap.Unity.HandsyDetector>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("SuccessExplosion");
        ParticleSystem exp = go.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = exp.shape;
        standardRadius = shapeModule.radius;

        //Set the scores integer value to 000 as default
        PlayerPrefs.SetInt("Score", 000);
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("Mult", multiplier);
        old = meshRenderer.material.color;
        
        //start timer at 3:00 to begin the countdown
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Capture the key of the next character
        DetermineKey();
        //Check if pressed
        if(leftHand.activated || rightHand.activated || leftHand_nums.activated || rightHand_nums.activated){
        //if(Input.GetKeyDown(key) || leftHand.activated || rightHand.activated || leftHand_nums.activated || rightHand_nums.activated){
            //If the letter was pressed within the sphere, success, destroy. increment score
            if(letter.active){
                letter.active = false;
                Destroy(letter.gameObject);
                AddStreak();
                ExplodeSuccess();
                IncScore();
                leftHand.activated = false; 
                rightHand.activated = false;
                leftHand_nums.activated = false;
                rightHand_nums.activated = false;
            }
        }
        
        //If letter is within the sphere and hits the center, destroy for a missed character
        if(letter.hit && letter.active){
            letter.active = false;
            Destroy(letter.gameObject);
            //findSpriteForMiss();
            ExplodeMissed();
            ResetStreak();
        }
    
        // display the countdown
        if (timerRunning)
        {
            if(currentTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
                DisplayTime(currentTime);
            }

            else //stop timer and end the game
            {
                currentTime = 0f;
                timeCounter.text = ("00:00");
                timerRunning = false;
                endScreen.GetComponent<PauseMenu>().EndGame();
                endScreenObject.SetActive(true);
            }
        }
    }

    /*  This function handles changing a boolean when entering the activators
        trigger (rigidbody), and as long as it is tagged with letter, we capture that
        object to be destroyed in Update() */
    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "Letter"){
            letter.GetComponent<Letter>().active = true;
        }
               
    }

    //  This function will increment the score through the games UI
    void IncScore(){
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + (100 * multiplier));
    }
    
    void ResetStreak(){
        streak = 0;
        multiplier = 1;
        UpdateGUI();
    }

    void AddStreak(){
        streak++;
        if(streak >= streakVal*4)
            multiplier = 4;
        else if(streak >= streakVal*3)
            multiplier = 3;
        else if(streak >= streakVal*2)
            multiplier = 2;
        else
            multiplier = 1;
        UpdateGUI();
    }

    void UpdateGUI(){
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("Mult", multiplier);
    }

    /*  This function will change the color of the activator if it is pressed
        without a character within its rigidbody */
    IEnumerator Missed(){
        meshRenderer.material.color = Color.black;
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material.color = old;
    }

    /*  ExplodeSuccess and ExplodeMissed will be called when appropriate to indicate a miss or 
        successful capture of the letter, two particle systems with different tags will be accessed
    */
    void ExplodeSuccess() {
        GameObject go = GameObject.FindGameObjectWithTag("SuccessExplosion");
        ParticleSystem exp = go.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = exp.shape;
        if(multiplier == 1)
            shapeModule.radius = standardRadius;
        else
            shapeModule.radius = standardRadius * multiplier;
        exp.Play();
        audioSuccess.Play();
    }
    void ExplodeMissed() {
        GameObject go = GameObject.FindGameObjectWithTag("MissedExplosion");
        ParticleSystem exp = go.GetComponent<ParticleSystem>();
        exp.Play();
        audioFail.Play();
    }

    /*  This function will compare all of the targets distances to know which
        is closest */
    Transform GetClosestLetter(){
        // Capture a list of all nearby letters
        List<Transform> characters = new List<Transform>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Letter")){
            characters.Add(go.GetComponent<Transform>());
        }

        Transform bestOption = null;
        Vector3 activatorPosition = transform.position;
        Vector3 targetPosition;
        float closest = Mathf.Infinity; //set as infinity to have a begining value
        float targetDistance;

        /*  iterate through all nearby transforms and compare distance to currently
            recorded distance */
        foreach(Transform target in characters){
            targetPosition = target.position - activatorPosition;   //get the different of the letter and activator
            targetDistance = targetPosition.sqrMagnitude;   //capture vector distance
            if(targetDistance < closest){   //check if its the closest
                closest = targetDistance;
                bestOption = target;
            }
        }

        return bestOption;
    }

    /*  DetermineKey will detect the closest character and assign the letter and the key that needs
        to be accessed for funcitonality in Update
    */
    void DetermineKey(){
        currentLetter = GetClosestLetter();
        letter = currentLetter.GetComponent<Letter>();
        key = currentLetter.name[7].ToString().ToLower();
    }

    void findSpriteForMiss(){
        //Find the sprite to display for a miss
        Debug.Log("Letter to find is: " + key);
        foreach(GameObject go in prefab){
            if(go.name.ToLower() == key){
                Debug.Log("Sprite name is: " + go.name.ToLower());
                Vector3 pos = this.transform.position;
                pos.y = pos.y + 2.5f;
                Instantiate(go, pos, Quaternion.Euler(0,0,0));
                StartCoroutine(fade(go, 1.4f));
            }
        }
    }

    IEnumerator fade(GameObject go, float duration)
    {
        float counter = 0;
        //Get current color
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>(); 
        Color spriteColor = sr.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

            //Wait for a frame
            yield return null;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeCounter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

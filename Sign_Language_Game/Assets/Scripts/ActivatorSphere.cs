using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorSphere : MonoBehaviour
{
    MeshRenderer meshRenderer;
    string key;
    Letter letter;
    Color old;
    Transform currentLetter;
    GameObject closestLetter;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 000);
        old = meshRenderer.material.color;   
    }

    // Update is called once per frame
    void Update()
    {
        DetermineKey();
        if(Input.GetKeyDown(key)){
            if(letter.active){
                letter.active = false;
                Destroy(letter.gameObject);
                ExplodeSuccess();
                IncScore();
            }
            else{
                StartCoroutine(Missed());
            }
        }
        
        if(letter.hit && letter.active){
            letter.active = false;
            Destroy(letter.gameObject);
            ExplodeMissed();
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

    // /*  This function handles changing a boolean when exiting the activators
    //     trigger (rigidbody) */
    // private void OnTriggerExit(Collider other) {
    //     if(!active){
    //         ExplodeMissed();
    //         active = false;
    //     }
    // }

    //  This function will increment the score through the games UI
    void IncScore(){
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);
    }

    /*  This function will change the color of the activator if it is pressed
        without a character within its rigidbody */
    IEnumerator Missed(){
        meshRenderer.material.color = Color.black;
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material.color = old;
    }
    /*  This function will change the color of the activator if it is pressed
        with a character within its rigidbody */
    // IEnumerator Success(){
    //     meshRenderer.material.color = Color.yellow;
    //     yield return new WaitForSeconds(0.05f);
    //     meshRenderer.material.color = old;
    // }

    void ExplodeSuccess() {
        GameObject go = GameObject.FindGameObjectWithTag("SuccessExplosion");
        ParticleSystem exp = go.GetComponent<ParticleSystem>();
        exp.Play();
    }
    void ExplodeMissed() {
        GameObject go = GameObject.FindGameObjectWithTag("MissedExplosion");
        ParticleSystem exp = go.GetComponent<ParticleSystem>();
        exp.Play();
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
        float closest = Mathf.Infinity;
        float targetDistance;

        /*  iterate through all nearby transforms and compare distance to currently
            recorded distance */
        foreach(Transform target in characters){
            targetPosition = target.position - activatorPosition;
            targetDistance = targetPosition.sqrMagnitude;
            if(targetDistance < closest){
                closest = targetDistance;
                bestOption = target;
            }
        }

        return bestOption;
    }

    void DetermineKey(){
        currentLetter = GetClosestLetter();
        letter = currentLetter.GetComponent<Letter>();
        key = currentLetter.name[7].ToString().ToLower();
    }
}

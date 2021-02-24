using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scolling_Background : MonoBehaviour
{
    MeshRenderer mr;
    Material mat;
    Vector2 offset;
    public float scrollTimeX, scrollTimeY;
    // Update is called once per frame
    void Update()
    {
        //Capture the MeshRenderers material element 0
        mr = GetComponent<MeshRenderer>();
        mat = mr.materials[0];
        //Get the textures offset values and change based on time and public variables
        offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime / scrollTimeX;
        offset.y += Time.deltaTime / scrollTimeY;
        //apply offset
        mat.mainTextureOffset = offset;
    }
}

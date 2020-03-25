using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHideObjs : MonoBehaviour
{
    Renderer HideObjs;
    float renderLevel = 0.0f;

    void Start ()
    {
        HideObjs = GetComponent<Renderer>();
        HideObjs.material.shader = Shader.Find("Toon/Lit Dissolve Appear");
    }
    

	void Update ()
    {
        //print("toten: " + totenRend.material.GetFloat("_Radius"));
	}

}

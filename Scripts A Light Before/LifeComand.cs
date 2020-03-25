using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComand : MonoBehaviour
{
    public float frequency = 1f;
    public float amplitude = 1f;
    public float glow = 0.0f;
    public Material material;
    public Color startEmissionColor = new Color(0.1F, 0.1F, 1.0F, 1F);
    public Color newEmissionColor;
    Renderer myRenderer;
 
    // Use this for initialization
    void Start()
    {
        glow = 1.0f;
        myRenderer = GetComponent<Renderer>();
        newEmissionColor = startEmissionColor;
    }
 
    // Update is called once per frame
    void Update()
    {
        //float glow = (2 + Mathf.Cos(Time.time * frequency)) * amplitude;
        //print("glow: " + glow);

        material.SetColor("_EmissionColor", newEmissionColor * glow);

        if (Input.GetKey(KeyCode.L))
        {
            glow += 0.1f;
        }
        else if (Input.GetKey(KeyCode.O))
        {
            glow -= 0.1f;
        }
    }
}

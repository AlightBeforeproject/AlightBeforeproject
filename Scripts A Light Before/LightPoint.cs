using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPoint : MonoBehaviour
{

    GameObject player;
    public GameObject selfObj;
    private Transform lightPos;
    private Transform playerPos;
    public static LightPoint instance;
    ShaderController shaderController;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        shaderController = player.GetComponent<ShaderController>();
        lightPos = this.transform;        
    }
	
	void Update ()
    {
        //print("dist: "+distLightPlayer());        

        if (distLightPlayer() <= 12.0f && shaderController.cRadius >= 2.0f)
        {
            //rend = this.gameObject.GetComponent<Renderer>();
            
            selfObj.gameObject.SetActive(true);
            

            //this.gameObject.SetActive(true);
        }
	}

    public float distLightPlayer()
    {
        return Vector3.Distance(playerPos.position, lightPos.position);
    }

}

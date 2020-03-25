using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShaderPosition: MonoBehaviour
{

    MeshRenderer meshRenderer;
    ShaderController shaderController;
    GameObject objects;
    GameObject player;
    List<GameObject> totenList;
    //List<GameObject> totenList;
    List<Renderer> rend;
    //public Material[] material;
    ControllMaterial controllMat;

    // Use this for initialization
    void Start ()
    {
        totenList = new List<GameObject>();
        //totenList = new List<GameObject>();
        rend = new List<Renderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        objects = GameObject.FindGameObjectWithTag("Toten");

        controllMat = objects.GetComponent<ControllMaterial>();

        totenList = GameObject.FindGameObjectsWithTag("Toten").ToList();
        //totenList = GameObject.FindGameObjectsWithTag("Toten").ToList();
        //rend = obsHideList.GetComponent<Renderer>();
        shaderController = player.GetComponent<ShaderController>();

        //rend. sharedMaterial = material[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        Shader.SetGlobalVector("_Position", transform.position);

        //if (controllMat.enterTrigger) // TA AQUI O PROBLEMA, COMO EU FAÇO PRA INTERAGIR COM OBSTACULOS INVISIVEIS
        //{
            for (int i = 0; i < totenList.Count; i++)
            {
                totenList[i].GetComponent<Renderer>().material.SetFloat("_dRadius", shaderController.cRadius);               
            }
        //}

        //for (int i = 0; i < totenList.Count; i++)
        //{
        //    totenList[i].GetComponent<Renderer>().material.SetFloat("_Radius", shaderController.cRadius); ;
        //}
        // rend.material.SetFloat("_Radius", shaderController.cRadius);        
    }    
}

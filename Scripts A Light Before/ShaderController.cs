using ProBuilder2.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    MeshRenderer meshRenderer;
    //ControllMaterial ctrlMaterial;
    //public Shader shader1;
    GameObject ground;
    List<GameObject> groundList;
    List<GameObject> hideElementsList;
    List<GameObject> enemyList;
    List<GameObject> totenList;
    //List<GameObject> obsList;
    Renderer rend;
    List<Renderer> rendList;
    public float cont = 0;
    public int cont2 = 0;
    public bool adiciona = false;
    public bool countdown = false;
    public bool resetCount = false;
    private float valRedux = 0.04f;
    public static ShaderController instance;
    public bool charLight = false;

    public float cRadius = 0.0f;

    // Use this for initialization
    void Start()
    {
        groundList = new List<GameObject>();
        enemyList = new List<GameObject>();
        totenList = new List<GameObject>();
        hideElementsList = new List<GameObject>();

        rendList = new List<Renderer>();

        groundList = GameObject.FindGameObjectsWithTag("Ground").ToList();
        enemyList = GameObject.FindGameObjectsWithTag("Inimigo").ToList();
        totenList = GameObject.FindGameObjectsWithTag("Toten").ToList();
        hideElementsList = GameObject.FindGameObjectsWithTag("HideObj").ToList();

        rendList = groundList.GetComponents<Renderer>().ToList();

        //for (int j = 0; j < totenList.Count; j++)
        //{
        //    ctrlMaterial = totenList[j].GetComponent<ControllMaterial>();
        //}

        //rend = GetComponent<Renderer>();

        //shader1 = Shader.Find("Toon/Lit Dissolve DoubleTex");
    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.SetFloat("_Radius", cRadius);
        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].GetComponent<Renderer>().material.SetFloat("_Radius", cRadius);
            groundList[i].GetComponent<Renderer>().material.SetFloat("_dRadius", cRadius);

        }
        for (int f = 0; f < totenList.Count; f++)
        {
            totenList[f].GetComponent<Renderer>().material.SetFloat("_Radius", cRadius);
        }
        for (int k = 0; k < hideElementsList.Count; k++)
        {
            hideElementsList[k].GetComponent<Renderer>().material.SetFloat("_dRadius", cRadius);
        }

        //print("cRadius: " + cRadius);
        //print("count: " + cont);
        //print("count2: " + cont2);

        //for (int f = 0; f < enemyList.Count; f++)
        //{
        //    enemyList[f].GetComponent<Renderer>().material.SetFloat("",)
        //}

        ShaderInput();

        ////Shader.SetGlobalFloat("_Radius", cRadius);
    }

    void ShaderInput()
    {
        if (PauseMenu.gameIsPaused == false)
        {
            if (Input.GetMouseButton(0) && cRadius <= 10.0f && adiciona == false
                || Input.GetButton("RT_Button") && cRadius <= 10.0f && adiciona == false)
            {
                cRadius += 0.07f;
                charLight = true;
                //changeSomething
            }            
            else if (cRadius > 0)
            {
                cRadius -= valRedux;
                cont -= 0.5f;                
                if (cRadius < 0)
                {
                    cRadius = 0;
                    cont2 = 0;
                    if (adiciona == true)
                    {
                        countdown = true;
                    }
                }
                if (cont < 0)
                {
                    cont = 0;
                }
            }
            if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("RT_Button"))
            {
                charLight = false;
            }

            if (cRadius >= 9.0f && adiciona == false)
            {
                cont += 1.0f;
            }
            if (cont >= 150)
            {
                adiciona = true;
                charLight = false;
                //countdown = true;

                cont = 0;
            }

            if (adiciona)
            {
                valRedux = 0.09f;
            }
            else
            {
                valRedux = 0.04f;
            }

            if (countdown && cont2 <= 200)
            {
                cont2 += 1;
            }
            else if (countdown && cont2 >= 200)
            {
                countdown = false;
                adiciona = false;
                cont2 = 0;
            }

            //if (countdown && cont2 <= 300)
            //{
            //    cont2 += 1;
            //}            

            //    //if (cRadius >= 0f)
            //    //{

            //    //}
            //}
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ControllMaterial : MonoBehaviour
{
    ShaderController shaderController;
    HandleTextFile handText;

    GameObject player;
    List<GameObject> spawns = new List<GameObject>();
    //GameObject enemyRender;    
    Renderer totenRend;

    float renderLevel = 0.0f;
    //public float amount = 0.0f;
    //SaveLoadGame saveLoad;

    MeshRenderer meshRenderer;
    //public Shader shader1;
    public GameObject objMesh;
    //public List<GameObject> obsList = new List<GameObject>();

    public GameObject[] obsList;
    
    List<GameObject> enemyList;
    List<Renderer> objRend;

    [SerializeField]
    public int tottenAtual = 0;

    Shader shader1;
    Shader shaderEnemy;

    Renderer rend;
    //Renderer rendEnemy;    

    Inimigo inimigo;

    public float entireRadius = 0.0f;
    public float dRadius = 0.0f;
    public float dist = 0.0f;

    public static int totenNum = 0;

    public Material[] material;

    public bool enterTrigger = false;
    public bool podeMorrer = false;
    public bool podeDesaparecer = false;
    public bool changeMat = false;
    public SphereCast sphereCast;
    float lerp = 0.0f;


    private void Awake()
    {

    }

    private void OnEnable()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    void Start()
    {
        objRend = new List<Renderer>();
        enemyList = new List<GameObject>();

        sphereCast = new SphereCast();

        
        
        player = GameObject.FindGameObjectWithTag("Player");
        spawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        sphereCast = player.GetComponent<SphereCast>();
        //enemyRender = GameObject.FindGameObjectWithTag("Inimigo");


        //objMesh = GameObject.FindGameObjectWithTag("HideObj");
        //objRend = objMesh.GetComponent<Renderer>();

        //obsList = GameObject.FindGameObjectsWithTag("Toten").ToList();
        enemyList = GameObject.FindGameObjectsWithTag("Inimigo").ToList();

        totenRend = GetComponent<Renderer>();
        totenRend.material.shader = Shader.Find("Toon/Lit Dissolve DoubleTex");


        shaderController = player.GetComponent<ShaderController>();

        for (int i = 0; i < spawns.Count; i++)
        {
            handText = spawns[i].GetComponent<HandleTextFile>();
        }


        //if (SaveLoadGame.instance.deuLoad == true)
        //{
        //    obsList[tottenAtual].GetComponent<Renderer>().material = material[1];
        //}

        //for (int j = 0; j < enemyList.Count; j++)
        //{
        //    matEnemy = enemyList[j].GetComponent<EnemyMatCtrl>();
        //}

        //for (int j = 0; j < obsList.Count; j++)
        //{            
        //    obsList[j].GetComponent<Renderer>().sharedMaterial = material[0];
        //}

        //for (int j = 0; j < enemyList.Count; i++)
        //{
        //    enemyRender = enemyList[j]
        //}
        //rendEnemy = GetComponent<Renderer>();
        //rendEnemy.material.shader = Shader.Find("Toon/Lit Dissolve");

        //rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Toon/Lit Dissolve Appear");

    }

    // Update is called once per frame
    void Update()
    {
        if (SaveLoadGame.instance.deuLoad == true)
        {
            if (SaveLoadGame.instance.spawnInt[tottenAtual] == 1)
            {
                obsList[tottenAtual].GetComponent<Renderer>().material = material[2];
            }
            else
            {
                obsList[tottenAtual].GetComponent<Renderer>().material = material[0];
            }
            //print(SaveLoadGame.instance.spawnInt[tottenAtual]);
        }

        
        dist = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        //print("Dist: " + dist);

        if (enterTrigger == true)
        {
            
            if (totenRend.material.GetFloat("_Radius") >= 6.0f )
            {

                //print("FOI");
                podeDesaparecer = true;
                
                obsList[tottenAtual].GetComponent<Renderer>().material = material[1];
                if (lerp <= 0.863)
                {
                    lerp += 0.1f;
                    material[1].SetFloat("_LerpValue", lerp);
                }
                if (lerp >= 0.863)
                {
                    obsList[tottenAtual].GetComponent<Renderer>().material = material[2];
                }
                //changeMat = true;

                //
                //if (enterTrigger == true)
                //{
                //    podeDesaparecer = true;
                //}  

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enterTrigger = true;
            //Debug.Log("entered in totten trigger");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enterTrigger = false;
        //if (enterTrigger == false)
        //{
        //    Debug.Log("outed  totten trigge");
        //    for (int j = 0; j < obsList.Count; j++)
        //    {
        //        obsList[j].GetComponent<Renderer>().material.SetFloat("_dRadius", dRadius);
        //    }
        //    if (dRadius > 0)
        //    {
        //        dRadius -= 0.02f;
        //        if (dRadius < 0)
        //        {
        //            dRadius = 0;
        //        }
        //    }
        //}
    }
}
using ProBuilder2.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Renderer render;    
    public Collider[] coliders;
    public float health = 150f;
    public GameObject axe;
    public GameObject axePivot0;
    public GameObject axePivot1;
    List<GameObject> enemy;

    public GameObject player;
    
    //public GameObject objects;
    public GameObject spawm;

    public SphereCast sphereCast;

    public HandleTextFile handleTextFile;

    public bool takingDamage = false;
    
    Animator animator;    

    public Material[] material;

   public static Inimigo instance;

    //EnemyStates
    public bool run = false;
    public bool idle = false;
    public bool death = false;
    public bool atack = false;
    float distAmount = 0.0f;

    void start()
    {        
        sphereCast = new SphereCast();
        handleTextFile = new HandleTextFile();
        enemy = new List<GameObject>();
        

        //objects = GameObject.FindGameObjectWithTag("HideObj");        
       
        enemy = GameObject.FindGameObjectsWithTag("Inimigo").ToList();

        render = GetComponent<Renderer>();
        //rend.enabled = true;
        //render.sharedMaterial = material[0];

        

    }

    public void init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawm = GameObject.FindGameObjectWithTag("Spawner");
        sphereCast = player.GetComponent<SphereCast>();
        handleTextFile = spawm.GetComponent<HandleTextFile>();
    }

    void Update()
    {
        changeMaterials();

        /*
         GetComponent<Renderer>().material.SetFloat("_dRadius", shaderController.cRadius); 
        */

        //takingDamage = false;
        

        if (sphereCast && sphereCast.colidiu == true)
        {
            //print("entrou aqui");
            
            render.enabled = true;
        }
        animator = GetComponent<Animator>();
        setAnimations();
        
    }

    public void TakeDamage(float amount)
    {
        //takingDamage = true;
    //    health -= amount;
        
    //    //if (health <= 0f)
    //    //{
    //    //    Die();
    //    //    handleTextFile.numInimigos -= 1;
    //    //}

    }

    

    public void setAnimations()
    {
        if (run == true)
        {
            animator.SetBool("Run", true);            
            axe.gameObject.GetComponent<Animator>().SetBool("AxeRun", true);
            axe.gameObject.transform.position = axePivot0.gameObject.transform.position;
        }
        else
        {
            animator.SetBool("Run", false);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeRun", false);
            axe.gameObject.transform.position = axePivot1.gameObject.transform.position;
        }
        if (idle == true)
        {
            animator.SetBool("Idle", true);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeIdle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeIdle", false);
        }
        if (death == true)
        {
            animator.SetBool("Death", true);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeDeath", true);
        }
        else
        {
            animator.SetBool("Death", false);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeDeath", false);
        }
        if (atack == true)
        {
            animator.SetBool("Atack", true);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeAtack", true);
        }
        else
        {
            animator.SetBool("Atack", false);
            axe.gameObject.GetComponent<Animator>().SetBool("AxeAtack", false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {

    //        
    //    }
    //    else
    //    {
    //        render.sharedMaterial = material[0];
    //    }
    //}

    void changeMaterials()
    {
        if (takingDamage)
        {
            render.sharedMaterial = material[1];
        }
        else
        {
            render.sharedMaterial = material[0];
        }
    }
}
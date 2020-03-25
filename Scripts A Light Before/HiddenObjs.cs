using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HiddenObjs : MonoBehaviour
{
    GameObject Player;
    HealthBarPlayer healthBarPlayer;
    public GameObject[] hideObsList;

    //public BoxCollider firstCollider;
    //public BoxCollider secondCollider;

    public bool enterObjTrigger = false;
    public bool ganhouVida = false;
    float lifeHealVal = 35.0f;
    float dif = 0.0f;
    float lerp = 0.0f;
    public Material[] material;
    public int objAtual = 0;
    bool podeAdicionar = false;

    PlayerComand playerComand;
    Renderer objRend;

    public static SaveLoadGame instance;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerComand = Player.gameObject.GetComponent<PlayerComand>();
        healthBarPlayer = Player.gameObject.GetComponent<HealthBarPlayer>();
        objRend = GetComponent<Renderer>();
        objRend.material.shader = Shader.Find("Toon/Lit Dissolve Appear");

    }

    void Update()
    {  
        if (podeAdicionar)
        {
            if (healthBarPlayer.hitpoint + lifeHealVal <= 150f)
            {
                ganhouVida = true;
                playerComand.playerLife += lifeHealVal;
                healthBarPlayer.HealDamage(lifeHealVal);
                podeAdicionar = false;
            }
            //if (healthBarPlayer.hitpoint + lifeHealVal >= 150f)
            //{
            //    dif = (healthBarPlayer.hitpoint + lifeHealVal) % 150f;
            //    healthBarPlayer.HealDamage(dif);
            //    podeAdicionar = false;
            //}
        }


        if (enterObjTrigger && objRend.material.GetFloat("_dRadius") > 10.0f
                && ganhouVida == false)
        {
            //Debug.Log("Entered Obj trigger");
            /*firstCollider.enabled = true*/
            ;

            hideObsList[objAtual].GetComponent<Renderer>().material = material[1];
            if (lerp <= 0.863)
            {
                lerp += 0.2f;
                material[1].SetFloat("_LerpValue", lerp);
            }
            if (lerp >= 0.863)
            {
                podeAdicionar = true;
                hideObsList[objAtual].GetComponent<Renderer>().material = material[2];
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        enterObjTrigger = true;
        //GetComponent<Renderer>().material = material[1];


    }

    private void OnTriggerExit(Collider other)
    {
        enterObjTrigger = false;
        //Debug.Log("Exited Obj trigger");
    }
}

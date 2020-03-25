using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBridges : MonoBehaviour
{
    GameObject Player;
    HealthBarPlayer healthBarPlayer;
    public GameObject[] hideObsList;

    GameObject saveLoad;

    public BoxCollider firstCollider;
    public BoxCollider secondCollider;

    public bool enterObjTrigger = false;

    float lerp = 0.0f;
    public Material[] material;
    public int objAtual = 0;
    bool podePassar = false;

    PlayerComand playerComand;
    Renderer objRend;

    public static SaveLoadGame instance;

    private void Awake()
    {
        //saveLoad = GameObject.FindGameObjectWithTag("Game");

        //if (!saveLoad.GetComponent<SaveLoadGame>().deuLoad )
        //{
        //    hideObsList[objAtual].GetComponent<Renderer>().material = material[0];
        //    PlayerPrefs.SetInt("bridgeActive", 0);
        //}
        //else
        //{
        //    if (PlayerPrefs.GetInt("bridgeActive") == 1)
        //    {
        //    hideObsList[objAtual].GetComponent<Renderer>().material = material[1];
        //    firstCollider.enabled = true;
        //    lerp = 1;
        //    }
        //}

        //if (PlayerPrefs.GetInt("bridgeActive") == 1 && SaveLoadGame.instance.deuLoad)
        //{
        //    hideObsList[objAtual].GetComponent<Renderer>().material = material[1];
        //    firstCollider.enabled = true;
        //}
        //else
        //{
        //    hideObsList[objAtual].GetComponent<Renderer>().material = material[0];
        //    PlayerPrefs.SetInt("bridgeActive", 0);
        //}
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerComand = Player.gameObject.GetComponent<PlayerComand>();
        healthBarPlayer = Player.gameObject.GetComponent<HealthBarPlayer>();
        objRend = GetComponent<Renderer>();
        objRend.material.shader = Shader.Find("Toon/Lit Dissolve Appear");

        saveLoad = GameObject.FindGameObjectWithTag("Game");

        if (!saveLoad.GetComponent<SaveLoadGame>().deuLoad)
        {
            hideObsList[objAtual].GetComponent<Renderer>().material = material[0];
            firstCollider.enabled = false;
            //PlayerPrefs.SetInt("bridgeActive", 0);
        }
        else
        {
            //if (PlayerPrefs.GetInt("bridge_0_Active") == 1)
            //{
                hideObsList[objAtual].GetComponent<Renderer>().material = material[1];
                firstCollider.enabled = true;
                lerp = 1;
            //}
        }
    }

    void Update()
    {
        //if (podeAdicionar /*&& playerComand.playerLife <= 150*/)
        //{
        //    ganhouVida = true;
        //    playerComand.playerLife += lifeHealVal;
        //    healthBarPlayer.HealDamage(lifeHealVal);
        //    podeAdicionar = false;
        //}
        print("Prefs" + PlayerPrefs.GetInt("bridgeActive"));

        if (enterObjTrigger && objRend.material.GetFloat("_dRadius") > 10.0f /*&& PlayerPrefs.GetInt("bridgeActive") != 1*/)
        {
            //Debug.Log("Entered Obj trigger");
            firstCollider.enabled = true;

            hideObsList[objAtual].GetComponent<Renderer>().material = material[1];

            if (lerp <= 0.863)
            {
                lerp += 0.1f;
                material[1].SetFloat("_LerpValue", lerp);
            }
            if (lerp >= 0.863)
            {
                podePassar = true;
                //PlayerPrefs.SetInt("bridgeActive", 1);
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

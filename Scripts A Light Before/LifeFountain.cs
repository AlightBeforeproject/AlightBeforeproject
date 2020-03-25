using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LifeFountain : MonoBehaviour
{
    GameObject Player;
    List<GameObject> spawmList;
    public List<GameObject> lightQuadrant;
    PlayerComand playerComand;
    HealthBarPlayer healthBarPlayer;
    HandleTextFile handleText;
    SaveLoadGame saveLoad;
    GameObject game;

    public GameObject[] chieldObjs;

    float lifeHealVal = 0.1f;
    public int numFase = 0;
    public int numQuadrant = 0;


    public bool savedGame = false;
    public bool savedTrigger = false;

    private void Awake()
    {
       
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    void Start()
    {

        //chieldObjs = gameObject.GetComponentsInChildren<GameObject>();

        game = GameObject.FindGameObjectWithTag("Game");

        Player = GameObject.FindGameObjectWithTag("Player");
        playerComand = Player.gameObject.GetComponent<PlayerComand>();
        healthBarPlayer = Player.gameObject.GetComponent<HealthBarPlayer>();
        saveLoad = game.GetComponent<SaveLoadGame>();
        spawmList = new List<GameObject>();
        spawmList = GameObject.FindGameObjectsWithTag("Spawner").ToList();

        for (int i = 0; i < spawmList.Count; i++)
        {
            spawmList[i].GetComponent<HandleTextFile>().morte();
            //handleText = spawmList[i].GetComponent<HandleTextFile>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numQuadrant <= saveLoad.recebeQuadrant)
        {
            savedTrigger = true;
        }
        if (saveLoad.deuLoad)
        {
            for (int i = 0; i < saveLoad.recebeQuadrant; i++)
            {
                for (int j = 0; j <
                    lightQuadrant[i].gameObject.transform.childCount; j++)
                {
                    //lightQuadrant[i].gameObject.transform.GetChild(j).gameObject.
                    //    GetComponent<Renderer>().enabled = true;
                    lightQuadrant[i].gameObject.transform.GetChild(j).gameObject.SetActive(true);
                    //SetActive(true);
                }
                //lightQuadrant[i].gameObject.
                //lightQuadrant[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                //GetComponentInChildren<GameObject>().SetActive(true);
                //lightQuadrant[i].GetComponent<Renderer>().enabled = false;
            }
            
        }
    }

    //void OnTriggerStay(Collider collEnter)
    //{
    //    if (collEnter.gameObject.tag == "Player")
    //    {

    //        //playerComand.checkpoint = transform.parent;


    //        if (playerComand.playerLife <= healthBarPlayer.getMaxPoints())
    //        {
    //            //print("GANHANDO VIDA");

    //            //playerComand.playerLife += lifeHealVal;
    //            //healthBarPlayer.HealDamage(lifeHealVal);
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider collEnter2)
    {
        if (collEnter2.gameObject.tag == "Player")
        {
            savedGame = true;


            //print("nFase:" + numFase);

            playerComand.x = transform.position.x;
            playerComand.y = transform.position.y;
            playerComand.z = transform.position.z;

            saveLoad.loadPos_x = transform.position.x;
            saveLoad.loadPos_y = transform.position.y;
            saveLoad.loadPos_z = transform.position.z;
            saveLoad.WriteText("SaveGame");

            // se o valor do save for maior ou igual ao valor do savepoint, ele fica true

            if (savedTrigger == false && savedTrigger == false)
            {
                if (saveLoad.saveLightQuadrant <= numQuadrant)
                {
                    saveLoad.saveLightQuadrant = numQuadrant;
                    //}
                    for (int i = 0; i < numQuadrant; i++)
                    {
                        for (int j = 0; j <
                            lightQuadrant[i].gameObject.transform.childCount; j++)
                        {
                            //lightQuadrant[i].gameObject.transform.GetChild(j).gameObject.
                            //GetComponent<Renderer>().enabled = true;
                            lightQuadrant[i].gameObject.transform.GetChild(j).gameObject.SetActive(true);
                        }
                        //lightQuadrant[i].gameObject.
                        //lightQuadrant[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                        //GetComponentInChildren<GameObject>().SetActive(true);
                        //lightQuadrant[i].GetComponent<Renderer>().enabled = false;
                    }
                }
            }

            savedTrigger = true;
        }
    }

    void OnTriggerExit(Collider collExit)
    {
        if (collExit.gameObject.tag == "Player")
        {
            savedGame = false;
        }
    }
}
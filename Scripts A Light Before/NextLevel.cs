using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextStage = 0;
    public static int receiveStage;
    public GameObject endGameUI;
    SaveLoadGame saveLoad;
    GameObject game;
    [SerializeField]
    public static bool passouFase = false;
    public float timer = 0.0f;
    public int seconds = 0;

    // Use this for initialization
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game");
        saveLoad = game.GetComponent<SaveLoadGame>();
    }

    // Update is called once per frame
    void Update()
    {
        receiveStage = nextStage;
    }

    void OnTriggerEnter(Collider collEnter2)
    {
        
        //print(seconds);

        //if (seconds != 5)
        //{
        //    timer += Time.deltaTime;
        //    seconds = (int)(timer % 60);            

            
        //}
        //else
        //{
        //    seconds = 0;
        //    timer = 0.0f;
        //    BlockWall.spawnsLimit = 0;
        //    SceneManager.LoadScene(0);
        //    endGameUI.SetActive(false);
        //}

        //SceneManager.LoadScene(nextStage);
        saveLoad.resetSpawns();
        passouFase = true;
    }

    //private void OnTriggerExit(Collider other2)
    //{
    //    passouFase = false;
    //}
}

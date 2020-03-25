using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool podeVoltar = false;

    public GameObject pauseMenuUI;
    public GameObject loadUI;
    public GameObject nextLevelObj;
    //public GameObject endGameUI;
    GameObject playerPrefab;
    public EventSystem eventSystem;
    public GameObject selectedObject;
    public GameObject player;
    PlayerComand playerCmd;
    public float timer = 0.0f;
    public float timer2 = 0.0f;
    public int seconds = 0;
    public int seconds2 = 0;

    public int menuId = 0;

    public Button button;

    void Awake()
    {

        
    }

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        seconds = 0;
        seconds2 = 0;
        timer2 = 0.0f;
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerCmd = playerPrefab.GetComponent<PlayerComand>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("Clicou: " + LoadCanvas.clicou);

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start_Button"))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }

        //print(seconds2);
        if (LoadCanvas.clicou && seconds != 5 || playerCmd.morreuLoad && seconds != 5)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            loadUI.SetActive(true);
            //playerCmd.inimigoReset = false;
        }
        else
        {
            seconds = 0;
            timer = 0.0f;
            playerCmd.morreuLoad = false;
            LoadCanvas.clicou = false;
            loadUI.SetActive(false);
        }
        if (NextLevel.passouFase && seconds2 != 5)
        {
            timer2 += Time.deltaTime;
            seconds2 = (int)(timer2 % 60);
            nextLevelObj.GetComponent<NextLevel>().endGameUI.SetActive(true);
            //endGameUI.SetActive(true);
            podeVoltar = true;
        }
        else if(podeVoltar && seconds2 == 5)
        {
            seconds2 = 0;
            timer2 = 0.0f;
            
            NextLevel.passouFase = false;            
            BlockWall.spawnsLimit = 0;
            SceneManager.LoadScene(NextLevel.receiveStage);
            //SceneManager.LoadScene(0); aqui eu pulo de fase
            podeVoltar = false;
        }
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        //playerCmd.GetComponent<PlayerComand>().enabled = true;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;        
        //playerCmd.GetComponent<PlayerComand>().enabled = false;
    }

    public void Menu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);        
    }

    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SaveLoadGame : MonoBehaviour
{
    //LifeFountain lifeFont;
    AsyncOperation asyncLoadLevel;
    LifeFountain lifeFountain;
    SceneLoader sceneLoader;
    NextLevel nextLevel;
    ControllMaterial ctrlMaterial;
    HealthBarPlayer healthBar;    

    public PauseMenu pauseMenu;
    public GameObject game;
    public GameObject nextLevelObj;

    public Transform playerLoadPos;
    public float loadPos_x = 0.0f;
    public float loadPos_y = 0.0f;
    public float loadPos_z = 0.0f;

    PlayerComand playerCmd;
    public GameObject player;

    HandleTextFile handleText;

    public GameObject playerObj;
    StreamReader reader = null;
    public bool spawnLoad = false;
    public bool deuLoad = false;

    Transform positions;

    public List<GameObject> listFonts = new List<GameObject>();
    public List<GameObject> listSpawns = new List<GameObject>();
    public List<GameObject> listTotens = new List<GameObject>();

    // assigned to allow first line to be read below
    public int spawn_0 = 0;
    public int spawn_1 = 0;
    public int spawn_2 = 0;
    int n_num_00 = 0;
    int numActulSpawn = 0;
    public int recebeQuadrant = 0;
    public int recebeVida = 0;

    public int saveLightQuadrant = 0;

    public float f_num_1 = 0f;
    public float f_num_2 = 0f;
    public float f_num_3 = 0f;

    char[] delimiter = { '_' };

    int numDefeatedSpawn = 0;

    string[] fields2 = null;

    //maneira de enviar para unity
    string filePath = "Assets\\StreamingAssets\\";

    //maneira de enviar para build
    //string filePath = "A Light Before - Prototype_Data\\StreamingAssets\\";

    string recebefase;
    string filename;
    string filename2;
    string txtContents;
    string text;
    string text2;
    string text3;
    string text4;
    string text5;
    string text6;
    string resetVar = "0";
    public string[] recebeSpawn = null;    

    [SerializeField]
    public List<string> spawnFiles = new List<string>();
    [SerializeField]
    public List<int> spawnInt = new List<int>();

    public static SaveLoadGame instance;

    void Awake()
    {
        //Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        listSpawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        if (SceneManager.GetActiveScene().name == "0_Menu")
        {
            GetComponent<SaveLoadGame>().enabled = false;
        }
        else
        {
            GetComponent<SaveLoadGame>().enabled = true;
        }

        if (spawnInt.Count <= 7)
        {
            for (int i = 0; i < 5; i++)
            {
                spawnInt.Add(0);
                spawnFiles.Add(resetVar);
            }
        }
        if (NextLevel.passouFase)
        {
            spawnInt.Clear();
            spawnFiles.Clear();
            listSpawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
            for (int j = 0; j < listSpawns.Count; j++)
            {
                spawnInt.Add(0);
                spawnFiles.Add(resetVar);
            }
        }
        
        listSpawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        listFonts = GameObject.FindGameObjectsWithTag("LifePoint").ToList();
        listTotens = GameObject.FindGameObjectsWithTag("Toten").ToList();
        
    }

    void Start()
    {

        listFonts = GameObject.FindGameObjectsWithTag("LifePoint").ToList();
        listSpawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        player = GameObject.FindGameObjectWithTag("Player");
        game = GameObject.FindGameObjectWithTag("Game");
        nextLevelObj = GameObject.FindGameObjectWithTag("NextLevel");
        healthBar = player.GetComponent<HealthBarPlayer>();

        sceneLoader = game.GetComponent<SceneLoader>();
        nextLevel = nextLevelObj.GetComponent<NextLevel>();

        playerCmd = player.GetComponent<PlayerComand>();

        for (int i = 0; i < listFonts.Count; i++)
        {
            lifeFountain = listFonts[i].GetComponent<LifeFountain>();
        }
        for (int j = 0; j < listSpawns.Count; j++)
        {
            handleText = listSpawns[j].GetComponent<HandleTextFile>();
            spawnInt.Add(0);
        }
        for (int k = 0; k < listTotens.Count; k++)
        {
            ctrlMaterial = listTotens[k].GetComponent<ControllMaterial>();
        }

    }

    private void Update()
    {       
        if (listFonts[0] == null)
        {
            listFonts = GameObject.FindGameObjectsWithTag("LifePoint").ToList();
        }
        if (listSpawns[0] == null)
        {
            listSpawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        }
        if (listTotens[0] == null)
        {
            listTotens = GameObject.FindGameObjectsWithTag("Toten").ToList();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (pauseMenu == null)
        {
            pauseMenu = Object.FindObjectOfType<PauseMenu>();
        }
        if (healthBar == null)
        {
            healthBar = player.GetComponent<HealthBarPlayer>();
        }
    }

    public void WriteText(string writeTxt)
    {
        filename = filePath + writeTxt + ".txt";
        StreamWriter myStrWriter = new StreamWriter(filename);

        for (int i = 0; i < listFonts.Count; i++)
        {
            lifeFountain.numFase = listFonts[i].GetComponent<LifeFountain>().numFase;
        }

        myStrWriter.WriteLine(lifeFountain.numFase);
        myStrWriter.WriteLine(loadPos_x);
        myStrWriter.WriteLine(loadPos_y);
        myStrWriter.WriteLine(loadPos_z);
        for (int j = 0; j < listSpawns.Count; j++)
        {
            //listSpawns[j].GetComponent<HandleTextFile>().spawnID
            //+"_" +
            myStrWriter.WriteLine(listSpawns[j].GetComponent<HandleTextFile>().GetMorreu());

        }
        myStrWriter.WriteLine(healthBar.GetHitpoint());
        myStrWriter.WriteLine(saveLightQuadrant);

        myStrWriter.Flush();
        myStrWriter.Close();
    }

    public void LoadText(string loadTxt)
    {
        numDefeatedSpawn = 0;
        player.GetComponent<SphereCast>().colidiu = false;
        HandleTextFile.blockBattle = false;
        deuLoad = true;
        NextLevel.passouFase = false;

        filename = filePath + loadTxt + ".txt";
        StreamReader myStrReader = new StreamReader(filename);

        recebefase = myStrReader.ReadLine();
        text = myStrReader.ReadLine();
        text2 = myStrReader.ReadLine();
        text3 = myStrReader.ReadLine();

        if (recebefase == "1")
            numActulSpawn = 3;
        else if (recebefase == "2")
            numActulSpawn = 4;
        else if (recebefase == "3")
            numActulSpawn = 5;
        else if (recebefase == "4")
            numActulSpawn = 0;


        spawnFiles.Clear();

        for (int i = 0; i < numActulSpawn; i++)
        {
            text4 = myStrReader.ReadLine();
            
            //recebeSpawn = text4;

            spawnFiles.Add(text4);
            spawnInt[i] = int.Parse(spawnFiles[i]);

            if (spawnFiles[i] == "1")
            {
                numDefeatedSpawn++;
            }

            //spawnFiles.Add(recebeSpawn);
            //print("spwnFiles: " + recebeSpawn[0]);
            //print("spwnFiles: " + recebeSpawn[1]);
        }

        //HandleTextFile.wallHint = numDefeatedSpawn;
        BlockWall.spawnsLimit = numDefeatedSpawn;

        text5 = myStrReader.ReadLine();
        text6 = myStrReader.ReadLine();

        int n_num_00 = int.Parse(recebefase);
        f_num_1 = float.Parse(text);
        f_num_2 = float.Parse(text2);
        f_num_3 = float.Parse(text3);

        recebeVida = int.Parse(text5); // erro aqui


        loadPos_x = f_num_1;
        loadPos_y = f_num_2;
        loadPos_z = f_num_3;
        //ctrlMaterial.setMaterials(2);

        recebeQuadrant = int.Parse(text6);

        myStrReader.Read();

        //player = Instantiate(playerObj, new Vector3(f_num_1, f_num_2, f_num_3), Quaternion.identity) as GameObject;
        SceneManager.LoadScene(n_num_00);
        //playerCmd.playerLife = recebeVida;
        //player.transform.position = new Vector3(f_num_1, f_num_2, f_num_3);

        pauseMenu.ResumeGame();


        myStrReader.Close();
    }

    public void resetSpawns()
    {
        for (int i = 0; i < listSpawns.Count; i++)
        {
            listSpawns[i].GetComponent<HandleTextFile>().resetMorte();
            spawnInt[i] = 0;
        }
    }

    public void resetList()
    {
        spawnInt.Clear();
        listSpawns = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        for (int j = 0; j < listSpawns.Count; j++)
        {
            spawnInt.Add(0);
        }
    }

    void OnDisable()
    {
        //Debug.Log("OnDisable");
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

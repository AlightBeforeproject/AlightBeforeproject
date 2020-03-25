using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class HandleTextFile : MonoBehaviour
{
    //CharacterGenerator thisCharacter;
    //test
    [Header("Health Settings")]
    public EnemyMatCtrl enemyMatCtrl;
    public GameObject totenKiller;
    GameObject enemyPrefab;
    SaveLoadGame saveLoad;
    SaveLoadGame newSaveLoad = new SaveLoadGame();
    public List<GameObject> enemyList = new List<GameObject>();
    PlayerComand playerCmd;
    GameObject playerPrefab;
    SphereCast sphereCast;
    public GameObject gamePrefab;
    public int holdHandle = 0;
    public static bool blockBattle = false;
    

    //public static int wallHint = 0;

    bool podeAparecer = false;

    [Header("Death Settings 2")]
    public static HandleTextFile instance;
    //public bool morreuTodos = false;
    [SerializeField]
    private int morreuTodos = 0;

    public enum enemySpawnRatio
    {
        Low,
        Medium,
        High
    }

    public Transform selfSpawner;

    

    [SerializeField]
    private GameObject enemyObj;
    

    [SerializeField]
    private int xOffset = 1;

    [SerializeField]
    private int zOffset = 1;

    [SerializeField]
    public float _mapWidth;
    public float mapWidth
    {
        get
        {
            return _mapWidth;
        }
        private set
        {
            _mapWidth = value;
        }
    }
    [SerializeField]
    public float _mapHeight;
    public float mapHeight
    {
        get
        {
            return _mapHeight;
        }
        private set
        {
            _mapHeight = value;
        }
    }
    [SerializeField]
    public float _mapSizeY;
    public float mapSizeY
    {
        get
        {
            return _mapSizeY;
        }
        private set
        {
            _mapSizeY = value;
        }
    }   

    public List<Vector3> enemPosition;

    //TextAsset temp = Resources.Load("map.txt") as TextAsset;

    //TextAsset _text = new

    // maneira a ser lida pelo editor da unity
    string filePath = "Assets\\StreamingAssets\\";

    // maneira para enviar para a build    
    //string filePath = "A Light Before - Prototype_Data\\StreamingAssets\\";

    //Random rnd = new Random();
    [SerializeField]
    int testeRand = 0;
    int selectRand = 0;

    [SerializeField]
    Vector2 spawnRatioLowRange;
    [SerializeField]
    Vector2 spawnRatioMediumRange;
    [SerializeField]
    Vector2 spawnRatioHighRange;

    public int numInimigos = 0;

    string filename;
    string txtContents;
    string text;

    string[] map = new string[5];
    string[] recebeMap = new string[5];
    string s;
    int[] linha = new int[3];
    int[] coluna = new int[5];
    public int lines = 0;
    public int columns = 0;

    public int spawnID = 0;
    public int MeuSpawnID = 0;
    public string myId;
    public GameObject[] objs;
    public enemySpawnRatio spawnDensity;

    #region leituraInimigos

    FileInfo theSourceFile = null;
    StreamReader reader = null;
    string text2 /*= " "*/; // assigned to allow first line to be read below
    char[] delimiter2 = { ':' };
    string[] fields2 = null;

    #endregion

    

    void Start()
    {
        //sphereCast = new SphereCast();

        WriteText("test_", lines, columns);
        createMap("map");

        selfSpawner = GameObject.FindGameObjectWithTag("Spawner").transform;
        enemPosition = new List<Vector3>();

        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        gamePrefab = GameObject.FindGameObjectWithTag("Game");

        playerCmd = playerPrefab.GetComponent<PlayerComand>();
        saveLoad = gamePrefab.GetComponent<SaveLoadGame>();

        sphereCast = playerCmd.GetComponent<SphereCast>();

        // maneira a ser lida pelo editor da unity        
        theSourceFile = new FileInfo("Assets\\StreamingAssets\\test_" + myId + ".txt");

        //maneira para enviar para build
        //theSourceFile = new FileInfo("A Light Before - Prototype_Data\\StreamingAssets\\test_" + myId + ".txt");

        reader = theSourceFile.OpenText();

        //createEnemies("map");
        //print("passou aqui");
        //LoadText("map");

        createEnemies(lines);

        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyMatCtrl = enemyList[i].GetComponent<EnemyMatCtrl>();
        }
    }

    void Update()
    {
        if (playerCmd.inimigoReset)
        {
            recreateEnemies();            
            //print("entrou");
        }        

        if(morreuTodos == 1)
        {
            podeAparecer = true;
        }


        
        //print("morreuTodos: " + morreuTodos);
        //print(numInimigos);
        //createMap("map");
        //WriteText("test");
        //LoadText("test");
    }

    //MemoryStream stream = new MemoryStream(byteArray);

    public void WriteText(string writeTxt, int lines, int columns)
    {
        filename = filePath + writeTxt + myId + ".txt";
        StreamWriter myStrWriter = new StreamWriter(filename);
        int enemyCount = 0;
        int numEnemies = 0;
        List<Vector2> enemiesPosition = new List<Vector2>();

        switch (spawnDensity)
        {
            case enemySpawnRatio.Low:
                numEnemies = (int)Random.Range(spawnRatioLowRange.x, spawnRatioLowRange.y);
                break;
            case enemySpawnRatio.High:
                numEnemies = (int)Random.Range(spawnRatioHighRange.x, spawnRatioHighRange.y);
                break;
            case enemySpawnRatio.Medium:
            default:
                numEnemies = (int)Random.Range(spawnRatioMediumRange.x, spawnRatioMediumRange.y);
                break;
        }
        //print("NUMENEMIES " + numEnemies);

        for (int i = 0; i < numEnemies; i++)
        {
            int xis = Random.Range(0, lines - 1);
            int xis2 = Random.Range(0, columns - 1);
            enemiesPosition.Add(new Vector2(xis, xis2));
            //print("enemiesPoss X : " + xis);
            //print("enemiesPoss Y : " + xis2);
        }

        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                bool spawned = false;

                foreach (Vector2 pos in enemiesPosition)
                {
                    if (i == pos.x && j == pos.y)
                    {
                        spawned = true;
                    }
                }

                enemiesPosition.Remove(enemiesPosition.Find(ep => ep.x == i && ep.y == j));

                if (spawned)
                {
                    myStrWriter.Write(1 + ":");
                    enemyCount++;
                }
                else
                {
                    myStrWriter.Write(0 + ":");
                }

                //coluna[i] = testeRand;
                //linha[j] = testeRand;

            }
            myStrWriter.Write(myStrWriter.NewLine);
        }


        //myStrWriter.WriteLine("Done");

        myStrWriter.Flush();
        myStrWriter.Close();
    }

    public void createMap(string mapType)
    {
        mapWidth = selfSpawner.transform.position.x;
        mapHeight = selfSpawner.transform.position.z;
        mapSizeY = selfSpawner.transform.position.y;

        filename = filePath + mapType + ".txt";
        StreamReader myMapCreator = new StreamReader(filename);

        //string s = myMapCreator.ReadLine();
        string s = myMapCreator.ReadToEnd();
        char[] delimiter = { ':' };
        string[] fields = s.Split(delimiter);

    }

    public void createEnemies(int lines)
    {
        EnemyMatCtrl.morreuBlock = false;
        text2 = reader.ReadLine();
        int i = 0;

        while (text2 != null)
        {
            fields2 = text2.Split(delimiter2);
            for (int j = 0; j < 5; j++)
            {
                if (fields2[j] == "1")
                {
                        numInimigos += 1;                        
                        //print(fields2[j]);
                        enemyPrefab = Instantiate
                        (enemyObj, new Vector3(10 * j + mapWidth, mapSizeY, mapHeight - i * 10.0f),
                        Quaternion.identity) as GameObject;

                    enemyPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

                    enemyPrefab.GetComponent<EnemyMatCtrl>().totenKiller = totenKiller;                    

                    enemyList.Add(enemyPrefab);

                    enemPosition.Add(new Vector3(
                    enemyPrefab.transform.position.x,
                    enemyPrefab.transform.position.y,
                    enemyPrefab.transform.position.z));

                    enemyPrefab.GetComponent<Inimigo>().init();
                    enemyPrefab.SetActive(false);                    
                }
                
            }
            
            text2 = reader.ReadLine();
            i++;
        }
        reader.Close();
    }

    public void recreateEnemies()
    {

        //print("entrou para reset");
        for (int i = 0; i < enemPosition.Count; i++)
        {
            enemyList[i].transform.position = enemPosition[i];
            enemyList[i].GetComponent<EnemyController>().runOverPlayer = false;
            enemyList[i].GetComponent<Inimigo>().run = false;
        }
        playerCmd.inimigoReset = false;
        
        //for (int i = 0; i < numInimigos; i++)
        //{
        //    enemyPrefab.transform.position = enemPosition[i];
        //}
    }


    //filename = filePath + mapType + ".txt";
    //StreamReader myMapCreator2 = new StreamReader(filename);
    //myMapCreator2.Read();

    //theSourceFile = new FileInfo(filename);
    //reader = theSourceFile.OpenText();

    //string s = myMapCreator2.ReadLine();
    //string s = myMapCreator2.ReadToEnd();
    //char[] delimiter = { ':' };
    //string[] fields = s.Split(delimiter);

    //text2 = reader.ReadLine();
    //if (text2 != null)
    //{
    //    fields2 = text2.Split(delimiter2);
    //    print(fields2[0]);
    //    //for (int i = 0; i < 5; i++)
    //    //{
    //    //    if (fields2[0] == "1")
    //    //    {
    //    //        GameObject enemyPrefab = Instantiate
    //    //                (enemyObj, new Vector3(xOffset /*+ i*/ /** j*/ - mapWidth * xOffset,
    //    //                0, zOffset * mapHeight /*- i*/ * zOffset),
    //    //                Quaternion.identity) as GameObject;

    //    //    }
    //    //}
    //    //for (int i = 0; i < 5; i++)
    //    //{
    //    //    print(fields2[i]);
    //    //}





    //for (int i = 0; i < 3; i++)
    //{
    //    for (int j = 0; j < 5; j++)
    //    {
    //        if (fields[i][j] == "1")
    //        {
    //            GameObject enemyPrefab = Instantiate
    //                    (enemyObj, new Vector3(xOffset /** j*/ - mapWidth * xOffset,
    //                    0, zOffset * mapHeight - i * zOffset),
    //                    Quaternion.identity) as GameObject;

    //        }
    //        Debug.Log(fields[i]);
    //        myMapCreator2.Close();
    //    }
    //}

    public void LoadText(string loadTxt)
    {
        filename = filePath + loadTxt + myId + ".txt";
        StreamReader myStrReader = new StreamReader(filename);
        text = myStrReader.ReadToEnd();

        //txtContents = myStrReader

        myStrReader.Read();
        //Debug.Log(text);

        myStrReader.Close();
    }

    void OnGUI()
    {
        GUILayout.Label(text);
    }

    //void OnTriggerEnter(Collider outro)
    void OnTriggerStay()
    {
        //print("entrou no trigger do spawn");
        if (PlayerComand.battleActive == false &&
            spawnID == BlockBattle.blockAtual &&            
            SphereCast.colidiu2 == true)
        {
            
            if (morreuTodos == 0 && podeAparecer == false)
                ///*&& spawnID == gamePrefab.GetComponent<SaveLoadGame>().spawnInt[1]*/)
            {
                GetComponent<HandleTextFile>().enabled = true;
            }
            else
            {
                GetComponent<HandleTextFile>().enabled = false;
            }
        

            totenKiller.GetComponent<ControllMaterial>().enabled = true;
            totenKiller.GetComponent<ControllMaterial>().tottenAtual = MeuSpawnID;

            if (morreuTodos != 1)
            {
                morreuTodos = SaveLoadGame.instance.spawnInt[MeuSpawnID];
            }
            //print("ID atual: " + gamePrefab.GetComponent<SaveLoadGame>().spawnInt[MeuSpawnID]);
            //print("MT: " + morreuTodos);
        }

        //if (SphereCast.colidiu2 && BlockBattle.blockAtual == BlockBattle.blockActive &&
        //    GetMorreu() == 0)
        //{
        //    blockBattle = true;
        //}
    }
    void OnTriggerExit(Collider outro2)
    {
        if (GetComponent<HandleTextFile>().enabled == true)
        {            
            if (enemyMatCtrl.morrer)
            {
                print("matou todos");
                totenKiller.GetComponent<ControllMaterial>().enabled = false;
                morreuTodos = 1;
                
                //wallHint += 1;
                BlockWall.spawnsLimit += 1;                
                
                podeAparecer = true;
                GetComponent<HandleTextFile>().enabled = false;
            }            
        }
        SphereCast.colidiu2 = false;
    }

    public int GetMorreu()
    {
        return morreuTodos;
    }

    public void setMorreu(int recebeMorreu)
    {
       morreuTodos = recebeMorreu;
    }

    public void resetMorte()
    {
        morreuTodos = 0;
    }

    public void morte()
    {
        //print(SaveLoadGame.instance.spawnInt.Count);
        if (SaveLoadGame.instance.spawnInt.Count > 0)
        {
        morreuTodos = SaveLoadGame.instance.spawnInt[MeuSpawnID];
        }
    }

    //public void randomFuncEasy()
    //{
    //    // 0 e 2 pq eu sorteio apenas 1 e 0, pq meu algoritmo lê 1 como inimigo e 0 como tile
    //    selectRand = Random.Range(0, 2);
    //}
}
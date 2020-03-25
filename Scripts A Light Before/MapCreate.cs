using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class MapCreate : MonoBehaviour
{
    //CharacterGenerator thisCharacter;
    //test

    public static int numMap = 0;

    public Transform selfSpawner;

    [SerializeField]
    private GameObject gridTile;

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
    public int spawnId = 0;
    

    #region leituraInimigos

    FileInfo theSourceFile = null;
    StreamReader reader = null;
    string text2 /*= " "*/; // assigned to allow first line to be read below
    char[] delimiter2 = { ':' };
    string[] fields2 = null;

    #endregion

    
    void Start()
    {
        //WriteText("test_", lines, columns);
        createMap("map");

        selfSpawner = GameObject.FindGameObjectWithTag("Spawner").transform;

        
    }

    void Update()
    {
        
        //print(numInimigos);
        //createMap("map");
        //WriteText("test");
        //LoadText("test");
    }

    //MemoryStream stream = new MemoryStream(byteArray);

    //public void WriteText(string writeTxt, int lines, int columns)
    //{
    //    filename = filePath + writeTxt + spawnId + ".txt";
    //    StreamWriter myStrWriter = new StreamWriter(filename);
    //    int enemyCount = 0;
    //    int numEnemies = 0;
    //    List<Vector2> enemiesPosition = new List<Vector2>();

        

    //    for (int i = 0; i < lines; i++)
    //    {
    //        for (int j = 0; j < columns; j++)
    //        {
                

    //            //coluna[i] = testeRand;
    //            //linha[j] = testeRand;

    //        }
    //        myStrWriter.Write(myStrWriter.NewLine);
    //    }

    //    myStrWriter.Flush();
    //    myStrWriter.Close();
    //}

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

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject TilePrefab = Instantiate
                (gridTile, new Vector3(10 * j + mapWidth, mapSizeY, mapHeight - i * 10.0f),
                Quaternion.identity) as GameObject;
            }           
            myMapCreator.Close();
        }
    }

   
    public void LoadText(string loadTxt)
    {
        filename = filePath + loadTxt + spawnId + ".txt";
        StreamReader myStrReader = new StreamReader(filename);
        text = myStrReader.ReadToEnd();        

        myStrReader.Read();

        myStrReader.Close();
    }

    void OnGUI()
    {
        GUILayout.Label(text);
    }

    //void OnTriggerEnter(Collider outro)
    //{
    //    print("entrou no trigger do spawn");
    //    GetComponent<HandleTextFile>().enabled = true;
    //}

    // maneira a ser lida pelo editor da unity        
    //theSourceFile = new FileInfo("Assets\\StreamingAssets\\test_" + spawnId + ".txt");

    //maneira para enviar para build
    //theSourceFile = new FileInfo("A Light Before - Prototype_Data\\StreamingAssets\\test_" + spawnId + ".txt");

    //reader = theSourceFile.OpenText();

    //createEnemies("map");
    //print("passou aqui");
    //LoadText("map");

    //createEnemies(lines);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBattle : MonoBehaviour
{

    public GameObject totenKiller;

    public SphereCast sphereCast;
    public GameObject player;
    public Renderer rend;
    public MeshCollider rend2;

    public List<GameObject> blockList = new List<GameObject>();
    public List<GameObject> spawnerNumList = new List<GameObject>();
    //public List<GameObject> tottenNumList = new List<GameObject>();

    public static int blockAtual = 0;
    public static int blockActive = 50;

    //public int spawnBlock;

    private void Awake()
    {

    }

    void Start()
    {
        sphereCast = new SphereCast();

        player = GameObject.FindGameObjectWithTag("Player");
        sphereCast = player.GetComponent<SphereCast>();
        rend2 = GetComponent<MeshCollider>();

        rend = GetComponent<Renderer>();
        blockList[blockAtual].GetComponent<Renderer>().enabled = false;
        blockList[blockAtual].GetComponent<MeshCollider>().enabled = false;
        blockList[blockAtual].GetComponent<RotateItself>().enabled = false;

    }


    void Update()
    {
        //print("test block 0 : " + SpawnerNumList[0].GetComponent<HandleTextFile>().GetMorreu());
        //print("test block 1 : " + SpawnerNumList[1].GetComponent<HandleTextFile>().GetMorreu());
        //print("blockAtual: " + blockAtual);
        //print("print: " + HandleTextFile.blockBattle);
        //&& blockAtual == blockActive)
        /*&&
            SpawnerNumList[blockAtual].GetComponent<HandleTextFile>().spawnID == blockAtual ||
            HandleTextFile.blockBattle && blockAtual == blockActive
            && SpawnerNumList[blockAtual].GetComponent<HandleTextFile>().GetMorreu() == 0
        */

        //print("atual: " + blockAtual);
        //print("ativo: " + blockActive);

        //print("material ativo: " + tottenNumList[blockAtual].GetComponent<ControllMaterial>().material);
        //print("Block: " + HandleTextFile.blockBattle);
        //print("GetMorreu: " + spawnerNumList[blockAtual]/*.GetComponent<HandleTextFile>().GetMorreu()*/);

        if (HandleTextFile.blockBattle && EnemyMatCtrl.morreuBlock == false &&
            spawnerNumList[blockAtual].GetComponent<HandleTextFile>().GetMorreu() != 1)
        {
            blockList[blockAtual].GetComponent<Renderer>().enabled = true;
            blockList[blockAtual].GetComponent<MeshCollider>().enabled = true;
            blockList[blockAtual].GetComponent<RotateItself>().enabled = true;
            ////rend.enabled = true;
            //rend2.enabled = true;
        }
        else
        {  
            blockList[blockAtual].GetComponent<Renderer>().enabled = false;
            blockList[blockAtual].GetComponent<MeshCollider>().enabled = false;
            blockList[blockAtual].GetComponent<RotateItself>().enabled = false;
        }
    }
}
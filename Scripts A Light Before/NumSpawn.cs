using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumSpawn : MonoBehaviour
{

    public int numSpawnVar = 0;


    public SphereCast sphereCast = new SphereCast();
    PlayerComand playerCmd;
    GameObject playerPrefab;


    // Use this for initialization
    void Start()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerCmd = playerPrefab.GetComponent<PlayerComand>();
        sphereCast = playerCmd.GetComponent<SphereCast>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        BlockBattle.blockAtual = numSpawnVar;
    }
    void OnTriggerExit(Collider outro2)
    {
        sphereCast.colidiu = false;
        //HandleTextFile.blockBattle = false;
    }
}

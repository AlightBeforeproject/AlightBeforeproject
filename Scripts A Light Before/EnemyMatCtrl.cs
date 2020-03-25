using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMatCtrl : MonoBehaviour
{

    //GameObject enemyRender;
    //Renderer rend;
    //Shader shader1;
    GameObject player;
    [SerializeField]
    List<GameObject> enemyList;
    public float amount = 0.0f;
    ControllMaterial ctrlMaterial;
    //GameObject objects;
    EnemyController enemyCtrl;
    public GameObject totenKiller;
    PlayerComand playerCmd;

    public bool morrer = false;
    public static bool morreuBlock = false;


    // Use this for initialization
    void Start()
    {
        //enemyRender = GameObject.FindGameObjectWithTag("Inimigo");


        //rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Toon/Lit Dissolve");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyList = new List<GameObject>();
        enemyList = GameObject.FindGameObjectsWithTag("Inimigo").ToList();
        //objects = GameObject.FindGameObjectWithTag("HideObj");

        ctrlMaterial = totenKiller.GetComponent<ControllMaterial>();

        enemyCtrl = GetComponent<EnemyController>();

        playerCmd = player.GetComponent<PlayerComand>();

    }

    // Update is called once per frame
    void Update()
    {
        //enemyRender.GetComponent<Renderer>().material.SetFloat("_DisAmount", amount);

        acess();
    }

    void acess()
    {
        //if (rend.material.GetFloat("_DisAmount") >= 0.5f)
        //{
        //    print("Alterei o inimigo");
        //}

        //for (int i = 0; i < enemyList.Count; i++)
        //{
        //    //Destroy(enemyList[i]);
        //    enemyList[i].GetComponent<Renderer>().material.SetFloat("_DisAmount", amount);
        //}
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            //Destroy(enemyList[i]);            
            if (enemyList[i] != null)
            {
                enemyList[i].GetComponent<Renderer>().material.SetFloat("_DisAmount", amount);
            }
            else
            {
                if (amount <= 1)
                {
                    enemyList.RemoveAt(i);
                }
            }
        }

        if (ctrlMaterial.podeDesaparecer && amount <= 1)
        {
            enemyCtrl.animMorte = true;
            enemyCtrl.runOverPlayer = false;
            PlayerComand.battleActive = false;
            SphereCast.colidiu2 = false;
            amount += 0.005f;
            //print("passou aqui");
        }
        if (amount >= 1)
        {
            Die();            
            amount = 0;
            
        }

        //ctrlMaterial.podeDesaparecer = false;

        //if (Input.GetKey(KeyCode.T))
        //{   
        //    //amount += 0.01f;
        //}



    }

    public void Die()
    {
        Destroy(gameObject);
        morrer = true;
        morreuBlock = true;
        HandleTextFile.blockBattle = false;
        BlockBattle.blockActive = 50;
    }
}

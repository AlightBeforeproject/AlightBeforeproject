using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SphereCast : MonoBehaviour
{
    public float enemyId = 0;
    public Material matt;
    public float radius = 0f;
    public Renderer[] renders;
    public Collider[] coliders;
    public Inimigo[] inimigos;
    public LayerMask mask;
    private Vector3 origin = Vector3.zero;

    HandleTextFile handText;
    List<GameObject> handleList;
    GameObject handle;

    ShaderController shaderCtrl;

    public UnityEvent awakeEnemies;

    //[SerializeField]
    public bool colidiu = false;
    public static bool colidiu2 = false;

    //public Jogador jogador;

    [SerializeField]
    public int atualNumber = 0;

    //// Use this for initialization
    void Start()
    {
        //jogador = GetComponent<Jogador>();
        handleList = new List<GameObject>();

        handle = GameObject.FindGameObjectWithTag("Spawner");
        handleList = GameObject.FindGameObjectsWithTag("Spawner").ToList();
        shaderCtrl = GetComponent<ShaderController>();

        for (int i = 0; i < handleList.Count; i++)
        {
            handText = handleList[i].GetComponent<HandleTextFile>();
        }
    }

    //// Update is called once per frame
    //void Update () {

    //}

    void LateUpdate() //void Update()
    {
        //print("print2: " + colidiu2);

        coliders = Physics.OverlapSphere(transform.position, radius, mask);
        foreach (Collider col in coliders) // para cada vez que colide, ele faz isso
        {
            if (col.tag == "Other")
            {
                colidiu = true;
                colidiu2 = true;

                inimigos = Resources.FindObjectsOfTypeAll<Inimigo>();
                if (BlockBattle.blockActive == 50)
                {
                    HandleTextFile.blockBattle = true;
                }
                if (BlockBattle.blockActive == BlockBattle.blockAtual)
                {
                    HandleTextFile.blockBattle = true;
                }

                //print("inimigos: " + inimigos.Length);
                for (int i = 0; i < inimigos.Length; i++)
                {
                    inimigos[i].gameObject.SetActive(true);
                }
                awakeEnemies.Invoke();
                col.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        radius = shaderCtrl.cRadius;
        //if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("RB_Button"))
        //{
        //    //radius += 0.5f;
        //    radius = shaderCtrl.cRadius;
        //}
        //else
        //{
        //    if (radius >= 0)
        //    {
        //        radius -= 0.1f;
        //    }            
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    
}

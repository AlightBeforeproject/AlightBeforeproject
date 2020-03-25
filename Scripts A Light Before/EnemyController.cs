using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    UnityEvent awakeListener;

    //Transform Player;

    public BoxCollider playerRange;
    Inimigo inimigo;
    LifeComand lifeCmd;
    GameObject Player;
    ControllMaterial ctrlMaterial;
    public CharacterController charCtrl;
    int MoveSpeed = 10;
    int MaxDist = 10;
    int MinDist = 4;
    int seconds = 0;
    float timer = 0.0f;
    float enemyDamage = 0.1f;
    public float enemyLife = 150f;
    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    PlayerComand playerCmd;
    Vector3 velocity = Vector3.zero;
    CameraShaker camShaker;
    public GameObject cam;

    //public Transform target;
    //private NavMeshHit hit;
    //public bool blocked = false;
    //public bool podeContornar = false;
    //public bool needScape = false;
    //public bool coliderEntered = false;
    public bool chegouPerto = false;
    public bool coliderOut = false;
    //public bool outedColiders = false;
    public bool podeAtacar = false;
    public bool animMorte = false;
    //public bool esconder = false;
    public bool runOverPlayer = false;

    public Transform targetObstacle;
    public Transform lastObstacle;

    public Vector3 dir = new Vector3();

    RaycastHit hit2;

    EnemyController script;

    HealthBarPlayer healthBarPlayer;

    public GameObject obstacles;
    //public GameObject RayDirections;
    //public Rigidbody rb;

    List<GameObject> obsList;

    

    //int sizeList = 5;
    private Transform t;

    private Transform obsPos;

    private Transform playerPos;

    private float range = 10.0f;

    public bool getCloset = false;
    Vector3 tempVect = new Vector3(0, 0, 0);

    float step = 0.0f;
    //    public bool isAwake = false;
    //public bool isAwake = false;
    CharacterController mover;

    public Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerRange = Player.gameObject.GetComponent<BoxCollider>();
        healthBarPlayer = Player.gameObject.GetComponent<HealthBarPlayer>();
        script = GetComponent<EnemyController>();
        playerCmd = Player.gameObject.GetComponent<PlayerComand>();
        lifeCmd = Player.gameObject.GetComponent<LifeComand>();

        charCtrl = Player.GetComponent<CharacterController>();
        inimigo = GetComponent<Inimigo>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        t = this.transform;

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camShaker = cam.GetComponent<CameraShaker>();

        mover = GetComponent<CharacterController>();
        
        //awakeListener = Player.GetComponent<SphereCast>().awakeEnemies;
        //awakeListener.AddListener(WakeUp);
        //enemyAI_ref = GetComponent<EnemyAI>();
        //rb = GetComponent<Rigidbody>();


    }

    void Update()
    {


        if (inimigo.render.enabled == true /*&& !runOverPlayer*/)
        {
            inimigo.idle = true;
            //if (DistPlayerTotten() <= 38.0f)
            if (DistPlayerEnemy() <= 28.0f && playerCmd.inimigoReset == false)
            {
                runOverPlayer = true;
                //playerCmd.inimigoReset = false;
            }
        }

        if (runOverPlayer && playerCmd.inimigoReset == false)
        {
            RunOver();
        }
        else
        {
            //runOverPlayer = false;
            podeAtacar = false;
        }

        if (podeAtacar)
        {
            inimigo.atack = true;
            inimigo.run = false;
        }
        else
        {
            inimigo.atack = false;
            if (runOverPlayer)
            {
                inimigo.run = true;
            }
        }
        if (animMorte)
        {
            inimigo.death = true;
            inimigo.atack = false;
            inimigo.run = false;
            inimigo.atack = false;
            inimigo.run = false;            
        }
        else
        {
            inimigo.death = false;
        }
        if (playerCmd.inimigoReset)
        {
            inimigo.atack = false;
            inimigo.run = false;
        }

        if (impact.magnitude > 0.2F) charCtrl.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);


    }


    //void evadeObstacle(Transform target)
    //{
    //    target = enemyAI_ref.GetFurthestObstacle().transform;
    //    dir = (target.position - transform.position).normalized;

    //    if (Physics.Raycast(transform.position, transform.forward, out hit2, 30) && !hit2.collider.isTrigger
    //        && hit2.collider.tag == "Obstacles")
    //    {
    //        if (hit2.transform != transform)
    //        {
    //            Debug.DrawLine(transform.position, hit2.point, Color.red);
    //            dir += hit2.normal * 50;
    //        }
    //    }

    //    Vector3 leftR = transform.position;
    //    Vector3 rightR = transform.position;

    //    leftR.x -= 2;
    //    rightR.x += 2;

    //    if (Physics.Raycast(leftR, transform.forward, out hit2, 30) && !hit2.collider.isTrigger
    //        && hit2.collider.tag == "Obstacles")
    //    {
    //        if (hit2.transform != transform)
    //        {
    //            Debug.DrawLine(leftR, hit2.point, Color.red);
    //            dir += hit2.normal * 50;
    //        }
    //    }
    //    if (Physics.Raycast(rightR, transform.forward, out hit2, 30) && !hit2.collider.isTrigger
    //        && hit2.collider.tag == "Obstacles")
    //    {
    //        if (hit2.transform != transform)
    //        {
    //            Debug.DrawLine(rightR, hit2.point, Color.red);
    //            dir += hit2.normal * 50;
    //        }
    //    }

    //    if (!coliderEntered)
    //    {
    //        Quaternion rot = Quaternion.LookRotation(dir);

    //        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
    //        transform.position += transform.forward * 4 * Time.deltaTime;
    //    }

    //    //}
    //}

    //void RunForest()
    //{
    //    inimigo.run = true;
    //    transform.LookAt(enemyAI_ref.GetFurthestObstacle().transform);
    //    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    //}

    void RunOver()
    {
        transform.LookAt(Player.transform);
        //transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //velocity = Vector3.

        mover.Move(transform.TransformDirection(Vector3.forward) * MoveSpeed * Time.deltaTime);

        //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);

    }

    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        dir.y = 0;
        //if (dir.z < 0) dir.z = -dir.z; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    //void timerFunc()
    //{
    //    timer += Time.deltaTime;
    //    seconds = (int)timer % 60;
    //}



    public float DistPlayerEnemy()
    {
        return Vector3.Distance(playerPos.position, t.position);
    }



    void OnTriggerEnter(Collider other)
    {
        //timerFunc();
        //print("seconds: " + seconds);

        //if (other.gameObject.tag == "Obstacles")
        //{
        //    if (!chegouPerto)
        //    {
        //        coliderEntered = true;
        //        //print("entrouColider");
        //    }
        //}
        //if (other.gameObject.tag == "outColiders_01" && esconder)
        //{
        //    print("passou onde eu quero");
        //    outedColiders = true;
        //}
        if (other == playerRange)
        {
            podeAtacar = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            coliderOut = true;
        }

        ////print("saiuColider");
        //if (other == playerRange)
        //{
        //    podeAtacar = false;
        //}
    }

    public void AttackAnimationEnded()
    {
        //AddImpact(Player.transform.position - transform.position , 50.0f);

        podeAtacar = false;

        if (DistPlayerEnemy() <= 8.0f)
        {
            lifeCmd.glow -= enemyDamage;
            //healthBarPlayer.TakeDamage(enemyDamage);
            ////playerCmd.charMove = true;
            ////playerCmd.charDamage = true;
            //playerCmd.playerLife -= enemyDamage;
        }
    }
}

/*void evadeObstacle()
{
    dir = (targetObstacle.position - transform.position).normalized;

    if (Physics.Raycast(transform.position, transform.forward, out hit2, 20) && !hit2.collider.isTrigger)
    {
        if (hit2.transform != transform)
        {
            Debug.DrawLine(transform.position, hit2.point, Color.red);
            dir += hit2.normal * 50;
        }
    }

    Vector3 leftR = transform.position;
    Vector3 rightR = transform.position;

    leftR.x -= 2;
    rightR.x += 2;

    if (Physics.Raycast(leftR, transform.forward, out hit2, 20) && !hit2.collider.isTrigger)
    {
        if (hit2.transform != transform)
        {
            Debug.DrawLine(leftR, hit2.point, Color.red);
            dir += hit2.normal * 50;
        }
    }
    if (Physics.Raycast(rightR, transform.forward, out hit2, 20) && !hit2.collider.isTrigger)
    {
        if (hit2.transform != transform)
        {
            Debug.DrawLine(rightR, hit2.point, Color.red);
            dir += hit2.normal * 50;
        }
    }

    Quaternion rot = Quaternion.LookRotation(dir);

    transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
    transform.position += transform.forward * 4 * Time.deltaTime;

}
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerComand : MonoBehaviour
{

    public Transform camPivot;
    ShaderController shaderCtrl;

    //public Transform checkpoint;
    float heading = 0;
    public float playerLife = 150f;
    public bool morreuLoad = false;
    public bool caiuAbismoSaved = false;

    public static bool battleActive = false;
    public List<GameObject> listFonts = new List<GameObject>();

    //[SerializeField]
    //public static bool gambiarra = false;

    public Transform cam;
    CharacterController mover;
    CameraController camController;
    LifeFountain lifeFountain;
    HealthBarPlayer healthBar;
    HandleTextFile handleTextFile;
    GameObject saveLoad;

    public static PlayerComand instance;

    Vector3 camF;
    Vector3 camR;

    Vector2 input;

    Vector3 intent;
    Vector3 velocity;

    [SerializeField]
    float speed = 10;
    float accel = 8;
    public float turnSpeed = 0;
    float gravity = 20.0F;
    //float velJump = 5.0F;
    float angle;

    public Vector3 moveDirection = Vector3.zero;

    public SphereCast sphereCast;

    Animator animator;
    GameObject Player;
    /*public*/
    GameObject camMouse;
    /*public*/
    GameObject camJoy;

    public List<GameObject> spawmList = new List<GameObject>();
    public GameObject fountain;
    public GameObject arma;

    //CharacterStates
    public bool charIdle = false;
    public bool charRun = false;
    //public bool charJump = false;
    public bool charDie = false;
    public bool charDamage = false;
    public bool charMove = false;
    public bool inimigoReset = false;
    public bool charStandardRun = false;
    

    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;

    private void Awake()
    {
        saveLoad = GameObject.FindGameObjectWithTag("Game");

        if (saveLoad.GetComponent<SaveLoadGame>().deuLoad)
        {
            transform.position = new Vector3(
            saveLoad.GetComponent<SaveLoadGame>().f_num_1 + 10.02f,
            saveLoad.GetComponent<SaveLoadGame>().f_num_2 + 2.0f,
            saveLoad.GetComponent<SaveLoadGame>().f_num_3);
        }
        else
        {
            transform.position = new Vector3(10f, 2f, 0f);
        }
    }


    // Use this for initialization
    void Start()
    {
        sphereCast = new SphereCast();
        spawmList = GameObject.FindGameObjectsWithTag("Spawner").ToList();

        listFonts = GameObject.FindGameObjectsWithTag("LifePoint").ToList();
        //lifeFountain = fountain.GetComponentInChildren<LifeFountain>();

        shaderCtrl = this.gameObject.GetComponent<ShaderController>();

        Player = GameObject.FindGameObjectWithTag("Player");
        mover = GetComponent<CharacterController>();
        camController = cam.gameObject.GetComponent<CameraController>();
        healthBar = gameObject.GetComponent<HealthBarPlayer>();
        sphereCast = Player.GetComponent<SphereCast>();

        for (int i = 0; i < listFonts.Count; i++)
        {
            lifeFountain = listFonts[i].GetComponent<LifeFountain>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("PLife: " + playerLife);
        //print("Hit: " + healthBar.hitpoint);
        //print("Gamb: " + gambiarra);
        if (mover.isGrounded)
        {
            //print("CharacterController is grounded");
            SaveLoadGame.instance.deuLoad = false;
            //charJump = false;
        }
        if (playerLife <= 0 /*|| Input.GetKey(KeyCode.L)*/)
        {
            //print("MORREU");
            if (SaveLoadGame.instance.deuLoad)
            {
                transform.position = new Vector3(
            saveLoad.GetComponent<SaveLoadGame>().f_num_1 + 10.02f,
            saveLoad.GetComponent<SaveLoadGame>().f_num_2,
            saveLoad.GetComponent<SaveLoadGame>().f_num_3);

            }
            else
            {
                transform.position = new Vector3(
                    SaveLoadGame.instance.loadPos_x + 10.02f,
                    SaveLoadGame.instance.loadPos_y,
                    SaveLoadGame.instance.loadPos_z);

            }
            morreuLoad = true;
            inimigoReset = true;
            HandleTextFile.blockBattle = false;
            sphereCast.colidiu = false;
            battleActive = true;
            StartCoroutine("ResetPlayer");
            BlockBattle.blockActive = BlockBattle.blockAtual;
            //SaveLoadGame.instance.recebeVida = 150;
        }
        animator = GetComponent<Animator>();

        if (charMove == false && PauseMenu.gameIsPaused == false && morreuLoad == false && LoadCanvas.clicou == false)
        {
            DoInput();
            DoMove();
        }

        CalculateCamera();
        setAnimations();


        moveDirection.y -= gravity * Time.deltaTime;
        mover.Move(moveDirection * Time.deltaTime);
    }

    void DoInput()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        if (
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetAxis("Vertical") > 0 ||
            Input.GetAxis("Vertical") < 0 ||
            Input.GetAxis("Horizontal") > 0 ||
            Input.GetAxis("Horizontal") < 0
            )
            charRun = true;
        else
            charRun = false;
    }

    void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void DoMove()
    {
        intent = camF * input.y + camR * input.x;

        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        velocity = Vector3.Lerp(velocity, transform.forward * input.magnitude * speed, accel * Time.deltaTime);

        mover.Move(velocity * Time.deltaTime);

        //if (Input.GetMouseButton(0) || Input.GetButton("RT_Button"))
        //{
        //    //Debug.Log("Hitou");
        //    armas[0].atirar();
        //}


        //if (Input.GetKey(KeyCode.Space) && charJump == false ||
        //     Input.GetButtonDown("A_Button") && charJump == false)
        //{
        //    moveDirection.y = velJump;
        //    charJump = true;
        //}

        if (charRun && Input.GetKey(KeyCode.LeftShift)
            || charRun && Input.GetButton("LT_Button"))
        {
            speed = 25.0f;
            charStandardRun = true;
        }
        else
        {
            speed = 10.0f;
            charStandardRun = false;
        }

        //if (Input.GetMouseButton(1) || Input.GetButton("LT_Button"))
        //{
        //    //arma.SetActive(!arma.activeSelf);            
        //    arma.SetActive(true);
        //    //if (camController.inCombat == true)
        //    //    turnSpeed = 1.5f;
        //}
        //else
        //{
        //    if (arma.activeSelf == true)
        //    {
        //        arma.SetActive(false);
        //        turnSpeed = 9f;
        //    }
        //}

        //transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * 5;
    }

    void setAnimations()
    {
        if (charIdle)
            animator.SetBool("Idle", true);
        else
            animator.SetBool("Idle", false);
        if (charRun)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);
        if (shaderCtrl.charLight)
            animator.SetBool("Lighting", true);
        else
            animator.SetBool("Lighting", false);
        //if (charJump)
        //    animator.SetBool("Jump", true);
        //else
        //    animator.SetBool("Jump", false);
        //if (charDamage)
        //    animator.SetBool("Damage", true);
        //else
        //    animator.SetBool("Damage", false);
        if (charStandardRun)
            animator.SetBool("StandRun", true);
        else
            animator.SetBool("StandRun", false);


        //if (charDie)
        //    animator.SetBool("Die", true);
        //else
        //    animator.SetBool("Die", false);
    }

    public IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        playerLife = 150;
        healthBar.hitpoint = 150;
        healthBar.UpdateHealthBar();
    }

    public void HittedAtack()
    {
        charDamage = false;
        charMove = false;
        //print("HittedAtack");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            for (int i = 0; i < listFonts.Count; i++)
            {
                //print(listFonts[i].GetComponent<LifeFountain>().savedTrigger);
                if (listFonts[i].GetComponent<LifeFountain>().savedTrigger)
                {
                    caiuAbismoSaved = true;
                }
            }

            if (caiuAbismoSaved)
            {
                print("Deucerto ");
                transform.position = new Vector3( x + 10.02f, y, z);                
            }
            else
            {
                transform.position = new Vector3(
                    SaveLoadGame.instance.loadPos_x + 10.02f,
                    SaveLoadGame.instance.loadPos_y,
                    SaveLoadGame.instance.loadPos_z);
                
            }
            morreuLoad = true;
            StartCoroutine("ResetPlayer");
        }
    }

    //public void EndJump()
    //{
    //    charJump = false;
    //    //print("jumped");
    //}
}
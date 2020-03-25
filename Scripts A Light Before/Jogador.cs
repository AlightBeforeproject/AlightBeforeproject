using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    //Variaveis
    [SerializeField]
    float life = 100.0f;
    float vel = 12.0F;
    float velTurn = 90F;
    float velJump = 8.0F;
    float gravity = 20.0F;
    float turnSpeed = 100;
    //CharacterStates
    bool charIdle = false;
    bool charRun = false;
    bool charJump = false;
    float angle;

    public Vector3 moveDirection = Vector3.zero;

    //Objetos e classes
    Animator animator;
    CharacterController controller;
    Transform cam;
    Quaternion targetRotation;
    
    GameObject Player;
    [SerializeField]
    public GameObject arma;

    //Arma_00[] armas = new Arma_00[3];

    void Start()
    {
        //Armas myArmas = new Arma_00();

        //Arma_00 myArma_00 = (Arma_00)myArmas;

        cam = Camera.main.transform;

        

        //Player = GameObject.FindGameObjectWithTag("Player");

        //getArma = Player.gameObject.GetComponent<Arma_00>();



        //Fruit myFruit = new Apple();

        //myFruit.SayHello();
        //myFruit.Chop();

        //Fruit myApple = (Apple)myFruit;

        //myApple.SayHello();
        //myApple.Chop();



    }

    void Update()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GetInput();
        if (Mathf.Abs(moveDirection.x) < 1 && Mathf.Abs(moveDirection.y) < 1)
        {
            if (controller.isGrounded)
            {
                moveInDirections();
                inputControllers();
                setAnimations();
                Rotate();
                CalculateDirection();
                charJump = false;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void moveInDirections()
    {
        //transform.position += transform.forward * vel * Time.deltaTime;
        //Vector3 dir = (cam.right * Input.GetAxis("Horizontal")) + (cam.forward * Input.GetAxis("Vertical"));
        //dir.y = 0;

        //moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //moveDirection = transform.TransformDirection(moveDirection);
        //transform.Rotate(Vector3.up, velTurn * Input.GetAxis("Horizontal") * Time.deltaTime);
        moveDirection *= vel;
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

    void GetInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(moveDirection.x, moveDirection.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void inputControllers()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, velTurn * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -velTurn * Time.deltaTime);
        }
        if (Input.GetMouseButton(0) || Input.GetButton("RT_Button"))
        {
            //Debug.Log("Hitou");            
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump2"))
        {
            moveDirection.y = velJump;
            charJump = true;
        }
        //if (Input.GetMouseButton(1) || Input.GetButton("LT_Button"))
        //{
        //    //arma.SetActive(!arma.activeSelf);
        //    //arma.SetActive(true);
        //}
        else
        {
            if (arma.activeSelf == true)
            {
                arma.SetActive(false);
            }
        }
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
        if (charJump)
            animator.SetBool("Jump", true);
        else
            animator.SetBool("Jump", false);
    }
}

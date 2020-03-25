using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float CameraMoveSpeed = 120.0f;
	public GameObject CameraFollowObj;
	Vector3 FollowPOS;
	public float clampAngle = 80.0f;
	public float inputSensitivity = 150.0f;
	public GameObject CameraObj;
	public GameObject PlayerObj;
	public float camDistanceXToPlayer;
	public float camDistanceYToPlayer;
	public float camDistanceZToPlayer;
	public float mouseX;
	public float mouseY;
	public float finalInputX;
	public float finalInputZ;
	public float smoothX;
	public float smoothY;
	private float rotY = 0.0f;
	private float rotX = 0.0f;
    GameObject saveLoad;

    public bool podeRodar = false;


    private void Awake()
    {
        saveLoad = GameObject.FindGameObjectWithTag("Game");

        if (saveLoad.GetComponent<SaveLoadGame>().deuLoad || PlayerObj.GetComponent<PlayerComand>().inimigoReset == true)
        {
            transform.position = new Vector3(
            saveLoad.GetComponent<SaveLoadGame>().f_num_1 + 10.02f,
            saveLoad.GetComponent<SaveLoadGame>().f_num_2 + 2.0f,
            saveLoad.GetComponent<SaveLoadGame>().f_num_3);
        }
    }

    // Use this for initialization
    void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		
		
	}
	
	// Update is called once per frame
	void Update () {

		// We setup the rotation of the sticks here
		float inputX = Input.GetAxis ("RightStickHorizontal");
		float inputZ = Input.GetAxis ("RightStickVertical");

        //if (PlayerObj.GetComponent<PlayerComand>().inimigoReset)
        //{
        //    transform.position = new Vector3(
        //    saveLoad.GetComponent<SaveLoadGame>().f_num_1 + 10.02f,
        //    saveLoad.GetComponent<SaveLoadGame>().f_num_2 + 2.0f,
        //    saveLoad.GetComponent<SaveLoadGame>().f_num_3);
        //}


        if (PlayerPrefs.GetInt("joystick") == 1)
        {
            //print("changed to Joy");

            //if (Input.GetAxis("R_Y_Button") < 0)
            //{
            //    currentX += Input.GetAxis("R_Y_Button");
            //}
            //if (Input.GetAxis("R_Y_Button") > 0)
            //{
            //    currentX += Input.GetAxis("R_Y_Button");
            //}

            //mouseX = Input.GetAxis("Mouse X");
            //mouseY = Input.GetAxis("Mouse Y");
            finalInputX = inputX /*+ mouseX*/;
            finalInputZ = inputZ /*- mouseY*/;

            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputZ * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            
            transform.rotation = localRotation;
        }
        else
        {
            //print("changed to mouse");

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            finalInputX = inputX + mouseX;
            finalInputZ = inputZ - mouseY;

            if (Input.GetMouseButton(1))
            {
                rotY += finalInputX * inputSensitivity * Time.deltaTime;
                rotX += finalInputZ * inputSensitivity * Time.deltaTime;

                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

                Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
                //if (podeRodar==false)
                //{
                //    localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                //    podeRodar = true;
                //}
                transform.rotation = localRotation;
                //print(localRotation);
            }
        }






        //if (PauseMenu.gameIsPaused == false)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //CameraUpdater();
    }

    void LateUpdate () {
		CameraUpdater ();
	}

	void CameraUpdater()
    {
        // set the target object to follow
        Transform target = CameraFollowObj.transform;

        //move towards the game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;
        if (PlayerObj.GetComponent<PlayerComand>().morreuLoad)
        {
            transform.position = new Vector3(
            PlayerObj.GetComponent<PlayerComand>().transform.position.x + 10.02f,
            PlayerObj.GetComponent<PlayerComand>().transform.position.y + 2.0f,
            PlayerObj.GetComponent<PlayerComand>().transform.position.z);

            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //print("resetou");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}

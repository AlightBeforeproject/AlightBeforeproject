using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    GameObject cameraTarget;
    EnemyController enemyController;
    //HandleTextFile handleTextFile;
    PlayerComand playerCmd;
    GameObject Player;

    public GameObject spawm;

    public Transform target;
    public Transform target2;
    public float smoothSpeed = 0.125f;

    public float rotateSpeed = 5;

    Vector3 offset;
    Vector3 offset2;
    Vector3 lastPosition;
    public LayerMask RaycastLayers;
    float rotate = 0;
    public float offsetDistance;
    public float offsetHeight;
    public float smoothing;
    public float turnSpeed = 50F;

    bool following = true;
    public bool inCombat = false;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 60.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 10.0f;

    public float currentX = 0.0f;
    public float currentY = 45.0f;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;

    public Transform alvo;
    RaycastHit hit = new RaycastHit();
    public float mouseX = 0;
    public float mouseY = 0;
    public float distCam = 0;

    PauseMenu pauseMenu;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraTarget = GameObject.FindGameObjectWithTag("Player");
        lastPosition = new Vector3(cameraTarget.transform.position.x,
                                   cameraTarget.transform.position.y + Input.GetAxis("Horizontal"),
                                   cameraTarget.transform.position.z);

        offset = new Vector3(10, 4, 3);

        camTransform = transform;

        //handleTextFile = new HandleTextFile();
        spawm = GameObject.FindGameObjectWithTag("Spawner");
        //handleTextFile = spawm.GetComponent<HandleTextFile>();
        playerCmd = Player.gameObject.GetComponent<PlayerComand>();
    }

    //public void init()
    //{


    //}

    void Update()
    {
        //if(Input.GetKey(KeyCode.F))
        //{
        //	if(following)
        //	{
        //		following = false;
        //	} 
        //	else
        //	{
        //		following = true;
        //	}
        //} 

        //if (enemyController.isAwake == true)
        //    inCombat = true;
        //else
        //    inCombat = false;

        //inCombat = cameraTarget.GetComponent<SphereCast>().colidiu;
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

        //#region outCombat
        playerCmd.turnSpeed = 8;
        //currentX += Input.GetAxis("Mouse X");
        //currentY += Input.GetAxis("Mouse Y");

        //if (Input.GetAxis("R_X_Button") < 0)
        //{
        //    currentY -= Input.GetAxis("R_X_Button");
        //}
        //if (Input.GetAxis("R_X_Button") > 0)
        //{
        //    currentY -= Input.GetAxis("R_X_Button");
        //}
        //if (Input.GetAxis("R_Y_Button") < 0)
        //{
        //    currentX += Input.GetAxis("R_Y_Button");
        //}
        //if (Input.GetAxis("R_Y_Button") > 0)
        //{
        //    currentX += Input.GetAxis("R_Y_Button");
        //}
        ////print(Input.GetAxis("R_X_Button"));
        ////print(Input.GetAxis("R_Y_Button"));

        //currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        //#endregion

        //myUpdate();

        //verificarInimigos();

    }

    void myUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

        transform.position = alvo.position - transform.forward * distCam;

        if (Physics.Linecast(alvo.position, transform.position, out hit, RaycastLayers))
        {
            transform.position = hit.point + transform.forward * 0.2f;
        }
    }
}
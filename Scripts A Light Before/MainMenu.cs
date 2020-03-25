using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public static bool joystick = false;
    Toggle m_Toggle;

    public static MainMenu instance;

    void OnEnable()
    {
        m_Toggle = transform.GetComponentInChildren<Toggle>(true);
        if (PlayerPrefs.GetInt("joystick") == 1)
        {   
            m_Toggle.isOn = true;            
        }
        else
        {
            m_Toggle.isOn = false;
        }
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //if (PlayerPrefs.GetInt("joystick") == 1)
        //{
        //    m_Toggle.isOn = true;
        //}
        //else
        //{
        //    m_Toggle.isOn = false;
        //}
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void JoystickOn()
    {
        if (!m_Toggle.isOn)
        {
            PlayerPrefs.SetInt("joystick", 0);
            //m_Toggle.isOn = false;
        }
        else
        {
            PlayerPrefs.SetInt("joystick", 1);
            //m_Toggle.isOn = true;
        }
        //print("Log: " + PlayerPrefs.GetInt("joystick"));
    }    

    public void Exit()
    {
        Application.Quit();
    }
}

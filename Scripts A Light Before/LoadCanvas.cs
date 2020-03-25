using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadCanvas : MonoBehaviour, IPointerClickHandler
{
    private SaveLoadGame saveLoad;
    public PauseMenu pauseMenu;
    public GameObject loadUI;
    public float TempoFixoSeg = 5;
    
    public static bool clicou = false;

    // Use this for initialization
    void Start()
    {
        saveLoad = Object.FindObjectOfType<SaveLoadGame>();
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        clicou = true;        
        saveLoad.LoadText("SaveGame");
    }

    public void ButtonClicked()
    {
        clicou = true;        
        saveLoad.LoadText("SaveGame");
    }
}
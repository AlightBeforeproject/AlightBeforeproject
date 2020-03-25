using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogo : MonoBehaviour {

    // Use this for initialization
    JarraClass jarraClass;

    GameObject _Player;

    //public bool inGame = true;

	void Start ()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        jarraClass = _Player.gameObject.GetComponentInChildren<JarraClass>();
        
    }

    void Update()
    {
        //while (jarraClass.entities != 2)
        //{
        //    print("Playing game.");
        //}
    }

    public void StartGame()
    {
        
        //print("funcionou");
    }
}

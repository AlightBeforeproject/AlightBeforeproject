using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Jogo jogo;

    void Start()
    {
        jogo = new Jogo();
    }
    void Update ()
    {
        jogo.StartGame();
	}
}

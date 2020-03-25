using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarraClass : MonoBehaviour
{
    GameObject Player;
    PlayerComand playerCmd;    
    Animator animator;

    public int entities = 0;

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerCmd = Player.gameObject.GetComponent<PlayerComand>();
        animator = GetComponent<Animator>();
    }
	
	
	void Update ()
    {
        animationJarr();

    }

    public void animationJarr()
    {
        if (playerCmd.charRun)
            animator.SetBool("jarrRun", true);
        else
            animator.SetBool("jarrRun", false);
    }
}

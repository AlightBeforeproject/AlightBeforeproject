using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour {

    public int limiteFase = 0;

    public Material[] material;
    Renderer wallRend;

    public GameObject[] hideObsList;

    [SerializeField]
    public static int spawnsLimit = 0;

    Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
        wallRend = GetComponent<Renderer>();
        wallRend.material.shader = Shader.Find("Standard");
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < spawnsLimit; i++)
        {
          hideObsList[i].GetComponent<Renderer>().material = material[1];
        }

        if (spawnsLimit == limiteFase)
        {
            animator.SetBool("OpenWall", true);
        }
        else
        {
            animator.SetBool("OpenWall", false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CkpHold : MonoBehaviour {

    public static CkpHold instance;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }


    }

    void Start () {
		
	}
	
	
}

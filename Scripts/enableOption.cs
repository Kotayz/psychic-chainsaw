using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableOption : MonoBehaviour {

    public  int     idTheme;
    public  bool    isEnable;

    public  GameObject  option_1;
    public  GameObject  option_2;

    // Use this for initialization
    void Start () {
        if ((idTheme > 1) && (!isEnable))
        {
            option_2.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

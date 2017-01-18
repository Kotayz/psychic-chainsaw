using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeOptions : MonoBehaviour {

    public  GameObject[] arrayObjects;

    public  GameObject  opt_0;
    public  GameObject  opt_1;    
    public  GameObject  opt_2;

    public  GameObject  off_opt_0;
    public  GameObject  off_opt_1;
    public  GameObject  off_opt_2;

    private GameObject btnOn;
    private GameObject btnOff;

    public int[] arrayInt;

    private dbAccess database = new dbAccess();

    // Use this for initialization
    void Start () {

        for (int i = 1; i <= arrayObjects.Length; i++)
        {
            if (i == 1)
            {
                opt_0.SetActive(true);
                off_opt_0.SetActive(false);
            }
            else
            {                
                if (database.IsActivated("THEME_1_OPT_" + i))
                {                    
                    btnOn = GameObject.Find("btnTema_" + i);
                    btnOff = GameObject.Find("off_btnTema_" + i);
                    btnOn.SetActive(true);
                    btnOff.SetActive(false);
                }
                else
                {
                    btnOn = GameObject.Find("btnTema_" + i);
                    btnOff = GameObject.Find("off_btnTema_" + i);
                    btnOn.SetActive(false);
                    btnOff.SetActive(true);
                }
            }
        }        
    }

    private void DisableAll()
    {
        opt_0.SetActive(false);
        off_opt_0.SetActive(true);
        opt_1.SetActive(false);
        off_opt_1.SetActive(true);
    }
}

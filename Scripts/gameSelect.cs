using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameSelect : MonoBehaviour {

    public void Option(int option)
    {
        switch (option)
        {
            case 1:
                SceneManager.LoadScene("Main");                
                break;

            case 2:
                
                break;

            case 3:
                
                break;

            case 4:

                break;
        }
    }
}



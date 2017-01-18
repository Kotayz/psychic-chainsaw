using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoTheme : MonoBehaviour
{

    public  int          idTheme;

    public  GameObject      star_1;
    public  GameObject      star_2;
    public  GameObject      star_3;

    private int             notaFinal;

    private dbAccess database = new dbAccess();

    // Use this for initialization
    void Start()
    {
        string Name = "THEME_1_OPT_" + idTheme;

        star_1.SetActive(false);
        star_2.SetActive(false);
        star_3.SetActive(false);

        notaFinal = database.GetFinalNote(Name);

        if (notaFinal == 10)
        {
            star_1.SetActive(true);
            star_2.SetActive(true);
            star_3.SetActive(true);
        }
        else if (notaFinal >= 7)
        {
            star_1.SetActive(true);
            star_2.SetActive(true);
            star_3.SetActive(false);
        }
        else if (notaFinal >= 5)
        {
            star_1.SetActive(true);
            star_2.SetActive(false);
            star_3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

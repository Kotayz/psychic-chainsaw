using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finalNote : MonoBehaviour {

    private int idTheme;

    public Text txtNote;
    public Text txtThemeInfo;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private int note;
    private int correctAnswers;
    // Use this for initialization
    void Start () {

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        idTheme = PlayerPrefs.GetInt("idTema");

        note = PlayerPrefs.GetInt("temp_FinalNote" + idTheme.ToString());
        correctAnswers = PlayerPrefs.GetInt("temp_CorrectAnswers" + idTheme.ToString());

        txtNote.text = note.ToString();
        txtThemeInfo.text = "Você acertou " + correctAnswers.ToString() + " de 20 pergunstas";

        if (note == 10)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (note >= 7)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        else if (note >= 5)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
    }
	
    public void playAgain()
    {
        //Application.LoadLevel("Theme_" + idTheme.ToString());
        SceneManager.LoadScene("Theme_" + idTheme.ToString());
    }
    // Update is called once per frame
    void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameTheme : MonoBehaviour {

    public Button       btnPlay;

    public Text         txtNomeTema;    
    public Text         txtInfoTema;

    public GameObject   infoTema;
    public GameObject   estrela1;
    public GameObject   estrela2;
    public GameObject   estrela3;

    public string[]     nomeTema;
    public int          numeroQuestoes;
    public int          enableTheme;

    private int         idTema;

    private dbAccess database = new dbAccess();

    // Use this for initialization
    void Start () {
        idTema = 0;
        txtNomeTema.text = nomeTema[idTema];
        txtInfoTema.text = "Você acertou X de U Questões";
        infoTema.SetActive(false);
        estrela1.SetActive(false);
        estrela2.SetActive(false);
        estrela3.SetActive(false);
        btnPlay.interactable = false;
    }
	
	public void selecioneTema(int i)
    {
        Debug.Log("Tema "+i);
        idTema = i;
        PlayerPrefs.SetInt("idTema", idTema);
        txtNomeTema.text = nomeTema[idTema];
        string Name = "THEME_1_OPT_" + i;
       
        int notaFinal = database.GetFinalNote(Name);
        int acertos = database.GetCorrectAnswers(Name);

        estrela1.SetActive(false);
        estrela2.SetActive(false);
        estrela3.SetActive(false);

        if (notaFinal == 10)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(true);
            estrela3.SetActive(true);
        }
        else if (notaFinal >= 7)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(true);
            estrela3.SetActive(false);
        }
        else if (notaFinal >= 5)
        {
            estrela1.SetActive(true);
            estrela2.SetActive(false);
            estrela3.SetActive(false);
        }

        txtInfoTema.text = "Você acertou " + acertos.ToString() + " de " + numeroQuestoes.ToString() + " Questões";
        infoTema.SetActive(true);
        btnPlay.interactable = true;
    }

    public void play()
    {        
        SceneManager.LoadScene("Theme_" + idTema.ToString());
    }
}

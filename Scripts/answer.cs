using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class answer : MonoBehaviour {

    private int idTema;

    public Text question;
    public SpriteRenderer myImage;
    public Text answerA;
    public Text answerB;
    public Text answerC;
    public Text answerD;
    public Text infoAnswers;

    public string[] questions;
    public Sprite[] mySprites;
    public string[] alternativeA;
    public string[] alternativeB;
    public string[] alternativeC;
    public string[] alternativeD;
    public string[] corrects;

    private int idQuestion;

    private float correctAnswers;
    public float amountQuestions;
    private float average;
    private int finalNote;
    private Cooldown cooldown;
    private dbAccess database = new dbAccess();

    // Use this for initialization
    void Start () {
        cooldown = (Cooldown)FindObjectOfType(typeof(Cooldown));
        
        idTema = PlayerPrefs.GetInt("idTema");
        idQuestion = 0;
        amountQuestions = questions.Length;        
        question.text = questions[idQuestion];
        myImage.sprite = mySprites[idQuestion];
        answerA.text = alternativeA[idQuestion];
        answerB.text = alternativeB[idQuestion];
        answerC.text = alternativeC[idQuestion];
        answerD.text = alternativeD[idQuestion];

        infoAnswers.text = "Respondendo "+(idTema + 1).ToString()+" de "+amountQuestions.ToString()+" perguntas" ;
    }
	
    public void response(string alternative)
    {
        switch (alternative)
        {
            case "A":
                if (alternativeA[idQuestion] == corrects[idQuestion])
                {
                    correctAnswers++;
                }
                break;

            case "B":
                if (alternativeB[idQuestion] == corrects[idQuestion])
                {
                    correctAnswers++;
                }
                break;

            case "C":
                if (alternativeC[idQuestion] == corrects[idQuestion])
                {
                    correctAnswers++;
                }
                break;

            case "D":
                if (alternativeD[idQuestion] == corrects[idQuestion])
                {
                    correctAnswers++;
                }
                break;
        }

        nextQuestion();
    }

    public void nextQuestion ()
    {
        idQuestion++;
        cooldown.cooldown.fillAmount = 1;
        
        if (idQuestion <= (amountQuestions - 1))
        {
            question.text = questions[idQuestion];
            answerA.text = alternativeA[idQuestion];
            answerB.text = alternativeB[idQuestion];
            answerC.text = alternativeC[idQuestion];
            answerD.text = alternativeD[idQuestion];
            myImage.sprite = mySprites[idQuestion];

            infoAnswers.text = "Respondendo " + (idQuestion + 1).ToString() + " de " + amountQuestions.ToString() + " perguntas";
        }
        else
        {
            int i = idTema;
            string Name = "THEME_1_OPT_" + i;

            average = 10 * (correctAnswers / amountQuestions);
            finalNote = Mathf.RoundToInt(average);

            if (finalNote > database.GetFinalNote(Name))
            {
                database.UpdateScore(Name, finalNote, (int)correctAnswers);
                PlayerPrefs.SetInt("finalNote" + idTema.ToString(), finalNote);
                PlayerPrefs.SetInt("correctAnswers" + idTema.ToString(), (int)correctAnswers);
            }

            i++;
            string nextName = "THEME_1_OPT_" + i;

            if ((finalNote > 5) && (!database.IsActivated(nextName)))
            {
                database.Activatenext(nextName);
            }

            PlayerPrefs.SetInt("temp_FinalNote" + idTema.ToString(), finalNote);
            PlayerPrefs.SetInt("temp_CorrectAnswers" + idTema.ToString(), (int)correctAnswers);
                        
            SceneManager.LoadScene("FinalNote");
        }
    }    
}

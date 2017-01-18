using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class Cooldown : MonoBehaviour
{

    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;

    private answer nextQuestion;

    void Start ()
    {
        nextQuestion = (answer) FindObjectOfType(typeof(answer));
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {         
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            Debug.Log(cooldown.fillAmount);
            if (cooldown.fillAmount == 0)
            {                
                nextQuestion.nextQuestion();
                cooldown.fillAmount = 1;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;

    public float timerValue;

    Quiz quiz;

    private void Start()
    {
        
        quiz = GetComponent<Quiz>();

    }

    void Update()
    {
        
        /*
        if(timerValue == timeToCompleteQuestion)
        {

            quiz.ProgressBarReturns();

        }
        */
        
        

        UpdateTimer();

    }

    public void CancelTimer()
    {

        timerValue = 0;

    }

    void UpdateTimer()
    {

        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {

            if(timerValue > 0)
            {

                fillFraction = timerValue / timeToCompleteQuestion; 
            }
            else
            {

                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;

            }

        }
        else
        {

            if(timerValue > 0)
            {

                fillFraction = timerValue / timeToShowCorrectAnswer;
                

            }
            else
            {

                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;

            }

        }

    }

}
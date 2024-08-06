using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int correctAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers()
    {

        return correctAnswers;

    }

    public void IncrementCorrectAnswers()
    {
        
        correctAnswers++;

    }

    public int GetQuestionsSeen()
    {

        return questionsSeen;

    }

    public void IncrementQuestionsSeen()
    {

        questionsSeen++;

    }

    
    public void SetScore(int score)
    {

        correctAnswers = score / 10; // Assuming each correct answer adds 10 to the score
    
    }

    public void SetQuestionsSeen(int seen)
    {

        questionsSeen = seen;
    
    }
    

    public int CalculateScore()
    {

        int totalQuestions = 10; 

        int score = GetCorrectAnswers() * 10;
        float percentage = (float)score / (totalQuestions * 10) * 100;

        return Mathf.RoundToInt(percentage);
    
    }

}
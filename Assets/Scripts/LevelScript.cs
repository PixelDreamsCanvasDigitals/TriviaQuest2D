using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{

    public Quiz quiz;

    private void Start()
    {
        
        quiz = FindObjectOfType<Quiz>();

    }
    private void Update()
    {
        Pass();
    }

    public void Pass()
    {

        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {

            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);

            /*
            if (PlayerPrefs.GetInt("Score" + currentLevel) >= 60)
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            }
            else
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
            }
            */
        }

        //Debug.Log("LEVEL" + PlayerPrefs.GetInt("levelsUnlocked") + "UNLOCKED");

    }
    
}
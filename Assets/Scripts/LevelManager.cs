using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    Quiz quiz;

    public Button[] levelButtons;

    int levelsUnlocked;

    void Start()
    {

        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 1; i < levelButtons.Length; i++)
        {

            levelButtons[i].interactable = false;

        }

        for (int i = 0; i < levelsUnlocked; i++)
        {

            levelButtons[i].interactable = true;

        }

    }

    public void LoadLevel(int levelIndex)
    {

        SceneManager.LoadScene(levelIndex);
        //PlayerPrefs.DeleteKey("levelsUnlocked");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("ProgressBarValue");
        PlayerPrefs.DeleteKey("QuestionsSeen");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    EndScreen endScreen;

    public GameObject Canvas;

    [SerializeField] GameObject answerButtons;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI questionText;
    
    void Awake()
    {

        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //quiz.isComplete = false;

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        Canvas.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(quiz.isComplete)
        {

            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
            //quiz.isComplete = false;
            answerButtons.SetActive(false);
            slider.gameObject.SetActive(false);
            questionText.gameObject.SetActive(false);
        }

    }

    public void OnReplayLevel()
    {

        quiz.isComplete = false;
        //PlayerPrefs.DeleteKey("Score");
        //PlayerPrefs.DeleteKey("ProgressBarValue");
        //PlayerPrefs.DeleteKey("QuestionsSeen");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}

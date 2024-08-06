using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Reflection;

public class Quiz : MonoBehaviour
{

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    int wrongAnswerIndex;
    [SerializeField] GameObject AnswerButton;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    public EndScreen endScreen;

    private int levelsUnlocked;

    public Button[] levelButtons;

    private LevelScript levelScript;

    public int Score { get { return scoreKeeper.CalculateScore(); } }

    public Button pauseButton;

    public GameObject pausePanel;

    public GameObject exitPanel;

    [SerializeField] LinkScriptable linkSO;

    public int questionIndex;

    private void Start()
    {

        Time.timeScale = 1;
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

        levelScript = FindObjectOfType<LevelScript>();

        LoadGameProgress();

        questions.Shuffle();
        questionIndex = 0;

    }
    

    void Update()
    {

        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {

            if (progressBar.value == progressBar.maxValue)
            {

                isComplete = true;
                return;

            }

            GetNextQuestion();
            timer.loadNextQuestion = false;

        }
        else if (!timer.isAnsweringQuestion)
        {

            if (!hasAnsweredEarly && timer.timerValue == 0)
            {

                progressBar.value++;
                DisplayAnswer(-1);
            
            }

        }

        
        if(progressBar.value == 10)
        {

            endScreen.ShowFinalScore();
            timer.CancelTimer();
            questionText.gameObject.SetActive(false);
            timerImage.enabled = false;

            for(int i = 0; i < answerButtons.Length; i++)
            {

                answerButtons[i].SetActive(false);

            }

        }

        linkSO.mainMenuProgressValue = progressBar.value;
    }

    public void OnAnswerSelected(int index)
    {

        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue)
        {

            isComplete = true;

        }

        if(timer.isAnsweringQuestion && timer.timerValue == 30)
        {

            progressBar.value++;

        }

    }

    void DisplayAnswer(int index)
    {

        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {

            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();

        }
        else
        {

            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n " + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        }
        progressBar.value++;
    }

    void GetNextQuestion()
    {

        if (questionIndex < questions.Count)
        {

            SetButtonState(true);
            SetDefaultButtonSprites();
            currentQuestion = questions[questionIndex];
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
            questionIndex++;

        }

    }

    void GetRandomQuestion()
    {
        
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        
        if (questions.Contains(currentQuestion))
        {

            questions.Remove(currentQuestion);

        }

    }

    void DisplayQuestion()
    {

        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {

            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);

        }

    }

    void SetButtonState(bool state)
    {

        for (int i = 0; i < answerButtons.Length; i++)
        {

            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;

        }

    }

    void SetDefaultButtonSprites()
    {

        for (int i = 0; i < answerButtons.Length; i++)
        {

            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;

        }

    }

    public void ProgressBarReturns()
    {

        progressBar.value++;

    }

    public void SceneChange(int index)
    {

        SaveGameProgress(); // new line added
        SceneManager.LoadScene(index);

    }

    public void SceneChangeforMathsScene(int index)
    {

        SceneManager.LoadScene(index);

    }

    public void PauseButton()
    {

        questionText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(false);
        AnswerButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;

    }

    public void ResumeButton()
    {

        questionText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        timerImage.gameObject.SetActive(true);
        AnswerButton.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void ExitButton()
    {

        pausePanel.SetActive(false);
        exitPanel.SetActive(true);
        Time.timeScale = 0.0f;

    }

    public void ExitNoButton()
    {

        exitPanel.SetActive(false);
        questionText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        timerImage.gameObject.SetActive(true);
        AnswerButton.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void ExitTheGame()
    {

        SaveGameProgress();
        Application.Quit();

    }

    void SaveGameProgress()
    {

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Score" + currentLevel, scoreKeeper.CalculateScore());
        PlayerPrefs.SetInt("QuestionsSeen" + currentLevel, scoreKeeper.GetQuestionsSeen());
        PlayerPrefs.SetInt("ProgressBarValue" + currentLevel, (int)progressBar.value);
        PlayerPrefs.Save();

    }

    void LoadGameProgress()
    {

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.HasKey("Score" + currentLevel))
        {
   
            int savedScore = PlayerPrefs.GetInt("Score" + currentLevel);
            int questionsSeen = PlayerPrefs.GetInt("QuestionsSeen" + currentLevel);
            int progressBarValue = PlayerPrefs.GetInt("ProgressBarValue" + currentLevel);

            scoreKeeper.SetScore(savedScore);
            scoreKeeper.SetQuestionsSeen(questionsSeen);
            progressBar.value = progressBarValue;

        }

    }

}
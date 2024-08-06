using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public GameObject[] arrows;

    public AudioSource sounds;

    public AudioClip[] playSounds;

    public AudioClip welcomeSound;

    private int num = 0;

    public Button nextButton;

    public Button mainMenu;

    public Button playButton;

    public GameObject fullStuff;

    public Button pauseButton;

    public GameObject pausePanel;

    void Start()
    {

        sounds = GetComponent<AudioSource>();
        //sounds.PlayOneShot(playSounds[0],1.0f);
        sounds.PlayOneShot(welcomeSound, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if(num == 6)
        {
            nextButton.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            sounds.PlayOneShot(playSounds[7], 1.0f);
        }
    }

    public void NextButton()
    {
        if(num < arrows.Length - 1)
        {

            arrows[num].SetActive(false);
            num++;
            sounds.Stop();
            arrows[num].SetActive(true);
            sounds.PlayOneShot(playSounds[num],1.0f);

        }
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayButton()
    {

        sounds.Stop();
        playButton.gameObject.SetActive(false);
        fullStuff.SetActive(true);
        sounds.PlayOneShot(playSounds[num], 1.0f);
        arrows[num].SetActive(true);
        
    }

    public void PauseButton()
    {
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        sounds.Pause();
    }

    public void ResumeButton()
    {
        sounds.Play();
        pauseButton.gameObject.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    public GameObject playButton;
    public GameObject exitButton;
    public GameObject soundButton;
    public GameObject soundPanel;
    public GameObject deleteDataButton;
    public GameObject menuPlate;
    public GameObject goBackButton;
    public GameObject selectionPanel;
    public GameObject tutorialButton;
    public GameObject exitPanel;
    public GameObject spinButton;
    public GameObject shopButton;

    public void PlayButton()
    {

        playButton.SetActive(false);
        exitButton.SetActive(false);
        deleteDataButton.SetActive(false);
        goBackButton.SetActive(true);
        selectionPanel.SetActive(true);
        soundButton.SetActive(false);
        tutorialButton.SetActive(false);
        spinButton.SetActive(false);
        shopButton.SetActive(false);
        menuPlate.SetActive(false);

    }

    public void GoBackButton()
    {

        deleteDataButton.SetActive(true);
        goBackButton.SetActive(false);
        playButton.SetActive(true);
        exitButton.SetActive(true);
        selectionPanel.SetActive(false);
        soundButton.SetActive(true);
        tutorialButton.SetActive(true);
        spinButton.SetActive(true);
        shopButton.SetActive(true);
        menuPlate.SetActive(true);

    }

    public void ExitButton()
    {

        playButton.SetActive(false);
        soundButton.SetActive(false);
        tutorialButton.SetActive(false);
        exitButton.SetActive(false);
        deleteDataButton.SetActive(false);
        exitPanel.SetActive(true);

    }

    public void ExitPanelNoButton()
    {

        playButton.SetActive(true);
        soundButton.SetActive(true);
        tutorialButton.SetActive(true);
        exitButton.SetActive(true);
        deleteDataButton.SetActive(true);
        exitPanel.SetActive(false);

    }

    public void ExitPanelYesButton()
    {
        Application.Quit();
    }

    public void ChangeThesScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    public void DeleteAll()
    {

        PlayerPrefs.DeleteAll();

    }

    public void SoundButton()
    {
        playButton.SetActive(false);
        soundButton.SetActive(false);
        exitButton.SetActive(false);
        deleteDataButton.SetActive(false);
        soundPanel.SetActive(true);
        tutorialButton.SetActive(false);
        spinButton.SetActive(false);
        shopButton.SetActive(false);
    }

    public void SoundButtonGoBack()
    {
        soundPanel.SetActive(false);
        playButton.SetActive(true);
        soundButton.SetActive(true);
        exitButton.SetActive(true);
        tutorialButton.SetActive(true);
        deleteDataButton.SetActive(true);
        spinButton.SetActive(true);
        shopButton.SetActive(true);
    }

}
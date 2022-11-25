using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPage, startPage, newGamePage, settingPage;

    private void Start()
    {
        SetActivePage(true, false, false, false);
    }

    private void SetActivePage(bool main, bool start, bool newGame, bool setting)
    {
        mainPage.SetActive(main);
        startPage.SetActive(start);
        newGamePage.SetActive(newGame);
        settingPage.SetActive(setting);
    }

    public void MainPage()
    {
        SetActivePage(true, false, false, false);
    }
    public void StartPage()
    {
        SetActivePage(false, true, false, false);
    }

    public void NewGamePage()
    {
        SetActivePage(false, false, true, false);
    }

    public void SettingPage()
    {
        SetActivePage(false, false, false, true);
    }
    public void PlayNewGame() {
        SceneManager.LoadScene("GameScene");
    }
    public void Exit() {
        Application.Quit();
    }

}

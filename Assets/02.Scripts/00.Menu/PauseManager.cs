using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;

    private void Awake()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    
    public void PauseGame()
    {
        print("Pause Game");
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        print("Continue Game");
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ShowSettings(bool activeness)
    {
        settingsPanel.SetActive(activeness);
    }

    public void GoToMain()
    {
        Time.timeScale = 1;
        RoutineManager.MoveScene(0);
    }

    public void QuitGame()
    {
        RoutineManager.QuitGame();
    }
}
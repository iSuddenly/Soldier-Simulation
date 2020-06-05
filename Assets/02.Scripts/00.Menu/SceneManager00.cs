using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager00 : MonoBehaviour
{
    public GameObject startPanel;

    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject helpPanel;
    public GameObject sonamuPanel;

    public GameObject managerFactory;

    private void Awake()
    {
        if (GameObject.Find("Manager") == null)
        {
            var manager = Instantiate(managerFactory);
            manager.name = "Manager";
        }
    }

    void Start()
    {
        startPanel.SetActive(true);
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        helpPanel.SetActive(false);
        sonamuPanel.SetActive(false);
    }


    public void ToggleSonamu(bool toMain)
    {
        //mainPanel.SetActive(toMain);
        sonamuPanel.SetActive(!toMain);
        if (!toMain) SoundManager.Instance.PlaySound("PageFlip");

    }

    public void ToggleHelp(bool toMain)
    {
        //mainPanel.SetActive(toMain);
        helpPanel.SetActive(!toMain);
    }

    public void ToggleSettings(bool toMain)
    {
        //mainPanel.SetActive(toMain);
        settingsPanel.SetActive(!toMain);
    }

    public void GameStart()
    {
        RoutineManager.MoveScene();
    }

    public void GameExit()
    {
        RoutineManager.QuitGame();
    }
}
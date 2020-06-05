using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneManager07 : Manager
{
    public static SceneManager07 Instance;

    public GameObject[] questGivers;

    public Image bgImage;
    public Image objectImage;

    public Sprite to;
    public Sprite original;

    public GameObject dialogueBubble;

    public GameObject questTestButton;

    public GameObject chocoPie;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        //StartCoroutine("Flow");
        objectImage.gameObject.SetActive(false);
        //questTestButton.SetActive(true);
        if(CalendarManager.playedDays == 1)
        {
            chocoPie.SetActive(true);
        }
        else
        {
            chocoPie.SetActive(false);
        }
    }
    
    void ChangeBackground(bool toOriginal/*, int questNo*/)
    {
        if (toOriginal)
        {
            bgImage.sprite = original;
            for(int i=0; i<questGivers.Length; i++)
            {
                questGivers[i].SetActive(true);
            }
            //기본 내무반 배경으로 변경
        }
        else
        {
            bgImage.sprite = to;
            for (int i = 0; i < questGivers.Length; i++)
            {
                questGivers[i].SetActive(false);
            }
            //Resources.Load
        }
    }

    public IEnumerator Flow()
    {
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);

        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }
        yield return new WaitUntil(() => Input.anyKeyDown);
        yield return new WaitWhile(() => DialogueFrame.Instance.isDialogueOnGoing);
        questTestButton.GetComponent<Button>().interactable = true;
    }

}

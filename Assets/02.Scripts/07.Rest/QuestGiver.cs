using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : IgnoreTransparent
{
    //사람 자체를 프리팹으로 만들어서 번호와 함께 
    public string questKey;
    public SceneManager07 mng;

    public bool isHasQuest;

    private void Start()
    {
        if (isHasQuest) transform.GetChild(0).gameObject.SetActive(true);
        else transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        //딴거 진행중이 아니라면
        if (!DialogueFrame.Instance.isDialogueOnGoing)
        {
            DialogueFrame.Instance.AddDialogue(DialogueManager.questDict[questKey]);
            StartCoroutine(mng.Flow());
        }

    }

}

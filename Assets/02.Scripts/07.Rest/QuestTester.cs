using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTester : MonoBehaviour
{
    public SceneManager07 mng;
    public GameObject testPanel;
    public GameObject[] boxes = new GameObject[4];

    void Start()
    {
        GetComponent<Button>().interactable = true;
        //StartCoroutine(AddListeners());        
    }
    

    IEnumerator AddListeners()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            var closureBoxIndex = i;
            var questButtons = boxes[i].GetComponentsInChildren<Button>();
            for (int j = 0; j < questButtons.Length; j++)
            {
                questButtons[j].gameObject.GetComponentInChildren<Text>().text = ((j + 1) * 10).ToString();
                int closureIndex = j;
                questButtons[closureIndex].onClick.AddListener(() => {
                    string questName = (boxes[closureBoxIndex].gameObject.name + ((closureIndex + 1) * 10).ToString() + "_1").ToString();
                    print("Execute" + questName);
                    DialogueFrame.Instance.AddDialogue(DialogueManager.questDict[questName]);
                    testPanel.SetActive(false);
                    StartCoroutine(mng.Flow());
                    //DialogueFrame.Instance.ExecuteDialogue();
                    questButtons[closureIndex].interactable = false;
                    GetComponent<Button>().interactable = false;
                });
            }
        }
        yield return null;
        
    }

   
}

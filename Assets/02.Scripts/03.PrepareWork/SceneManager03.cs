using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;
/// <summary>
/// Chatting before Main Game
/// </summary>
public class SceneManager03 : Manager
{
    //이때 픽토그램들 줄줄이 들어오게 하기
    public static SceneManager03 Instance;

    private TextAsset ta;
    public List<string[]> bubbleTexts = new List<string[]>();
    public GameObject[] speechBubbles;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        ta = Resources.Load<TextAsset>("TextFiles/SmallTalks");
        ReadSmallTalks();
        SetBubbleWords();
        Useful.SetActiveness(false, speechBubbles);
    }

    void Start()
    {
        StartCoroutine("Flow");
    }
    
    IEnumerator Flow()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < speechBubbles.Length; i++)
        {
            speechBubbles[i].SetActive(true);
            yield return new WaitWhile(() => !speechBubbles[i].GetComponent<SpeechBubble>().isBubbleDone);
        }
        yield return new WaitUntil(() => Input.anyKeyDown);
        RoutineManager.MoveScene();
    }

    private void SetBubbleWords()
    {
        var chosenLine = Useful.RandomElement(bubbleTexts);
        for (int i = 0; i < speechBubbles.Length; i++)
        {
            speechBubbles[i].GetComponentInChildren<SpeechBubble>().targetString = chosenLine[i];
        }
    }

    private void ReadSmallTalks()
    {
        var rows = ta.text.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            var columns = rows[i].Split(',');
            bubbleTexts.Add(columns);
        }
    }
}

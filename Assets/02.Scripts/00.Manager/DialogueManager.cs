using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using System.Linq;
/// <summary>
/// Parse Dialogue CSVs
/// </summary>
public static class DialogueManager
{
    private static readonly string textFilePath = "TextFiles/";

    private static TextAsset scene2Text;
    private static TextAsset scene5GoodText, scene5SosoText, scene5BadText;
    private static TextAsset scene6UpText, scene6MidText, scene6DownText, scene6TinkerbellText, scene6WildBoarText, scene6Caught1Text, scene6Caught2Text, scene6CaughtNotText;
    private static TextAsset scene8GoodText, scene8SosoText, scene8BadText;

    private static TextAsset questText;

    public static List<Dialogue> scene2Monologues = new List<Dialogue>(); //Scene2 급양관 대사
    public static List<Dialogue> scene5GoodMonologues = new List<Dialogue>(); //Scene5 배식잘함
    public static List<Dialogue> scene5SosoMonologues = new List<Dialogue>(); //Scene5 배식그냥저냥
    public static List<Dialogue> scene5BadMonologues = new List<Dialogue>(); //Scene5 배식못함
    public static List<Dialogue> scene6UpMonologues = new List<Dialogue>(); //Scene6 오르막
    public static List<Dialogue> scene6MidMonologues = new List<Dialogue>(); //Scene6 산중턱
    public static List<Dialogue> scene6DownMonologues = new List<Dialogue>(); //Scene6 내리막
    public static List<Dialogue> scene6TinkerbellMonologues = new List<Dialogue>(); //Scene6 팅커벨
    public static List<Dialogue> scene6WildBoarMonologues = new List<Dialogue>(); //Scene6 멧돼지 만남
    public static List<Dialogue> scene6Caught1Monologues = new List<Dialogue>(); //Scene6 급양관 걸림1
    public static List<Dialogue> scene6Caught2Monologues = new List<Dialogue>(); //Scene6 급양관 걸림2
    public static List<Dialogue> scene6CaughtNotMonologues = new List<Dialogue>(); //Scene6 안걸림
    public static List<Dialogue> scene8GoodMonologues = new List<Dialogue>(); //Scene8 소나무 배식잘함
    public static List<Dialogue> scene8SosoMonologues = new List<Dialogue>(); //Scene8 소나무 배식그냥저냥
    public static List<Dialogue> scene8BadMonologues = new List<Dialogue>(); //Scene8 소나무 배식못함

    public static void LoadTexts()
    {
        scene2Text = Resources.Load<TextAsset>(textFilePath + "Scene2");
        scene5GoodText = Resources.Load<TextAsset>(textFilePath + "Scene5_Good");
        scene5SosoText = Resources.Load<TextAsset>(textFilePath + "Scene5_Soso");
        scene5BadText = Resources.Load<TextAsset>(textFilePath + "Scene5_Bad");
        scene6UpText = Resources.Load<TextAsset>(textFilePath + "Scene6_Up");
        scene6MidText = Resources.Load<TextAsset>(textFilePath + "Scene6_Mid");
        scene6DownText = Resources.Load<TextAsset>(textFilePath + "Scene6_Down");
        scene6TinkerbellText = Resources.Load<TextAsset>(textFilePath + "Scene6_Tinkerbell");
        scene6WildBoarText = Resources.Load<TextAsset>(textFilePath + "Scene6_WildBoar");
        scene6Caught1Text = Resources.Load<TextAsset>(textFilePath + "Scene6_Caught1");
        scene6Caught2Text = Resources.Load<TextAsset>(textFilePath + "Scene6_Caught2");
        scene6CaughtNotText = Resources.Load<TextAsset>(textFilePath + "Scene6_CaughtNot");
        scene8GoodText = Resources.Load<TextAsset>(textFilePath + "Scene8_Good");
        scene8SosoText = Resources.Load<TextAsset>(textFilePath + "Scene8_Soso");
        scene8BadText = Resources.Load<TextAsset>(textFilePath + "Scene8_Bad");

        questText = Resources.Load<TextAsset>(textFilePath + "Quest");
    }

    public static void Read()
    {
        //추후 얘네 다 딕셔너리로 수정...
        ReadMonologues(scene5GoodText, scene5GoodMonologues);
        ReadMonologues(scene5SosoText, scene5SosoMonologues);
        ReadMonologues(scene5BadText, scene5BadMonologues);
        ReadMonologues(scene6UpText, scene6UpMonologues);
        ReadMonologues(scene6MidText, scene6MidMonologues);
        ReadMonologues(scene6DownText, scene6DownMonologues);
        ReadMonologues(scene6TinkerbellText, scene6TinkerbellMonologues);
        ReadMonologues(scene6WildBoarText, scene6WildBoarMonologues);
        ReadMonologues(scene6Caught1Text, scene6Caught1Monologues);
        ReadMonologues(scene6Caught2Text, scene6Caught2Monologues);
        ReadMonologues(scene6CaughtNotText, scene6CaughtNotMonologues);
        ReadMonologues(scene8GoodText, scene8GoodMonologues);
        ReadMonologues(scene8SosoText, scene8SosoMonologues);
        ReadMonologues(scene8BadText, scene8BadMonologues);
    }


    public static void ReadD()
    {
        //yield return new WaitWhile(() => SoldierManager.soldierDict.ContainsKey("급양관"));
        Debug.Log("Start parsing");
        ReadMonologues(scene2Text, scene2Monologues, SoldierManager.soldierDict["급양관"]);

        ReadDialogues(questText, questDict);
        //yield return null;
    }

    /// <summary>
    /// CSV to Monologue
    /// CSV : Dial+ogue, IsFinished
    /// </summary>
    /// <param name="textAsset">CSV File</param>
    /// <param name="dialogues">Target Dialogue List</param>
    /// <param name="speaker">if null : player monologue</param>
    private static void ReadMonologues(TextAsset textAsset, List<Dialogue> dialogues, Soldier speaker = null)
    {
        var rows = textAsset.text.Split(new char[] { '\n' });
        var firstRow = rows[0].Split(new char[] { ',' });
        
        for (int i = 1; i < rows.Length; i++) //Ignore first & last line
        {
            var columns = rows[i].Split(new char[] { ',' }); //Dialogue, IsFinished
            var speeches = columns[0].Split(new char[] { '+' }); //SpeechLines

            var speechLines = new Queue<SpeechLine>();

            foreach (var speech in speeches)
            {
                speechLines.Enqueue(new SpeechLine
                {
                    Speaker = speaker,
                    Speech = speech,
                });
            }

            dialogues.Add(new Dialogue {
                speechLines = speechLines,
            });
        }
        
    }

    private static string[] keywords = new string[] {"Fork", "BG", "Show", "Text", "Aff", "Health", "Execute"};
    
    public static Dictionary<string, Dialogue> questDict; //Quest Dialogues

    private static void ReadDialogues(TextAsset textAsset, Dictionary<string, Dialogue> dict)
    {
        var rows = textAsset.text.Split(new char[] { '\n' });
        var firstRow = rows[0].Split(new char[] { ',' });

        string currentQuest = "";
        string currentKey = ""; //이렇게 빼는거 별로인거같다.
        Soldier tempSoldier = null;
        
        for (int i = 1; i < rows.Length; i++) //Ignore first & last line
        {
            //print("Line " + i +" " + rows[i]);
            var columns = rows[i].Split(new char[] { ',' }); //Key, No, Speaker, Dialogue
            if (Useful.IsEmpty(columns)) continue; //If all columns in single line is empty, continue

            if (!string.IsNullOrEmpty(columns[0])) currentQuest = columns[0];  //If current line begins new Quest
            if (!string.IsNullOrEmpty(columns[1])) //If current line begins new Dialogue
            {
                if (!dict.ContainsKey(currentQuest + "_" + columns[1])) //Check if key already exists in Dictionary
                {
                    currentKey = currentQuest + "_" + columns[1]; //ex)"급양관10_2"
                    dict.Add(currentKey, new Dialogue()); //Add new pair to dictionary
                    //print("Second Column isnt null, new KeyPair created : " + currentKey);
                }
            }

            var speeches = columns[3].Split(new char[] { '+' }); //Array of SpeechLines.Speech

            if (keywords.Contains(speeches[0])) //If Line starts with Keyword, don't split
            {
                dict[currentKey].speechLines.Enqueue(new SpeechLine
                {
                    Speaker = null,
                    Speech = columns[3],
                });
            }
            else //If Line doesn't start with Keyword, add queue to Dialogue
            {
                foreach (var speech in speeches)
                {
                    Soldier s;
                    if (SoldierManager.soldierDict.ContainsKey(columns[2]))
                    {
                        s = SoldierManager.soldierDict[columns[2]];
                        tempSoldier = s;
                    }
                    //else if (columns[2] == "플레이어") s = null;
                    else if (columns[2] == "") s = tempSoldier;
                    else s = SoldierManager.moreSoldierDict[columns[2]];

                    dict[currentKey].speechLines.Enqueue(new SpeechLine
                    {
                        //Speaker = SoldierManager.soldierDict.ContainsKey(columns[2]) ? SoldierManager.soldierDict[columns[2]] : SoldierManager.moreSoldierDict[columns[2]],
                        Speaker = s,
                        Speech = speech,
                    });
                }

            }
        }
    }
    
    public static Dialogue GetRandomDialogue(List<Dialogue> dialogueList)
    {
        var a = Random.Range(0, dialogueList.Count - 1);
        Dialogue d = dialogueList[a];
        dialogueList.RemoveAt(a);
        //print(d + " " + d.speechLines.Count);
        return (d);
        //return dialogueList[Random.Range(0, dialogueList.Count-1)];
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;
using System.Linq;
/// <summary>
/// Getting rid of Food Waste
/// </summary>
public class SceneManager06 : Manager
{
    public static SceneManager06 Instance;

    private TextAsset tinkerbellText;
    public static List<Dialogue> tinkerbellDialogues = new List<Dialogue>(); //Scene6 팅커벨

    public Image bg;
    public Sprite uphillFactory, mountainFactory, downhillFactory;
    public Sprite wildBoarFactory;

    public GameObject shadow;
    public GameObject catchingMan; //급양관
    public GameObject fairy; //요정
    public GameObject fairyEffect;
    public GameObject[] boxes = new GameObject[3]; //선물상자들
    public GameObject closedBox;
    public bool boxClicked;
    public Sprite[] presents = new Sprite[6];
    public Sprite[] rabbits = new Sprite[3];

    public GameObject foodWaste;

    private bool isBoarAppear;
    private bool isFairyAppear;
    private bool isGetCaught;

    private float boarLikelihood = 2f; //Likelihood of Wildboar Appearance
    private float fairyLikelihood = 2f; //Likelihood of Fairy Appearance
    private float caughtLikelihood = 2f; //Likelihood to be caught

    public GameObject fuckingImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        tinkerbellText = Resources.Load<TextAsset>("TextFiles/Tinkerbell");
    }

    void Start()
    {
        SetDestiny();
        StartCoroutine(Flow());
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].SetActive(false);
        }
        closedBox.SetActive(false);
    }

    private void SetDestiny()
    {
        if(CalendarManager.playedDays == 1)
        {
            isFairyAppear = true;
            isBoarAppear = false;
            isGetCaught = false;
        }
        else
        {
            isGetCaught = true;
            isBoarAppear = true;
            isFairyAppear = false;
        }

        //if(UnityEngine.Random.Range(0, 10) <= caughtLikelihood) isGetCaught = true;
        //var a = UnityEngine.Random.Range(0, 10);
        //if (a <= 1) isFairyAppear = true;
        //else if(1 < a && a <= 2) isBoarAppear = true;
        
        if(isFairyAppear) print("FAIRY");
    }

    IEnumerator Flow()
    {
        yield return new WaitWhile(() => !SoldierManager.moreSoldierDict.ContainsKey("팅커벨"));
        TinkerbellDialogue();
        SoundManager.Instance.PlaySound("FootStep");
        yield return new WaitWhile(() => !DialogueManager.scene6UpMonologues.Any());
        DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6UpMonologues));
        yield return new WaitUntil(() => Input.anyKeyDown);
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);

        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }
        bg.sprite = mountainFactory;

        DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6MidMonologues));
        yield return new WaitUntil(() => Input.anyKeyDown);
        foodWaste.SetActive(true);
        SoundManager.Instance.PlaySound("Splash");
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);
        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }

        if (isBoarAppear)
        {
            bg.sprite = wildBoarFactory;
            SoundManager.Instance.PlaySound("WildBoar");
            DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6WildBoarMonologues));
            yield return new WaitUntil(() => Input.anyKeyDown);
            DialogueFrame.Instance.ExecuteDialogue();
            yield return new WaitUntil(() => Input.anyKeyDown);
            while (DialogueFrame.dialogues.Count != 0)
            {
                if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
                yield return null;
            }
            bg.sprite = mountainFactory;
        }
        yield return new WaitUntil(() => Input.anyKeyDown);

        if (isFairyAppear)
        {
            fairy.SetActive(true);
            fairyEffect.SetActive(true);
            SoundManager.Instance.PlaySound("Fairy");
            DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6TinkerbellMonologues));
            yield return new WaitUntil(() => Input.anyKeyDown);
            fairyEffect.SetActive(false);
            DialogueFrame.Instance.ExecuteDialogue();
            yield return new WaitUntil(() => Input.anyKeyDown);
            Useful.SetActiveness(true, boxes[0], boxes[1], boxes[2]);
            while (DialogueFrame.dialogues.Count != 0)
            {
                if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
                yield return null;
            }

            int imsinum = /*UnityEngine.Random.Range(0, tinkerbellDialogues.Count - 1)*/1;
            DialogueFrame.Instance.AddDialogue(tinkerbellDialogues[imsinum]);


            //print("팅커벨다이얼로그 : " + tinkerbellDialogues.Count);
            //DialogueFrame.Instance.AddDialogue(Useful.RandomElement(tinkerbellDialogues));
            yield return new WaitWhile(() => !boxClicked);
            switch (imsinum)
            {
                case 0:
                    fuckingImage.GetComponent<Image>().sprite = presents[0];
                    break;
                case 1:
                    fuckingImage.GetComponent<Image>().sprite = presents[1];
                    break;
                case 2:
                    fuckingImage.GetComponent<Image>().sprite = presents[2];
                    break;
                case 3:
                    fuckingImage.GetComponent<Image>().sprite = presents[3];
                    break;
                case 4:
                    fuckingImage.GetComponent<Image>().sprite = presents[4];
                    break;
                case 5:
                    fuckingImage.GetComponent<Image>().sprite = presents[5];
                    break;
            }
            Useful.SetActiveness(false, boxes[0], boxes[1], boxes[2]);
            closedBox.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            while (DialogueFrame.dialogues.Count != 0)
            {
                if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
                yield return null;
            }
            closedBox.SetActive(false);
            fairy.SetActive(false);
        }

        fairy.SetActive(false);
        bg.sprite = downhillFactory;
        foodWaste.SetActive(false);

        var sl = new Queue<SpeechLine>();
        sl.Enqueue(new SpeechLine
        {
            Speaker = null,
            Speech = "(저 멀리서 수상한 물체가 보인다... 설마?)",
        });
        Dialogue suspicious = new Dialogue
        {
            speechLines = sl,
        };

        DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6DownMonologues));
        SoundManager.Instance.PlaySound("FootStep");
        yield return new WaitUntil(() => Input.anyKeyDown);
        shadow.SetActive(true);
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);
        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }
        DialogueFrame.Instance.AddDialogue(suspicious);

        yield return new WaitUntil(() => Input.anyKeyDown);
        shadow.SetActive(true);
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);
        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }

        //걸렸을 경우
        if (isGetCaught)
        {
            DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6Caught1Monologues));
            DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6Caught2Monologues));
            yield return new WaitUntil(() => Input.anyKeyDown);
            SoundManager.Instance.PlaySound("DryCough");
            DialogueFrame.Instance.ExecuteDialogue();
            catchingMan.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
            while (DialogueFrame.dialogues.Count != 0)
            {
                if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
                yield return null;
            }

        }
        else
        {
            bg.sprite = rabbits[0];
            yield return new WaitUntil(() => Input.anyKeyDown);

            bg.sprite = rabbits[1];
            DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene6CaughtNotMonologues));
            yield return new WaitUntil(() => Input.anyKeyDown);
            bg.sprite = rabbits[2];
            DialogueFrame.Instance.ExecuteDialogue();
            yield return new WaitUntil(() => Input.anyKeyDown);
            while (DialogueFrame.dialogues.Count != 0)
            {
                if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
                yield return null;
            }

        }

        RoutineManager.MoveScene();
    }

    private void TinkerbellDialogue()
    {
        var rows = tinkerbellText.text.Split(new char[] { '\n' });
        var firstRow = rows[0].Split(new char[] { ',' });

        string[] keywords = new string[] { "Fork", "BG", "Show", "Text", "Aff", "Health", "Execute" };

        for (int i = 1; i < rows.Length; i++) //Ignore first & last line
        {
            print("Line " + tinkerbellDialogues.Count);
            var columns = rows[i].Split(new char[] { ',' }); //No, Speaker, Dialogue
            if (Useful.IsEmpty(columns)) continue; //If all columns in single line is empty, continue
            
            var speeches = columns[2].Split(new char[] { '+' }); //Array of SpeechLines.Speech
            
            if (!string.IsNullOrEmpty(columns[0])) //If column isn't empty, new Dialogue
            {
                var speechLines = new Queue<SpeechLine>();
                speechLines.Enqueue(new SpeechLine
                {
                    Speaker = SoldierManager.moreSoldierDict[columns[1]],
                    Speech = columns[2],
                });
                tinkerbellDialogues.Add(new Dialogue { speechLines = speechLines, });
            }
            else
            {
                var speechLine = new SpeechLine
                {
                    Speaker = SoldierManager.moreSoldierDict[columns[1]],
                    Speech = columns[2],
                };
                tinkerbellDialogues[tinkerbellDialogues.Count - 1].speechLines.Enqueue(speechLine);

            }

        }
    }

}

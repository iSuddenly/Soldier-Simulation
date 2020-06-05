using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;
using System.Linq;
/// <summary>
/// Show FoodWaste
/// </summary>
public class SceneManager05 : Manager
{
    public SpriteRenderer foodWaste;
    public Sprite[] foodWasteFactories;
    public int result; //1, 2, 3, 4 the higher the better

    public Image[] foodImage;
    public GameObject[] xs;
    public Text[] foodNums;

    public GameObject yesButton, noButton;

    public GameObject panel;
    public GameObject resultBox;

    void Start()
    {
        Initialize();
        StartCoroutine(Flow());
    }

    private void Initialize()
    {
        SetFoodWaste(result);
        ActivateButtons(false);

        for (int i = 0; i < foodImage.Length; i++)
        {
            foodImage[i].sprite = FoodManager.todayFoods[i].GetSprite();
            foodNums[i].text = GameObject.Find("ToCarryOn").GetComponent<CarryData>().leftFoodAmount[i].ToString();
        }
        
    }

    IEnumerator Flow()
    {
        yield return new WaitWhile(() => !DialogueManager.scene5BadMonologues.Any());
        print("Dialogue exists well and happy");
        result = CarryData.Instance.result;
        DialogueFrame.Instance.AddDialogue(GetResultMonologue(result));
        yield return new WaitUntil(() => Input.anyKeyDown);
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);
        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }
        yield return new WaitUntil(() => Input.anyKeyDown);
        panel.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        resultBox.SetActive(true);


        var sl = new Queue<SpeechLine>();
        sl.Enqueue(new SpeechLine
        {
            Speaker = null,
            Speech = "버리러 갈까?",
        });
        Dialogue shallIGo = new Dialogue
        {
            speechLines = sl,
        };

        DialogueFrame.Instance.AddDialogue(shallIGo);
        yield return new WaitUntil(() => Input.anyKeyDown);
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);
        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }
        Useful.SetActiveness(true, yesButton, noButton);
    }

    IEnumerator Animate()
    {
        Vector3 initialScale = new Vector2(60f, 60f);
        Vector3 goalScale = new Vector2(50f, 50f);

        yield return new WaitForSeconds(1.5f);
        //x
        //countup
    }

    private void SetFoodWaste(int result)
    {
        foodWaste.sprite = foodWasteFactories[result-1];
    }

    private void ActivateButtons(bool activeness)
    {
        yesButton.SetActive(activeness);
        noButton.SetActive(activeness);
    }

    private Dialogue GetResultMonologue(int result){
        Dialogue monologue;
        switch (result)
        {
            case 1: //Bad
                monologue = DialogueManager.scene5BadMonologues.RandomElement(); //Useful. 안붙였는데도 되네?
                break;
            case 2: //Soso
                monologue = DialogueManager.scene5SosoMonologues.RandomElement();
                break;
            case 3: //Good
                monologue = DialogueManager.scene5GoodMonologues.RandomElement();
                break;
            case 4: //Perfect
                monologue = DialogueManager.scene5GoodMonologues.RandomElement();
                break;
            default:
                monologue = null;
                break;
        }
        return monologue;
    }
    
}

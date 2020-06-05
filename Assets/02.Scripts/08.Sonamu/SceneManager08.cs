using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;

public class SceneManager08 : Manager //소나무씬 매니저
{
    public static SceneManager08 Instance;

    public GameObject first;
    public GameObject second;

    public InputField inputField;
    public Text confirmedText;
    public Button confirmButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitWhile(() => DialogueManager.scene8GoodMonologues.Count == 0);
        Initialize();
        yield return new WaitUntil(() => Input.anyKeyDown);
        SoundManager.Instance.PlaySound("PageFlip");
        second.SetActive(true);
        first.SetActive(false);
    }

    void Initialize()
    {
        first.SetActive(true);
        second.SetActive(false);
        print(DialogueManager.scene8GoodMonologues.Count);
    }

    public void ConfirmDiary()
    {
        StartCoroutine(Confirm());
    }

    IEnumerator Confirm()
    {
        string writtenText = inputField.GetComponent<InputField>().text + " ";
        confirmedText.text = writtenText;
        inputField.text = "";
        confirmButton.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        string moreText = GetResultMonologue(CarryData.Instance.result).Next().Speech;

        for (int i = 0; i < moreText.Length; i++)
        {
            confirmedText.text += moreText[i];
            yield return new WaitForSeconds(.05f);
        }

        StartCoroutine(FinishDay());
    }

    IEnumerator FinishDay()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        DataManager.SaveAllData();
        CalendarManager.NextDay();
        RoutineManager.MoveScene(1);

    }

    private Dialogue GetResultMonologue(int result)
    {
        Dialogue monologue;
        switch (result)
        {
            case 1: //Bad
                monologue = DialogueManager.scene8BadMonologues.RandomElement();
                break;
            case 2: //Soso
                monologue = DialogueManager.scene8SosoMonologues.RandomElement();
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

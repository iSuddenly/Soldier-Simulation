using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExtensionMethods;

public class DialogueFrame : MonoBehaviour
{
    public static DialogueFrame Instance;

    public static List<Dialogue> dialogues = new List<Dialogue>();

    public bool isDialogueOnGoing;
    
    public bool isTyping;
    private float typingInterval = 0.03f;

    public GameObject dialogueBox;
    public Image speakerImage;
    public Image speakerTextImage;
    public Text speakerText;
    public Text speechText;
    private string targetSpeechText;

    public GameObject forkBox;
    public Button forkYes;
    public Button forkNo;
    public Text forkYesText;
    public Text forkNoText;

    public Sprite blankSpriteFactory;

    public Image bgImg;
    public Image objectImg;
    public GameObject popUp;
    public Text popUpText;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
        speakerImage.sprite = blankSpriteFactory;
        bgImg = GameObject.Find("BG").GetComponent<Image>();
    }

    void Start()
    {
        //ActivateFork(false);
        //ActivateDialogueFrame(false);
        //ClearDialogueFrame();
    }

    public void AddDialogue(Dialogue dialogue)
    {
        dialogues.Add(dialogue);
    }

    public void AddDialogue(int kind, int? index = null)
    {
        //kind : 2.급양관대사, 5.배식후독백, 6.오르막산내리막
        if (index == null)
        {
            index = UnityEngine.Random.Range(0, DialogueManager.scene2Monologues.Count - 1); //일단 이렇게.
        }
    }
    
    public void ExecuteDialogue()
    {
        //speakerImage.gameObject.SetActive(true);
        speakerImage.sprite = blankSpriteFactory;
        ActivateDialogueFrame(true);
        ShowDialogueLine(dialogues[0].Next());
        isDialogueOnGoing = true;
    }


    private IEnumerator ClearDialogueFrame() //Clear Dialogue Texts, Deactivate Frame
    {
        StartCoroutine(DialogueCharacter.Instance.Exit());
        StartCoroutine(DialogueBox.Instance.Exit());
        yield return new WaitUntil(() => !speakerImage.enabled); //일단은 이렇게 하는데, 좋은 방법이 아닌것같다
        speakerImage.sprite = null;
        ActivateDialogueFrame(false);
    }

    public void ActivateDialogueFrame(bool isActive) //private로 하고싶은데...
    {
        dialogueBox.SetActive(isActive);
        speakerImage.gameObject.SetActive(isActive);
        if (isActive == false)
        {
            speakerImage.sprite = blankSpriteFactory;
            speakerText.text = null;
            speechText.text = null;
            speakerTextImage.gameObject.SetActive(false);
        }
    }

    private void ActivateFork(bool isActive, Fork fork = null)
    {
        forkBox.SetActive(isActive);
        if(fork != null)
        {
            forkYesText.text = fork.YesString;
            forkNoText.text = fork.NoString;
        }
    }

    public void DialogueTouched()
    {
        if (isTyping) //If typing effect is running
        {
            isTyping = false; //Cancel typing effect
        }
        else if(!isTyping && dialogues[0].speechLines.Count != 0) //If typing effect isn't running && lines are left
        {
            ShowDialogueLine(dialogues[0].Next()); //Show next line
        }
        else if(!forkBox.activeSelf) //If Dialouge has no more lines
        {
            StartCoroutine(ClearDialogueFrame());
            dialogues.RemoveAt(0);
            GetNextDialogue();
        }
    }

    private void GetNextDialogue()
    {
        if (/*dialogues.Count != 0*/ !dialogues.IsEmpty())
        {
            ExecuteDialogue();
        }
        else //All Dialogues finished
        {
            EndDialogueFrame();
            print("All Dialogues Finished");
        }
    }

    private void ShowDialogueLine(SpeechLine line)
    {
        //추후 MethodInfo로 바꾸기
        if (line.Speech.Contains("+")) //If line is keyword command
        {
            var lineChunks = line.Speech.Split('+');

            if (line.Speech.StartsWith("Fork"))
            {
                ActivateFork(true);
                forkYesText.text = lineChunks[1];
                forkNoText.text = lineChunks[2];
                speechText.text = "";
                forkYes.onClick.AddListener(() => { //Add Listener to Yes Button
                    print(lineChunks[3]);
                    dialogues.RemoveAt(0);
                    AddDialogue(DialogueManager.questDict[lineChunks[3]]);
                    print(dialogues.Count);
                    GetNextDialogue();
                });
                forkNo.onClick.AddListener(() => { //Add Listener to No Button
                    print(lineChunks[4]);
                    dialogues.RemoveAt(0);
                    AddDialogue(DialogueManager.questDict[lineChunks[4]]);
                    print(dialogues.Count);
                    GetNextDialogue();
                });
            }
            else if (line.Speech.StartsWith("Execute"))
            {
                ActivateFork(false);
                AddDialogue(DialogueManager.questDict[lineChunks[1]]);

            }
            else if (line.Speech.StartsWith("Text"))
            {
                popUpText.text = lineChunks[1];
                StartCoroutine(TextBlink());
                ActivateFork(false);
            }
            else if (line.Speech.StartsWith("BG"))
            {
                ActivateFork(false);
                float.TryParse(lineChunks[1], out float num);
                bgImg.sprite = Resources.Load<Sprite>("Quest/BG/" + lineChunks[1]);
                //speakerImage.sprite = blankSpriteFactory;
                //speechText.text = "";
                //speakerTextImage.gameObject.SetActive(false);
                StartCoroutine(DialogueCharacter.Instance.Exit());

                //휴게실(Default):0, 내무반:1, 취사장:2, 흡연장:3, PX:4
                if (lineChunks[1] == "0") Useful.SetActiveness(true, SceneManager07.Instance.questGivers); //휴게실일 경우
                else Useful.SetActiveness(false, SceneManager07.Instance.questGivers);

            }
            else if (line.Speech.StartsWith("Show"))
            {
                ActivateFork(false);
                Scene scene = SceneManager.GetActiveScene();
                if (scene.buildIndex != 6) objectImg.sprite = Resources.Load<Sprite>("Quest/Object/" + lineChunks[1]) as Sprite;
                //초코파이:1, 짬뽕볶음면:2, 치킨:3, 잔반:4, TV:5, 선물상자:6, 열린선물상자:7, 만두:8, 휴가증:9, 메뚜기:10
                print("Show : " + lineChunks[1]);
                StartCoroutine(ImageBlink());
            }
            else if (line.Speech.StartsWith("Rep"))
            {
                ActivateFork(false);
                float.TryParse(lineChunks[1], out float num);
                StatusInGame.Instance.reputation += num;
                print("Reputation Changed : " + num);
            }
            else if (line.Speech.StartsWith("Aff"))
            {
                ActivateFork(false);
                float.TryParse(lineChunks[2], out float num);
                StatusInGame.Instance.affinity[lineChunks[1]] += num;
            }
            else if (line.Speech.StartsWith("Health"))
            {
                ActivateFork(false);
                float.TryParse(lineChunks[1], out float num);
                StatusInGame.Instance.health += num;
                print("Health Changed : " + num);
            }

            return;
        }

        if (!dialogueBox.activeSelf) ActivateDialogueFrame(true);

        if (line.Speaker == null || line.Speaker == SoldierManager.moreSoldierDict["플레이어"]) //If soliloquy
        {
            print("player or null");
            speakerTextImage.gameObject.SetActive(false);
            //speakerImage.sprite = blankSpriteFactory; //나중에 수정.
            StartCoroutine(DialogueCharacter.Instance.Behind());
        }
        else //플레이어가 아닌 사람의 대사
        {
            print("other soldier");

            speakerTextImage.gameObject.SetActive(true);
            speakerImage.color = Color.white;
            speakerText.text = line.Speaker.Name;
            speakerImage.sprite = line.Speaker.Picture;
            StartCoroutine(DialogueCharacter.Instance.Front());
        }

        ActivateFork(false);
        StartCoroutine(TypingEffect(line.Speech));
    }

    private IEnumerator ImageBlink()
    {
        objectImg.gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        yield return new WaitUntil(() => Input.anyKeyDown);
        objectImg.gameObject.SetActive(false);
    }

    private IEnumerator TextBlink()
    {
        popUp.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        yield return new WaitUntil(() => Input.anyKeyDown);
        popUp.SetActive(false);
    }

    private IEnumerator TypingEffect(string speech)
    {
        speechText.text = "";
        isTyping = true;
        for (int i = 0; i < speech.Length; i++)
        {
            if (!isTyping) break;
            speechText.text += speech[i];
            yield return new WaitForSeconds(typingInterval);
            if(Useful.isPunctuationMark(speech, i))
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        speechText.text = speech;
        isTyping = false;
    }
    

    private void EndDialogueFrame()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex == 7) bgImg.sprite = Resources.Load<Sprite>("Quest/BG/0");

        StartCoroutine(DialogueCharacter.Instance.Exit());
        print("Dialogue Finished and dialogues list has " + dialogues.Count);
        isDialogueOnGoing = false;
    }
}

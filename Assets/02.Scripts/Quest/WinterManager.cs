using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;
/// <summary>
/// Main Game
/// </summary>
public class WinterManager : Manager
{
    public static WinterManager Instance;

    public bool isWinter;

    public Transform soldierSpawn;
    public Transform cookSpawn;

    [Header("Where Sprites will be placed")]
    public SpriteRenderer riceHouse, gookHouse;
    public Transform banchan1Mother, banchan2Mother, banchan3Mother;
    
    [Header("Food Settings")]
    public GameObject foodFactory;
    public static int foodAmount; //Amount of food to be created
    public GameObject[] foodStickers = new GameObject[5]; //Real food prefab

    [Header("ETC")]
    public GameObject pictogram;
    public GameObject speechBubble;
    public GameObject moveButton, doneButton;

    public GameObject soldierFactory; //Prefab of soldier
    public int iterationNum = 0; //Number of soldier iteration
    public SoldierMove currentSoldier; //Soldier in progress
    
    public bool isAskingMore; //Is current soldier asking more?
    public bool isGotMore; //Has current soldier got more food?

    private void Awake()
    {
        if (Instance == null) Instance = this;

        ActivateGiveMoreUI(false);
        moveButton.SetActive(false);
        doneButton.SetActive(false);
    }
    

    public void StartMainGame()
    {
        print("Start Main Game");
        moveButton.SetActive(true);
        MakeSoldier();
        PictogramManager.Instance.nextOneTrigger = true;
    }

    private void MakeSoldier()
    {
        if (iterationNum >= SoldierManager.soldiersToday.Count) return;
        print(iterationNum + " : " + SoldierManager.soldiersToday[iterationNum].Name);
        GameObject s = Instantiate(soldierFactory);
    }

    public void GiveFood()
    {
        if (foodAmount > 0) foodAmount--;
        if (isAskingMore) isGotMore = true;
    }

    public void DoneGiving() //Done Button
    {
        if (!isAskingMore) //At first trial
        {
            currentSoldier.CheckFulfilled();
            if (currentSoldier.fulfilled == true) //If soldier is fulfilled
            {
                print("병사가 배식에 만족하고 넘어갑니다.");
                BaechicButton.Instance.MoveSikpan();
                CallNextSoldier();
            }
            else //If soldier isn't fulfilled
            {
                print("병사가 배식에 만족하지 못해 더 달라고 합니다");
                StartCoroutine(BaechicButton.Instance.SikpanPingPong());
                SayMore();
                currentSoldier.GetAngry();
                ActivateGiveMoreUI(true);
            }
        }
        else //At retrial
        {
            if (isGotMore) //If current soldier got more food
            {
                currentSoldier.CheckFulfilled();
                if (currentSoldier.fulfilled == true) //If soldier is fulfilled
                {
                    print("병사가 배식에 만족하고 넘어갑니다.");
                    BaechicButton.Instance.MoveSikpan();
                    CallNextSoldier();
                    isAskingMore = false;
                }
                else //If soldier isn't fulfilled
                {
                    print("병사가 배식에 만족하지 못해 더 달라고 합니다");
                    StartCoroutine(BaechicButton.Instance.SikpanPingPong());
                    SayMore();
                    currentSoldier.GetAngry();
                    ActivateGiveMoreUI(true);
                }
                isGotMore = false;
                isAskingMore = true;
            }
            else //If I didn't give more food
            {
                print("병사가 배식에 만족하지 못한 채 넘어갑니다.");
                BaechicButton.Instance.MoveSikpan();
                currentSoldier.GetAngry(); //Soldier gets angry
                CallNextSoldier();
                isAskingMore = false;
                //호감도 깎는 작업
            }
        }
    }

    public void CallNextSoldier()
    {
        doneButton.GetComponent<Button>().interactable = false;
        if (iterationNum + 1 >= SoldierManager.soldiersToday.Count)
        {
            print("WANT TO STOP HERE");
            StartCoroutine(MoveToNextScene());
            currentSoldier.MakeSoldierDie();
            ActivateGiveMoreUI(false);
            doneButton.SetActive(false);
            return;
        }

        currentSoldier.MakeSoldierDie();
        iterationNum++; //이거 인덱스가 나뉘어있는게 바람직하지 않다
        MakeSoldier();
        ActivateGiveMoreUI(false);
        SikpanManager.Instance.EmptySikpan();//식판 초기화
        PictogramManager.Instance.NextOne();
    }

    public void ActivateGiveMoreUI(bool isActive)
    {
        Useful.SetActiveness(isActive, speechBubble/*, yesButton, noButton*/);
    }

    private IEnumerator MoveToNextScene()
    {
        //CarryData.Instance.leftFoodAmount = foodAmount;
        yield return new WaitForSeconds(3.0f);
        RoutineManager.MoveScene();
    }

    public void SayHello()
    {
        speechBubble.GetComponentInChildren<Text>().text = SoldierManager.soldiersToday[iterationNum].SayHello();
        speechBubble.SetActive(true);
    }

    public void SayMore()
    {
        speechBubble.GetComponentInChildren<Text>().text = SoldierManager.soldiersToday[iterationNum].SayMore();
        isAskingMore = true;
    }

}

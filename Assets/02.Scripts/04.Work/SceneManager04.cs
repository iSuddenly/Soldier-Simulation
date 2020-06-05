using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;
/// <summary>
/// Main Game
/// </summary>
public class SceneManager04 : Manager
{
    public static SceneManager04 Instance;

    public bool isWinter;

    public Transform soldierSpawn;
    public Transform cookSpawn;

    [Header("Where Sprites will be placed")]
    public SpriteRenderer riceHouse, gookHouse;
    public Transform banchan1Mother, banchan2Mother, banchan3Mother;

    [Header("Food Sprites")]
    public Sprite[] foodSprites = new Sprite[5]; //Rice_T1, Gook_T1, Side1, Side2, Side3
    public Sprite[] gookSprites = new Sprite[6]; //S1, S2, S3, T1, T2, T3
    public Sprite[] riceSprites = new Sprite[6]; //S1, S2, S3, T1, T2, T3

    [Header("Food Settings")]
    public GameObject foodFactory;
    public static int[] foodAmount = new int[5]; //Amount of food to be created
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
    
    IEnumerator Start()
    {
        yield return new WaitWhile(() => FoodManager.todayFoods.IsNullOrEmpty());
        SetFoodSprites();
    }

    private void SetFoodSprites()
    {
        string riceID = FoodManager.todayFoods[0].SpriteID.ToString();
        string gookID = FoodManager.todayFoods[1].SpriteID.ToString();

        for (int i = 0; i < foodSprites.Length; i++)
        {
            foodSprites[i] = FoodManager.todayFoods[i].GetSprite();
        }
        for (int i = 0; i < 3; i++)
        {
            riceSprites[i] = FoodManager.todayFoods[0].GetSprite(true, i+1);
            riceSprites[i + 3] = FoodManager.todayFoods[0].GetSprite(false, i+1);
            gookSprites[i] = FoodManager.todayFoods[1].GetSprite(true, i + 1);
            gookSprites[i + 3] = FoodManager.todayFoods[1].GetSprite(false, i + 1);
        }

        for (int i = 0; i < foodAmount.Length; i++)
        {
            float offset = FoodManager.todayFoods[i].Amount * SoldierManager.soldiersToday.Count;
            foodAmount[i] = (int)Random.Range(offset - 5, offset + 5);
            print($"i번째 음식 : {foodAmount[i]}개");
        }

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

    public void GiveFood(int which)
    {
        if(foodAmount[which] > 0) foodAmount[which]--;
        switch (which)
        {
            case 0:
                print("밥배식");
                if (foodAmount[which] == 15) riceHouse.sprite = riceSprites[2];
                else if (foodAmount[which] == 7) riceHouse.sprite = riceSprites[3];
                else if (foodAmount[which] == 0) riceHouse.sprite = null;
                break;
            case 1:
                print("국배식");
                if (foodAmount[which] == 15) gookHouse.sprite = gookSprites[5];
                else if (foodAmount[which] == 7) gookHouse.sprite = gookSprites[6];
                else if (foodAmount[which] == 0) gookHouse.sprite = null;
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
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
            MoveToNextScene();
            currentSoldier.MakeSoldierDie();
            ActivateGiveMoreUI(false);
            doneButton.SetActive(false);
            return;
        }

        currentSoldier.MakeSoldierDie();
        iterationNum++; //이거 인덱스가 나뉘어있는게 바람직하지 않다
        MakeSoldier();
        ActivateGiveMoreUI(false);
        SikpanManager.Instance.EmptySikpan(); //식판 초기화
        PictogramManager.Instance.NextOne();
    }
    
    public void ActivateGiveMoreUI(bool isActive) 
    {
        Useful.SetActiveness(isActive, speechBubble);
    }

    public void MoveToNextScene()
    {
        CarryData.Instance.leftFoodAmount = foodAmount;
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

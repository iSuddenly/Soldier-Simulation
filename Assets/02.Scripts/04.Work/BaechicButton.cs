using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaechicButton : MonoBehaviour
{
    public static BaechicButton Instance = null;

    //Movement of FoodBucket
    public GameObject foodBucket;
    float bucketLerpTime = 0.5f;
    float bucketCurrentLerpTime = 0f;
    bool isMovingBucket;
    bool isShowingLeft = true;
    Vector2 leftSidePos = new Vector2(0, -1);
    Vector2 rightSidePos = new Vector2(-5.6f, -1f);
    public Sprite leftSprite, rightSprite;
    public Image moveButtonImage;
    
    //Movement of FoodTray
    public GameObject foodTray;
    float foodTrayLerpTime = 0.3f;
    float foodTrayCurrentLerpTime = 0f;
    private bool isMovingTray;
    private bool isHidingTray = true;
    Vector2 upSidePos = new Vector2(0, 7);
    Vector2 downSidePos = new Vector2(0, 0.77f);
    public GameObject doneButton; //배식완료 버튼

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        BaechicDaeControl();
        FoodTrayControl();
    }

    private void FoodTrayControl()
    {
        if (isMovingTray)
        {
            float perc = foodTrayCurrentLerpTime / foodTrayLerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            if (isHidingTray) foodTray.transform.position = Vector2.Lerp(upSidePos, downSidePos, perc);
            else foodTray.transform.position = Vector2.Lerp(downSidePos, upSidePos, perc);
            foodTrayCurrentLerpTime += Time.deltaTime;
            if (foodTrayCurrentLerpTime > foodTrayLerpTime)
            {
                isMovingTray = false;
                isHidingTray = !isHidingTray;
                DoneButtonActiveness();
                if (!isHidingTray) doneButton.SetActive(true);
                foodTrayCurrentLerpTime = 0;
            }
        }
    }

    private void BaechicDaeControl()
    {
        if (isMovingBucket)
        {
            float perc = bucketCurrentLerpTime / bucketLerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            if (isShowingLeft) foodBucket.transform.position = Vector2.Lerp(leftSidePos, rightSidePos, perc);
            else foodBucket.transform.position = Vector2.Lerp(rightSidePos, leftSidePos, perc);
            bucketCurrentLerpTime += Time.deltaTime;
            if (bucketCurrentLerpTime > bucketLerpTime)
            {
                isMovingBucket = false;
                if (isShowingLeft)
                {
                    foodBucket.transform.position = rightSidePos;
                    moveButtonImage.sprite = leftSprite;
                }
                else
                {
                    foodBucket.transform.position = leftSidePos;
                    moveButtonImage.sprite = rightSprite;
                }
                isShowingLeft = !isShowingLeft;
                bucketCurrentLerpTime = 0;
            }
        }
    }

    public void MoveBaechicdae()
    {
        isMovingBucket = true; //Trigger
    }
    
    public void MoveSikpan()
    {
        isMovingTray = true; //Trigger
        DoneButtonActiveness();
    }

    public IEnumerator SikpanPingPong()
    {
        isMovingTray = true;
        DoneButtonActiveness();
        yield return new WaitForSeconds(0.5f);
        isMovingTray = true;
        DoneButtonActiveness();
    }

    public void DoneButtonActiveness()
    {
        if (SceneManager04.Instance.currentSoldier.state == SoldierMove.State.InProgress && !isMovingTray && !isHidingTray)
        {
            doneButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            doneButton.GetComponent<Button>().interactable = false;
        }
    }
    
}

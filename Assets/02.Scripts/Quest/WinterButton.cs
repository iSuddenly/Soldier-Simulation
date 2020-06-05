using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinterButton : MonoBehaviour
{
    public static WinterButton Instance = null;

    //배식통의 움직임과 관련된 변수들
    public GameObject baechictong;
    float baeLerpTime = 0.5f;
    float baeCurrentLerpTime = 0f;
    bool isMovingBaechicdae;
    bool isShowingLeft = true;
    Vector2 leftSidePos = new Vector2(0, -1);
    Vector2 rightSidePos = new Vector2(-5.6f, -1f);
    public Sprite leftSprite, rightSprite;
    public Image moveButtonImage;

    //식판의 움직임과 관련된 변수들
    public GameObject sikpan;
    float sikpanLerpTime = 0.3f;
    float sikpanCurrentLerpTime = 0f;
    private bool isMovingSikpan;
    private bool isHidingSikpan = true;
    Vector2 upSidePos = new Vector2(0, 7);
    Vector2 downSidePos = new Vector2(0, 0.77f);
    public GameObject doneButton; //배식완료 버튼

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        BaechicDaeControl();
        SikpanControl();
    }

    private void SikpanControl()
    {
        if (isMovingSikpan)
        {
            float perc = sikpanCurrentLerpTime / sikpanLerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            if (isHidingSikpan) sikpan.transform.position = Vector2.Lerp(upSidePos, downSidePos, perc);
            else sikpan.transform.position = Vector2.Lerp(downSidePos, upSidePos, perc);
            sikpanCurrentLerpTime += Time.deltaTime;
            if (sikpanCurrentLerpTime > sikpanLerpTime)
            {
                isMovingSikpan = false;
                isHidingSikpan = !isHidingSikpan;
                DoneButtonActiveness();
                if (!isHidingSikpan) doneButton.SetActive(true);
                sikpanCurrentLerpTime = 0;
            }
        }
    }

    private void BaechicDaeControl()
    {
        if (isMovingBaechicdae)
        {
            float perc = baeCurrentLerpTime / baeLerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            if (isShowingLeft) baechictong.transform.position = Vector2.Lerp(leftSidePos, rightSidePos, perc);
            else baechictong.transform.position = Vector2.Lerp(rightSidePos, leftSidePos, perc);
            baeCurrentLerpTime += Time.deltaTime;
            if (baeCurrentLerpTime > baeLerpTime)
            {
                isMovingBaechicdae = false;
                if (isShowingLeft)
                {
                    baechictong.transform.position = rightSidePos;
                    moveButtonImage.sprite = leftSprite;
                }
                else
                {
                    baechictong.transform.position = leftSidePos;
                    moveButtonImage.sprite = rightSprite;
                }
                isShowingLeft = !isShowingLeft;
                baeCurrentLerpTime = 0;
            }
        }
    }

    public void MoveBaechicdae()
    {
        isMovingBaechicdae = true; //Trigger
    }

    public void MoveSikpan()
    {
        isMovingSikpan = true; //Trigger
        DoneButtonActiveness();
    }

    public IEnumerator SikpanPingPong()
    {
        isMovingSikpan = true;
        DoneButtonActiveness();
        yield return new WaitForSeconds(0.5f);
        isMovingSikpan = true;
        DoneButtonActiveness();

    }

    public void DoneButtonActiveness()
    {
        if (SceneManager04.Instance.currentSoldier.state == SoldierMove.State.InProgress && !isMovingSikpan && !isHidingSikpan)
        {
            doneButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            doneButton.GetComponent<Button>().interactable = false;
        }
    }


}

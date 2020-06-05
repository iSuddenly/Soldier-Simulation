using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager01 : Manager
{
    public static SceneManager01 Instance;

    private int passedDays;
    private int yestPassedDays;

    public Text[] todayNum = new Text[3]; //000일차 오늘꺼
    public Text[] yesterdayNum = new Text[3]; //00일차 어제꺼
    public Transform[] dayNums = new Transform[3]; //0 00 000
    public Transform startPos;
    public Transform goalPos;

    public GameObject holiday;

    public Image bar; //Filling bar
    public Text barNum; //00%

    public GameObject pressAnyKey;

    private IEnumerator Start()
    {
        barNum.text = "";
        bar.fillAmount = 0;
        pressAnyKey.SetActive(false);
        
        holiday.SetActive(CalendarManager.IsHolidayScheduled());
        passedDays = CalendarManager.passedDays;
        yestPassedDays = CalendarManager.yestPassedDays;

        StartCoroutine(CountUp());
        yield return null;
    }
    
    private IEnumerator CountUp() //00일차 숫자 차오르는 애니메이션
    {
        print("CountUp");
        float countLerpTime = 0.5f;
        float countCurrentLerpTime = 0f;

        int one = passedDays / 1 % 10;
        int ten = passedDays / 10 % 10;
        int hundred = passedDays / 100 % 10; 
        todayNum[0].text = (hundred == 0) ? "" : hundred.ToString();
        todayNum[1].text = (hundred == 0 && ten == 0) ? "" : ten.ToString();
        todayNum[2].text = one.ToString();
        
        int yestOne = yestPassedDays / 1 % 10;
        int yestTen = yestPassedDays / 10 % 10;
        int yestHundred = yestPassedDays / 100 % 10;
        yesterdayNum[0].text = (yestHundred == 0) ? "" : yestHundred.ToString();
        yesterdayNum[1].text = (yestHundred == 0 && yestTen == 0) ? "" : yestTen.ToString();
        yesterdayNum[2].text = yestOne.ToString();

        yield return new WaitForSeconds(1.0f);

        while (dayNums[2].position.y <= goalPos.position.y)
        {
            float perc = countCurrentLerpTime / countLerpTime;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            dayNums[2].transform.position = Vector2.Lerp(startPos.position, goalPos.position, perc);
            if (one == 0) { dayNums[1].transform.position = Vector2.Lerp(startPos.position, goalPos.position, perc); }
            if (ten == 0) { dayNums[0].transform.position = Vector2.Lerp(startPos.position, goalPos.position, perc); }
            countCurrentLerpTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }

        //추후에 일 십 백 살짝 타이밍 다르게 차오르게 변경
        //위치 제대로 오도록 변경
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FillBar());
    }

    private IEnumerator FillBar() //Bar filling animation
    {
        print("Fill Bar");
        float lerpDuration = 1f;
        float currentLerpTime = 0;

        float barInitialNum = yestPassedDays;
        float barGoalNum = (float)passedDays  / CalendarManager.totalDays * 100; //최종으로 나타날 바 퍼센트 수.
        //barGoalNum = todayIndex * 100 / dayEnd;
        //print(barGoalNum); //8

        while (currentLerpTime <= lerpDuration)
        {
            float perc = currentLerpTime / lerpDuration;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);

            bar.fillAmount = Mathf.Lerp(barInitialNum, barGoalNum / 100, currentLerpTime);
            barNum.text = Mathf.Lerp(barInitialNum, barGoalNum, currentLerpTime).ToString("0") + "%";
            
            StickTextToBar(bar.transform.position.x - bar.transform.localScale.x / 2 + bar.transform.localScale.x * perc * barGoalNum / 100);

            currentLerpTime += Time.deltaTime;

            yield return new WaitForSeconds(0.01f);
        }
        
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("PressAnyKey");
    }

    private void StickTextToBar(float x)
    {
        barNum.gameObject.transform.position = new Vector2(x, bar.gameObject.transform.position.y);
    }    

    private IEnumerator PressAnyKey()
    {
        StartCoroutine("Blink");
        yield return new WaitUntil(() => Input.anyKeyDown);
        RoutineManager.MoveScene();
    }

    private IEnumerator Blink()
    {
        //코루틴 간단하게 하나로 합칠 수 없을까. 굳이 두개 쓰기 좀 그렇다
        while (!Input.anyKeyDown)
        {
            pressAnyKey.SetActive(!pressAnyKey.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

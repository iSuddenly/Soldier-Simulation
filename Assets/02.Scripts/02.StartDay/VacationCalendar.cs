using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
/// <summary>
/// Create Calendar, Set Vacation
/// </summary>
public class VacationCalendar : MonoBehaviour
{
    public Text monthText;
    public Button[] dayButtons;
    public Text showingDayText; //몇박 쓸 수 있는지 알려주는 텍스트
    public GameObject toast; //pop-up toast

    public Color32 originalColor = Color.white;
    public Color32 selectedColor;
    public Color32 confirmedColor;

    public int availableVacation = 10;

    List<DateTime> tempVacation = new List<DateTime>();
    List<Button> clickedButtons = new List<Button>();

    public GameObject resetButton;
    public GameObject confirmButton;


    private void Awake()
    {
        dayButtons = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        selectedColor = new Color32(255, 190, 190, 255);
        confirmedColor = new Color32(255, 146, 146, 255);
        WriteDays();
        ChangeDayText(availableVacation);
    }


    private void WriteDays()
    {
        int month = CalendarManager.todayDate.Month;//몇월
        int firstDayOfMonth = CalendarManager.firstDayOfMonth; //1일 요일. 임시로 토요일
        int totalDaysInMonth = CalendarManager.totalDaysInMonth; //총 몇일 있는지. 임시로 총 31일

        monthText.text = month.ToString()+"월";
        for(int i = 0; i < dayButtons.Length; i++)
        {
            if(i < 6 || i > totalDaysInMonth+firstDayOfMonth-1) //또는 휴가 못가는 날짜일 때 추가
            {
                dayButtons[i].interactable = false;
                continue;
            }
            //dayButtons[i].image.color = originalColor;
            dayButtons[i].GetComponentInChildren<Text>().text = (i-firstDayOfMonth+1).ToString();
            int closureIndex = i;
            dayButtons[closureIndex].onClick.AddListener(() => DayButtonClick(closureIndex));
        }
        
    }

    private void ButtonState() //특수 날짜 버튼 비활성화 및 표기
    {
        
    }

    public void DayButtonClick(int i)
    {
        print("Button "+ i +" Clicked");
        if (dayButtons[i].image.color != selectedColor && dayButtons[i].image.color != confirmedColor) //나중에 수정.
        {
            dayButtons[i].image.color = selectedColor;
            tempVacation.Add(new DateTime(CalendarManager.todayDate.Year, CalendarManager.todayDate.Month, 1).AddDays(i - CalendarManager.firstDayOfMonth));
            clickedButtons.Add(dayButtons[i]);
            ChangeDayText(availableVacation - clickedButtons.Count);
        }
    }

    public void ResetVacation()
    {
        foreach (var button in clickedButtons)
        {
            button.image.color = originalColor;
        }
        tempVacation.Clear();
        clickedButtons.Clear();
        ChangeDayText(availableVacation);
    }

    public void ConfirmVacation() //휴가 확정 버튼
    {
        if (!tempVacation.Any()) return; //If no date is chosen return

        tempVacation.Sort((a, b) => a.CompareTo(b)); //Sort Temp Vacation list
        TimeSpan ts = tempVacation[tempVacation.Count - 1] - tempVacation[0];

        if (ts.Days != tempVacation.Count-1){ //만약에 날짜들이 연속되지 않았을 경우
            print(ts.Days + " : " + tempVacation.Count);
            ResetVacation();
            StartCoroutine(Toast("한번에 연속된\n휴가일만 신청가능합니다."));
        }
        else if(tempVacation.Count > availableVacation) //사용가능 휴가일을 넘었을 경우
        {
            print(ts.Days + " : " + tempVacation.Count);
            ResetVacation();
            StartCoroutine(Toast("허용된 휴가일 이상 신청할 수 없습니다."));
        }
        else
        {
            CalendarManager.SetVacation(tempVacation);
            foreach (var button in clickedButtons)
            {
                button.interactable = false;
                button.image.color = confirmedColor;
            }
            resetButton.SetActive(false);
            confirmButton.SetActive(false);
            tempVacation.Clear();
            clickedButtons.Clear();
            DiableDayButtons();
            StartCoroutine(Toast("휴가 신청이 완료되었습니다."));
        }
    }

    private void DiableDayButtons()
    {
        for (int i = 0; i < dayButtons.Length; i++)
        {
            dayButtons[i].interactable = false;
        }
    }

    private void ChangeDayText(int i)
    {
        showingDayText.text = "사용가능 휴가일수 : " + i.ToString() + "박";
    }
    
    private IEnumerator Toast(string str)
    {
        toast.GetComponentInChildren<Text>().text = str;
        toast.SetActive(true);
        yield return new WaitForSeconds(.5f);
        toast.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
/// <summary>
/// Handles time flow from game start to ending
/// Date of today, Vacation, Main Quests, ...
/// </summary>
public static class CalendarManager
{
    private static readonly Calendar calendar;

    public static int playedDays = 0;

    private static readonly DateTime initialDate = new DateTime(2014, 3, 4); //입대 날짜
    private static readonly DateTime endDate = new DateTime(2015, 12, 3); //전역일 날짜

    public static DateTime yesterdayDate; //전날 날짜
    public static DateTime todayDate; //오늘 날짜

    public static int totalDays; //총 날짜
    public static int passedDays; //입대일 이후 지난 날
    public static int yestPassedDays; //전날기준 입대일 이후 지난 날
    public static int leftDays; //전역일까지 남은 날 557

    public static List<DateTime> scheduledVacation = new List<DateTime>(); //Scheduled Vacation
    public static Dictionary<string, List<DateTime>> specialDates; //특수한 날짜. 휴가 지정 불가

    private static int leftBreakDays; //총 남은 휴가일수

    public static int firstDayOfMonth; //달의 첫째 요일
    public static int totalDaysInMonth; //현재 달의 총 날짜 수

    static CalendarManager()
    {
        calendar = CultureInfo.InvariantCulture.Calendar;

        todayDate = new DateTime(2014, 3, 12); //임시
        yesterdayDate = new DateTime(2014, 3, 4); //임시

        totalDays = (endDate - initialDate).Days;
        GetPassedDays();

        SetSpecialDays();

        firstDayOfMonth = (int)new DateTime(todayDate.Year, todayDate.Month, 1).DayOfWeek;
        totalDaysInMonth = DateTime.DaysInMonth(todayDate.Year, todayDate.Month);
    }

    /// <summary>
    /// 2014.3.3~3.31 취사병(이병)
    /// 2014 6.2~6.30 취사병(일병)
    /// 2015.1.1~31 취사병(상병)
    /// 2015 9.1~9.30 취사병(병장)
    /// </summary>
    private static void SetSpecialDays()
    {
        // 지정휴가
        specialDates["휴가"] = new List<DateTime>();

        //임시로 넣어둔 특수날짜
        specialDates["임시"] = new List<DateTime>{
            new DateTime(2014, 3, 19),
            new DateTime(2014, 3, 20),
            new DateTime(2014, 3, 21),
        };

        // 유격: 2014.6.19~21
        specialDates["유격"] = new List<DateTime>{
            new DateTime(2014, 6, 19),
            new DateTime(2014, 6, 20),
            new DateTime(2014, 6, 21),
        };

        // 혹한기: 2015.1.9~11
        specialDates["혹한기"] = new List<DateTime>{ 
            new DateTime(2015, 1, 10),
            new DateTime(2015, 1, 11),
        };

    }

    public static void SetVacation(List<DateTime> vacationList)
    {
        specialDates["휴가"].AddRange(vacationList);
        specialDates["휴가"].Sort((a, b) => a.CompareTo(b));

        foreach (var vacation in specialDates["휴가"]) Debug.Log(vacation);
    }


    public static float GetDayPercent()
    {
        passedDays = (todayDate - initialDate).Days;
        return (float)passedDays / (float)totalDays;
    }

    public static void NextDay()
    {
        yesterdayDate = todayDate;
        yestPassedDays = passedDays;

        playedDays++;

        //일단 휴가 고려 안한것.
        if ((int)todayDate.DayOfWeek == 6) //토요일이면
        {
            todayDate.AddDays(1);
        }
        else
        {
            todayDate.AddDays(8);
        }
        GetPassedDays();
    }

    public static int GetPassedDays()
    {
        passedDays = (todayDate - initialDate).Days;
        return passedDays;
    }

    public static bool IsHolidayScheduled()
    {
        return (scheduledVacation.Count != 0);
    }

    public static void AddImsiBreak(int num)
    {

    }

    public static DateTime GetEarliestBreak() //가장 빠른 휴가일 반환
    {
        return scheduledVacation[0];
    }
}

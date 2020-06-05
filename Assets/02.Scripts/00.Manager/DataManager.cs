using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Save and Load Game Data
/// </summary>
public static class DataManager
{
    public static void LoadAllData()
    {
        LoadHealth();
        LoadAffinity();
        LoadReputation();
        LoadCalendar();
        LoadItem();
        LoadQuest();
        LoadFoodWaste();
        LoadSonamu();
        LoadSettings();
    }

    public static void LoadInGameData()
    {

    }

    public static void LoadOuterData()
    {

    }


    public static void SaveAllData()
    {
        SaveHealth();
        SaveAffinity();
        SaveReputation();
        SaveCalendar();
        SaveItem();
        SaveQuest();
        SaveFoodWaste();
        SaveSonamu();
        SaveSettings();
    }

    private static void ResetAllData()
    {

    }

    private static void LoadHealth()
    {
        throw new NotImplementedException();
    }

    private static void LoadAffinity()
    {
        throw new NotImplementedException();
    }

    private static void LoadReputation()
    {
        throw new NotImplementedException();
    }

    private static void LoadCalendar()
    {
        throw new NotImplementedException();
    }

    private static void LoadItem()
    {
        throw new NotImplementedException();
    }

    private static void LoadQuest()
    {
        throw new NotImplementedException();
    }

    private static void LoadFoodWaste()
    {
        throw new NotImplementedException();
    }

    private static void LoadSonamu()
    {
        throw new NotImplementedException();
    }

    private static void LoadSettings()
    {
        throw new NotImplementedException();
    }

    private static void SaveHealth() //체력
    {

    }
    
    private static void SaveAffinity() //고정군인별 호감도
    {

    }

    private static void SaveReputation() //전체 호감도
    {

    }

    private static void SaveCalendar() //날짜, 휴가
    {

    }

    private static void SaveItem()//아이템. 휴가증 초코파이 등
    {

    }

    private static void SaveQuest()//퀘스트 진행상황. 다음날로 이어지는 퀘스트
    {

    }

    private static void SaveFoodWaste() //잔반량
    {

    }

    private static void SaveSonamu() //소나무
    {

    }

    private static void SaveSettings() //설정
    {
        //플레이어 이름, 진동, 소리
    }

    private static void SaveInfo() //기타 정보
    {
        //플레이타임
    }


    
}

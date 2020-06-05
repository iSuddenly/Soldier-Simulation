using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using System.Linq;

public class Soldier
{
    public int? Index { get; private set; }
    public string Name { get; private set; }
    public string RealName { get; private set; }
    public Sprite Picture { get; private set; } //고정군인 10명뿐이라 이미지 넣어두고 계속 사용
    public Sprite[] facePictures = new Sprite[5];
    public float Affinity { get; set; }
    public int Rank { get; private set; }
    public List<int> likedFoods = new List<int>();
    public List<int> hatedFoods = new List<int>();
    public string[] helloSpeeches;
    public string[] moreSpeeches;

    private string spritePath = "Soldiers/SF";

    public Soldier() //For Random Soldiers
    {
        this.Rank = UnityEngine.Random.Range(1, 5);
        //좋아하는 음식, 싫어하는 음식 작업. 높은 확률로 조기튀김을 싫어한다

        switch (Rank)
        {
            case 1:
                this.helloSpeeches = new string[5] { "충성!", "윽 배고픕니다!", "맛있는 거 나왔으면!", "맛있는 밥...", "요리가 먹고싶다...!" };
                switch (StatusInGame.playerRank)
                {
                    case 1:
                        this.moreSpeeches = new string[4] { "조금 더 주십시오!", "더 주시지 말입니다!", "배가 고픕니다!", "반찬..조금만 더 주시겠습니까!" };
                        break;
                    case 2:
                        this.moreSpeeches = new string[3] { "혹시 더 주실 수 있으십니까!", "배가 아주 고픕니다!", "훈련해서 허기가 집니다!" };
                        break;
                    case 3:
                        this.moreSpeeches = new string[3] { "조금만 더 주십시오!!", "배가 너무 고픕니다!!", "양이 부족합니다..!!" };
                        break;
                    case 4:
                        this.moreSpeeches = new string[3] { "조..조금만 더 주시겠습니까!!!", "양이 모자란 것 같습니다!!!", "더 주시겠습니까!!!" };
                        break;
                }
                break;
            case 2:
                this.helloSpeeches = new string[4] { "이따 족구해야겠다", "싸지방 가고 싶다...", "악 집 가고 싶다", "재밌는 거 어디 없나..." };
                switch (StatusInGame.playerRank)
                {
                    case 1:
                        this.moreSpeeches = new string[3] { "더 주십시오", "훈련 받아서 허기집니다", "조금만 더.." };
                        break;
                    case 2:
                        this.moreSpeeches = new string[3] { "더 줘~~~~", "배가 고픕니다", "맛있는 반찬으로다가 더 주십시오" };
                        break;
                    case 3:
                        this.moreSpeeches = new string[3] { "더 주시겠습니까 상병님!", "모자랍니다!", "더 주십시오!" };
                        break;
                    case 4:
                        this.moreSpeeches = new string[3] { "배에서 꼬르륵 소리가 나지 말입니다!!", "배가 안 찰 것 같습니다!!", "더 주시겠습니까!!" };
                        break;
                }
                break;
            case 3:
                this.helloSpeeches = new string[5] { "힘들다", "밥시간이 돌아왔다", "배가 고파...", "이따 족구나 한 판 때려야겠다", "잠이나 자고 싶네" };
                switch (StatusInGame.playerRank)
                {
                    case 1:
                        this.moreSpeeches = new string[3] { "에이 더 줘라", "배고프다고~", "양이 너무 적은 거 같은데" };
                        break;
                    case 2:
                        this.moreSpeeches = new string[3] { "야야 더 줘라", "너 병장한테만 맛있는 거 주지?", "더 달라니까 그러네!" };
                        break;
                    case 3:
                        this.moreSpeeches = new string[3] { "맛있는 것 좀 더 줘라", "아이고 배가 고프지 말입니다", "더 주시죠 " };
                        break;
                    case 4:
                        this.moreSpeeches = new string[3] { "배가 고픕니다!", "더 주시지 말입니다", "병장님 양이 모자랍니다" };
                        break;
                }
                break;
            case 4:
                this.helloSpeeches = new string[6] { "으허~아암", "전역 얼마나 남았지...", "제대하고 싶다...", "질린다 질려..." , "PX갈 걸...", "어휴 피곤해" };
                switch (StatusInGame.playerRank)
                {
                    case 1:
                        this.moreSpeeches = new string[4] { "이럴거냐?", "개미똥만큼 주네", "야 더 줘!", "이걸 누구 코에 붙이라고?!" };
                        break;
                    case 2:
                        this.moreSpeeches = new string[3] { "지금 나 다이어트 시키냐?", "알아서 더 주고 해라~응?", "아 완전 배고파" };
                        break;
                    case 3:
                        this.moreSpeeches = new string[3] { "야야 더 달라고", "배고프다고 말 하는 거 못 들었냐", "가득 달라고 가득!" };
                        break;
                    case 4:
                        this.moreSpeeches = new string[3] { "에헤이~ 더 주시죠", "같은 병장끼리 이러깁니까?", "양이 너무 모자란 거 아닙니까~" };
                        break;
                }
                break;
        }                                            

    }

    public Soldier(int index, string name) //For NPC Soldiers
    {
        this.Index = index;
        this.Name = name;
        this.Picture = Resources.Load<Sprite>(spritePath + index.ToString());
    }

    public Soldier(int index, string name, string realName, string rankStr, List<int> likedFoods, List<int> hatedFoods, string affinityStr, string[] helloSpeeches, string[] moreSpeeches)
    {
        this.Index = index;
        this.Name = name;
        this.RealName = realName;
        this.Picture = Resources.Load<Sprite>(spritePath + index.ToString());
        SetPictureSprites(index);
        int.TryParse(rankStr, out int rankInt);
        this.Rank = rankInt;
        this.likedFoods = likedFoods;
        this.hatedFoods = hatedFoods;
        float.TryParse(affinityStr, out float affinityFloat);
        this.Affinity = affinityFloat;

        this.helloSpeeches = new string[helloSpeeches.Length];
        this.moreSpeeches = new string[moreSpeeches.Length];
        this.helloSpeeches = helloSpeeches;
        this.moreSpeeches = moreSpeeches;
    }

    private void SetPictureSprites(int index)
    {
        for (int i = 0; i < facePictures.Length; i++)
        {
            this.facePictures[i] = Resources.Load<Sprite>(spritePath + index.ToString() + "_" + (i+1).ToString());
        }
    }

    public void Greet()
    {
        Debug.Log($"I am {Name} and I like {likedFoods[0]}, hate {hatedFoods[0]}. Affinity : {Affinity}");
    }

    public bool IsLikedFood(int index)
    {
        foreach (var food in likedFoods) if (index == food) return true;
        return false;
    }

    public bool IsHatedFood(int index)
    {
        foreach (var food in hatedFoods) if (index == food) return true;
        return false;
    }

    public bool IsRandomSoldier()
    {
        return Index == null;
    }

    public string SayHello()
    {
        return helloSpeeches[UnityEngine.Random.Range(0, helloSpeeches.Length)];
    }

    public string SayMore()
    {
        return moreSpeeches[UnityEngine.Random.Range(0, moreSpeeches.Length)];
    }
    
    public void SetAffinity() //Set Affinity and Reputation
    {

    }

}

public static class SoldierManager
{
    private const string soldierSpritePath = "Soldiers/";
    private const string textFilePath = "TextFiles/";

    public static Dictionary<string, Soldier> soldierDict;
    public static Dictionary<string, Soldier> moreSoldierDict;

    public static int soldierNum; //오늘 등장할 병사 수
    public static List<Soldier> soldiersToday = new List<Soldier>(); //오늘 등장할 병사 리스트

    static SoldierManager()
    {
        //ReadSoldiers();
        //SetMoreSoldiers();
        //SetSoldierTodayList();
        //foreach (var soldier in SoldierManager.soldierDict) soldier.Value.Greet();
    }

    public static void ReadSoldiers()
    {
        TextAsset soldierText = Resources.Load<TextAsset>(textFilePath + "Soldier");

        //Parsing
        var rows = soldierText.text.Split(new char[] { '\n' });
        for (int i = 1; i < rows.Length; i++) //Ignore first & last rows
        {
            var columns = rows[i].Split(new char[] { ',' });
            //name, realName, rank, likedFoods, hatedFoods, affinity, helloSpeeches, moreSpeeches

            var likedFoods = new List<int>();
            var hatedFoods = new List<int>();
            foreach (var likedFood in columns[3].Split(new char[] { '+' }))
            {
                int.TryParse(likedFood, out int foodIndex);
                likedFoods.Add(foodIndex);
            }
            foreach (var hatedFood in columns[4].Split(new char[] { '+' }))
            {
                int.TryParse(hatedFood, out int foodIndex);
                hatedFoods.Add(foodIndex);
            }

            var helloSpeeches = columns[6].Split(new char[] { '+' });
            var moreSpeeches = columns[7].Split(new char[] { '+' });

            if(!soldierDict.ContainsKey(columns[0]))
                soldierDict.Add(columns[0], new Soldier(i, columns[0], columns[1], columns[2], likedFoods, hatedFoods, columns[5], helloSpeeches, moreSpeeches));
        }
    }
    
    public static List<Soldier> PickSoldiers(int num)
    {
        var pickedSoldiers = new List<Soldier>();
        foreach (var value in Useful.RandomValues(soldierDict).Take(num))
        {
            if (pickedSoldiers.Contains(value))
            {
                num++;
            }
            else
            {
                pickedSoldiers.Add(value);
            }
        }
        return pickedSoldiers;
    }

    public static float GetSoldierAffinity(string name)
    {
        return soldierDict[name].Affinity;
    }

    public static float SetSoldierAffinity(string name, float affinityVariance)
    {
        return soldierDict[name].Affinity += affinityVariance;
    }

    public static void SetMoreSoldiers()
    {
        moreSoldierDict.Add("플레이어", new Soldier(0, "플레이어"));
        moreSoldierDict.Add("말년병장", new Soldier(11, "말년병장"));
        moreSoldierDict.Add("후임", new Soldier(12, "후임"));
        moreSoldierDict.Add("창식이", new Soldier(13, "창식이"));
        moreSoldierDict.Add("팅커벨", new Soldier(14, "팅커벨"));
    }

    public static void SetSoldierTodayList()
    {
        soldiersToday.Clear();
        int[] randomPerc = { 2, 3, 4 }; //Fixed : Random (ex 2:8, 3:7, 4:6)
        int todaysRandomPerc = randomPerc[UnityEngine.Random.Range(0, randomPerc.Length)];

        soldierNum = UnityEngine.Random.Range(10, 15); //Total number of soldiers
        int todaysFixedNum = soldierNum * todaysRandomPerc / 10; //Fixed Soldiers
        int todaysRandomNum = soldierNum - todaysFixedNum; //Random Soldiers

        soldiersToday.AddRange(SoldierManager.PickSoldiers(todaysFixedNum)); //Add Fixed Soldiers
        var randomSoldiers = new List<Soldier>(); //Make Random Soldiers
        for (int i = 0; i < todaysRandomNum; i++) randomSoldiers.Add(new Soldier());
        soldiersToday.AddRange(randomSoldiers); //Add Random Soldiers
        Useful.Shuffle(soldiersToday); //Shuffle List

        soldierNum = todaysFixedNum + todaysRandomNum;

        var imsiString = "";
        foreach (var s in soldiersToday) imsiString += (((s.Name == null) ? "R" : s.Name) + s.Rank + " / ");
        Debug.Log("고정군인 : " + todaysFixedNum + " 랜덤군인 : " + todaysRandomNum + "||||||| " + imsiString);
    }
}

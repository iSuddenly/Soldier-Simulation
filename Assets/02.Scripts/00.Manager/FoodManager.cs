using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    public int Category { get; private set; } //1=밥, 2=국, 3=메인반찬, 4=서브반찬, 5=섭섭(김치)
    public int SpriteID { get; private set; }
    public string Name { get; private set; }
    public float SpriteSize { get; private set; }
    public float Amount { get; private set; }
    public bool IsRotate { get; private set; }

    private string foodPath = "Food/";

    public Food(int category, int spriteID, string name, float spriteSize, float amount, bool isRotate)
    {
        this.Category = category;
        this.SpriteID = spriteID;
        this.Name = name;
        this.SpriteSize = spriteSize;
        this.Amount = amount;
        this.IsRotate = isRotate;
    }

    public string Greet()
    {
        return ($"{Name}, SpriteID {SpriteID}, SpriteSize {SpriteSize}. Amount : {Amount}, Rotate : {IsRotate}");
    }

    public Sprite GetSprite()
    {
        if(this.Category >= 3) return Resources.Load<Sprite>(foodPath + SpriteID);
        else return Resources.Load<Sprite>(foodPath + SpriteID + "_T1");
    }

    public Sprite GetSprite(bool isSikpan, int num) //Rice, Soup
    {
        if (this.Category >= 3) return null;
        if(isSikpan) return Resources.Load<Sprite>(foodPath + SpriteID.ToString() + "_S" + num.ToString());
        else return Resources.Load<Sprite>(foodPath + SpriteID.ToString() + "_T" + num.ToString());
    }
}
/// <summary>
/// Parse Food CSVs
/// </summary>
public static class FoodManager
{
    private static string textFilePath = "TextFiles/";

    public static Dictionary<int, List<Food>> foodDict;

    public static Food[] todayFoods;
    
    static FoodManager()
    {
        //ReadFoods();
        //SetTodaysMenu();
        //foreach (var food in foodDict[1]) print(food.Greet());
    }

    public static void ReadFoods()
    {
        TextAsset foodText = Resources.Load<TextAsset>(textFilePath + "Food");

        //Parsing
        var rows = foodText.text.Split(new char[] { '\n' });
        for (int i = 1; i < rows.Length; i++) //Ignore first & last rows
        {
            var columns = rows[i].Split(new char[] { ',' });
            //category, name, spriteID, spriteSize, amount, isRotate
            int.TryParse(columns[0], out int category);
            int.TryParse(columns[2], out int spriteID);
            float.TryParse(columns[3], out float spriteSize);
            float.TryParse(columns[4], out float amount);

            bool isRotate = true;
            if (columns[5] == "X" || columns[5] == "x") isRotate = false;

            //int spriteID, string name, float spriteSize, float amount, bool isRotate
            if (foodDict.ContainsKey(category))
                foodDict[category].Add(new Food(category, spriteID, columns[1], spriteSize, amount, isRotate));
            else
                foodDict[category] = new List<Food> { new Food(category, spriteID, columns[1], spriteSize, amount, isRotate) };
        }
    }

    private static (Food, Food, Food, Food, Food) PickRandomMenu()
    {
        Food rice = foodDict[1][Random.Range(0, foodDict[1].Count - 1)];
        Food soup = foodDict[2][Random.Range(0, foodDict[2].Count - 1)];
        Food b1 = foodDict[3][Random.Range(0, foodDict[3].Count - 1)];
        Food b2 = foodDict[4][Random.Range(0, foodDict[4].Count - 1)];
        Food b3 = foodDict[5][Random.Range(0, foodDict[5].Count - 1)];
        Debug.Log($"Today's Random Menu is  {rice.Name},  {soup.Name}, {b1.Name}, {b2.Name}, {b3.Name}");

        return (rice, soup, b1, b2, b3);
    }

    public static void SetTodaysMenu()
    {
        var foods = PickRandomMenu();
        todayFoods = new Food[5] { foods.Item1, foods.Item2, foods.Item3, foods.Item4, foods.Item5 };
    }

}
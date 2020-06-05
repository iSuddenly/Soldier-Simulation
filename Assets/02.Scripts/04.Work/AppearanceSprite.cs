using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceSprite : MonoBehaviour //나중에 SpriteBundle 상속
{
    public static AppearanceSprite Instance;

    private string bodyPath = "Soldiers/Body/";
    private string eyesPath = "Soldiers/Eyes/";
    private string nosePath = "Soldiers/Nose/";
    private string mouthPath = "Soldiers/Mouth/";
    

    public Sprite[] bodyFactories = new Sprite[3]; //몸 3종류, 코와 연결
    public Sprite[,] eyesFactories = new Sprite[7,2]; //눈 7종류, 상태 2개씩
    public Sprite[,] noseFactories = new Sprite[3,5]; //코 3종류, 상태 5개씩, 몸과 연결
    public Sprite[,] mouthFactories = new Sprite[2,5]; //입 2종류, 상태 5개씩

    private void Awake()
    {
        if (Instance == null) Instance = this;
        try
        {
            if (WinterManager.Instance.isWinter)
            {
                bodyPath = "Soldiers/Body/Winter/";
                nosePath = "Soldiers/Nose/Winter/";
            }
        }
        catch
        {

        }

    }

    void Start()
    {
        for (int i = 0; i < bodyFactories.Length; i++)
            bodyFactories[i] = Resources.Load<Sprite>(bodyPath + (i + 1).ToString());
        
        PutSpriteToArray(eyesFactories, eyesPath);
        PutSpriteToArray(noseFactories, nosePath);
        PutSpriteToArray(mouthFactories, mouthPath);
        
    }

    private void PutSpriteToArray(Sprite[,] spriteArray, string path) //일차배열까지 포괄가능하도록?
    {
        for (int i = 0; i < spriteArray.GetLength(0); i++)
            for (int j = 0; j < spriteArray.GetLength(1); j++)
                spriteArray[i, j] = Resources.Load<Sprite>(path + (i + 1).ToString() + "-" + (j + 1).ToString());
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// Change apperance of soldiers
/// </summary>
public class RandomAppearance : MonoBehaviour
{
    public SpriteRenderer bodyHouse, eyesHouse, mouthHouse, noseHouse;
    public SpriteRenderer shadow;
    public GameObject randomFace;
    public SpriteRenderer fixedFace;
    public SpriteRenderer insignia;
    public SpriteRenderer floatingInsignia;

    int bodyId, eyesId, mouthId;

    private int bodyAmount = 3;
    private int eyeAmount = 7;
    private int mouthAmount = 2;

    public Sprite[] bodiesFactory; //몸 3종류, 코와 연결
    public Sprite[] eyesFactory = new Sprite[2]; //눈 7종류, 상태 2개씩
    public Sprite[] mouthsFactory = new Sprite[5]; //입 2종류, 상태 5개씩
    public Sprite[] nosesFactory = new Sprite[5]; //코 3종류, 상태 5개씩, 몸과 연결
    public Sprite[] insigniaFactory = new Sprite[4]; //계급장 스프라이트

    private Vector2[] facePos = new Vector2[3]; //Position of eyes and nose based on soldier height
    private Vector2[] insigniaPos = new Vector2[3]; //Position of Insignia based on soldier height

    public bool isRandom;

    void Start()
    {
        isRandom = SoldierManager.soldiersToday[SceneManager04.Instance.iterationNum].IsRandomSoldier();
        StartColor();
        floatingInsignia.sprite = Resources.Load<Sprite>("Insignia/" + SoldierManager.soldiersToday[SceneManager04.Instance.iterationNum].Rank.ToString());

        if (!isRandom) //고정군인이면
        {
            bodyHouse.sprite = SoldierManager.soldiersToday[SceneManager04.Instance.iterationNum].Picture;
            randomFace.SetActive(false);
            fixedFace.gameObject.SetActive(true);
            noseHouse.gameObject.SetActive(false); //코 비활성화
            insignia.sprite = null;
            SpriteAppearance(0, 0, 0, true);
        }
        else //랜덤군인이면
        {
            fixedFace.gameObject.SetActive(false);
            var pa = PickAppearance();
            //SpriteAppearance(pa.Item2, pa.Item1, pa.Item3, true);
            SpriteAppearance(0, 0, 0, true);
            facePos = new Vector2[] { new Vector2(0f, 0f), new Vector2(0.14f, -0.68f), new Vector2(0, 1.42f), };
            randomFace.transform.localPosition = facePos[pa.Item1];

            insigniaPos = new Vector2[] { new Vector2(1.77f, -1.56f), new Vector2(1.58f, -0.20f), new Vector2(1.26f, -1.6f), };
            insignia.transform.localPosition = insigniaPos[pa.Item1];
            insignia.sprite = insigniaFactory[SoldierManager.soldiersToday[SceneManager04.Instance.iterationNum].Rank-1];
        }
    }

    public (int, int, int) PickAppearance()
    {
        bodyId = UnityEngine.Random.Range(0, bodyAmount);
        eyesId = UnityEngine.Random.Range(0, eyeAmount);
        mouthId = UnityEngine.Random.Range(0, mouthAmount);

        return (bodyId, eyesId, mouthId);
    }

    public void SpriteAppearance(int eyeState, int bodyState, int mouthState, bool isFirst = false)
    {
        if (!isRandom)
        {
            if (isFirst) bodyHouse.sprite = SoldierManager.soldiersToday[SceneManager04.Instance.iterationNum].Picture;
            fixedFace.sprite = SoldierManager.soldiersToday[SceneManager04.Instance.iterationNum].facePictures[mouthState];
        }
        else
        {
            if (isFirst) bodyHouse.sprite = AppearanceSprite.Instance.bodyFactories[bodyId];
            eyesHouse.sprite = AppearanceSprite.Instance.eyesFactories[eyesId, eyeState];
            noseHouse.sprite = AppearanceSprite.Instance.noseFactories[bodyId, bodyState];
            mouthHouse.sprite = AppearanceSprite.Instance.mouthFactories[mouthId, mouthState];
        }
    }
    
    public void LookAngry(int angerness)
    {
        switch (angerness) {
            case 0:
                StartCoroutine(ShakeInAnger(0.2f));
                SpriteAppearance(0, 0, 0);
                break;
            case 1:
                StartCoroutine(ShakeInAnger(0.2f));
                SpriteAppearance(1, 1, 1);
                break;
            case 2:
                StartCoroutine(ShakeInAnger(0.3f));
                SpriteAppearance(1, 2, 2);
                break;
            case 3:
                StartCoroutine(ShakeInAnger(0.3f));
                SpriteAppearance(1, 3, 3);
                break;
            case 4:
                StartCoroutine(ShakeInAnger(0.4f));
                SpriteAppearance(1, 4, 4);
                break;
            default:
                StartCoroutine(ShakeInAnger(0.5f));
                break;
        }
        byte value = Convert.ToByte(160 - (angerness * 20));
        eyesHouse.color = noseHouse.color = mouthHouse.color = fixedFace.color = new Color32(255, value, value, 255);
    }

    private IEnumerator ShakeInAnger(float sec)
    {
        if (!GetComponent<SoldierMove>().aboutTodie)
        {
            float currentTime = 0f;
            Vector3 origin = transform.position;
            while (currentTime < sec)
            {
                transform.position = origin + (UnityEngine.Random.onUnitSphere * 0.1f);
                currentTime += Time.deltaTime;
                yield return new WaitForSeconds(0.001f);
            }
            yield return null;
        }
    }

    Color32 blank = new Color(255, 255, 255, 0);
    

    public IEnumerator Show()
    {
        StartColor();
        float lerpDuration = 0.5f;
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            bodyHouse.color = eyesHouse.color = mouthHouse.color = noseHouse.color = insignia.color = fixedFace.color = floatingInsignia.color = Color32.Lerp(blank, Color.white, currentLerpTime / lerpDuration);
            shadow.color = Color32.Lerp(Color.white, blank, currentLerpTime / lerpDuration);
            shadow.gameObject.transform.localScale = Vector3.Lerp(new Vector3(1.15f,1.15f,1.15f), new Vector3(1.4f, 1.4f, 1.4f), currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;

        SceneManager04.Instance.SayHello();
        yield return new WaitForSeconds(.75f);
        BaechicButton.Instance.MoveSikpan();
    }

    private void StartColor() //시작할 때 그림자만 보이도록.
    {
        bodyHouse.color = eyesHouse.color = mouthHouse.color = noseHouse.color = fixedFace.color = insignia.color = floatingInsignia.color = blank;
        shadow.color = Color.white;
    }

    public IEnumerator Hide()
    {
        Color32 midColor = fixedFace.color;

        float lerpDuration = 0.4f;
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            eyesHouse.color = mouthHouse.color = noseHouse.color = fixedFace.color = Color32.Lerp(midColor, blank, currentLerpTime / lerpDuration);
            bodyHouse.color = insignia.color = floatingInsignia.color = Color32.Lerp(Color.white, blank, currentLerpTime / lerpDuration);
            shadow.color = Color32.Lerp(blank, Color.white, currentLerpTime / lerpDuration);
            bodyHouse.gameObject.transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(0.7f, 0.7f, 0.7f), currentLerpTime / lerpDuration);
            shadow.gameObject.transform.localScale = Vector3.Lerp(new Vector3(1.4f, 1.4f, 1.4f), new Vector3(1.15f, 1.15f, 1.15f), currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        StartColor();
    }

}
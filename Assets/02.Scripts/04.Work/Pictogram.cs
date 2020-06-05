using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pictogram : MonoBehaviour //GameObject상속?
{
    public bool nextTrigger;

    public Sprite[] sprite;
    public Sprite[] leaveSprite;

    float currentTime = 0f;

    Transform[] spots = new Transform[7];
    public int current;

    float timeLimit = 240f;
    public bool nerveGotOff;


    private void Start()
    {
        spots[0] = GameObject.Find("BornSpot").transform;
        spots[1] = GameObject.Find("Spot (1)").transform;
        spots[2] = GameObject.Find("Spot (2)").transform;
        spots[3] = GameObject.Find("Spot (3)").transform;
        spots[4] = GameObject.Find("Spot (4)").transform;
        spots[5] = GameObject.Find("Spot (5)").transform;
        spots[6] = GameObject.Find("DeathSpot").transform;
        //최악. 나중에 수정

        //StartCoroutine(WaitUntilAngry());

    }

    private void Update()
    {
        if (nextTrigger)
        {
            StartCoroutine("Move");
            nextTrigger = false;
        }

        currentTime += Time.deltaTime;
        if (currentTime >= timeLimit)
        {
            nerveGotOff = true;
        }

        if (transform.position.x <= spots[5].position.x)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Move() {
        float delay = Random.Range(0.0f, 0.25f);
        yield return new WaitForSeconds(delay); //약간의 랜덤한 딜레이
        StartCoroutine("Change");
        float lerpTime = Random.Range(2f, 3f); //속도도 약간 랜덤
        float currentLerpTime = 0f;
        
        while(currentLerpTime < lerpTime)
        {
            float perc = currentLerpTime / lerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            currentLerpTime += Time.deltaTime;
            transform.position = Vector3.Lerp(spots[current].position, spots[current+1].position, perc);
            yield return new WaitForSeconds(0.01f);
        }
        current += 1;
        if (current >= 6)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Change()
    {
        for (int i = 0; i < 2; i++) //애니메이션 2회 반복
        {
            for (int j = 0; j < 3; j++) //스프라이트 3장
            {
                GetComponent<Image>().sprite = sprite[j];
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    IEnumerator WaitUntilAngry()
    {
        yield return new WaitUntil(() => nerveGotOff);
        //GetComponent<Image>().material.SetFloat() ;
        GetComponent<Image>().color = new Color32(255, 100, 100, 255);
        StartCoroutine(GetOut());
        while (transform.position.x <= spots[0].transform.position.x) {
            transform.Translate(Vector3.right * Time.deltaTime * 2f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }

    IEnumerator GetOut()
    {
        for (int i = 0; i < 5; i++) //애니메이션 5회 반복
        {
            for (int j = 0; j < 3; j++) //스프라이트 3장
            {
                GetComponent<Image>().sprite = leaveSprite[j];
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    IEnumerator MoveOn()
    {
        yield return null;
    }
}

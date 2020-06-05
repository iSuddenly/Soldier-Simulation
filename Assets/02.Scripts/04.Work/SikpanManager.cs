using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SikpanManager : MonoBehaviour //식판 관리, 배식통의 음식 스프라이트 관리.
{
    public static SikpanManager Instance;

    [Header("House")]
    public SpriteRenderer riceHouse;
    public SpriteRenderer gookHouse;
    public GameObject[] banchanHouse = new GameObject[3];

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }



    public void PourRice()
    {
        switch (SceneManager04.Instance.currentSoldier.eachFulfill[0]) //식판 스프라이트 수정
        {
            case 1:
                riceHouse.sprite = SceneManager04.Instance.riceSprites[0];
                break;
            case 2:
                riceHouse.sprite = SceneManager04.Instance.riceSprites[1];
                break;
            case 3:
                riceHouse.sprite = SceneManager04.Instance.riceSprites[2];
                break;
            default:
                break;
        }
        //배식통 스프라이트
        //솔져 상태 스크립트는 다른곳에서.
    }

    public void PourGook()
    {
        switch (SceneManager04.Instance.currentSoldier.eachFulfill[1]) //식판 스프라이트 수정
        {
            case 1:
                gookHouse.sprite = SceneManager04.Instance.gookSprites[0];
                break;
            case 2:
                gookHouse.sprite = SceneManager04.Instance.gookSprites[1];
                break;
            case 3:
                gookHouse.sprite = SceneManager04.Instance.gookSprites[2];
                break;
            default:
                break;
        }
    }

    public void PourBanchan(GameObject go, int i)
    {
        go.transform.parent = banchanHouse[i].transform;
        go.transform.position = RandomPointInSikpan(i);
        switch (go.tag)
        {
            case "Banchan1":
                ClickManager.Instance.FillFood(2);
                break;
            case "Banchan2":
                ClickManager.Instance.FillFood(3);
                break;
            case "Banchan3":
                ClickManager.Instance.FillFood(4);
                break;
        }

        //각자 위치 지정, 박스 안에 어느정도 랜덤한 포인트. 이 함수 다시만들기. 편집 용이하도록
        //랜덤하게 딱 붙도록
    }

    Vector2 RandomPointInSikpan(int which)
    {
        Vector2 point = transform.position;
        switch (which)
        {
            case 0:
                point = new Vector2(Random.Range(banchanHouse[0].transform.position.x + 0.2f, banchanHouse[0].transform.position.x - 0.2f), Random.Range(banchanHouse[0].transform.position.y + 0.2f, banchanHouse[0].transform.position.y - 0.2f));
                break;
            case 1:
                point = new Vector2(Random.Range(banchanHouse[1].transform.position.x + 0.2f, banchanHouse[1].transform.position.x - 0.2f), Random.Range(banchanHouse[1].transform.position.y + 0.2f, banchanHouse[1].transform.position.y - 0.2f));
                break;
            case 2:
                point = new Vector2(Random.Range(banchanHouse[2].transform.position.x + 0.2f, banchanHouse[2].transform.position.x - 0.2f), Random.Range(banchanHouse[2].transform.position.y + 0.2f, banchanHouse[2].transform.position.y - 0.2f));
                break;
        }
        return point;
    }


    public void EmptySikpan()
    {
        for(int i = 0; i < banchanHouse.Length; i++)
        {
            foreach(Transform child in banchanHouse[i].transform)
            {
                Destroy(child.gameObject);
            }
        }
        riceHouse.sprite = null;
        gookHouse.sprite = null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Soldiers in Main Game
/// </summary>
public class SoldierMove : MonoBehaviour
{
    public enum State { Born, InProgress, AboutToDie };
    public State state;

    public Transform spawnPoint;
    public Transform stopPoint;
    public Transform deathPoint;

    private float speed = 6f;

    public int[] eachFulfill = new int[5]; //음식 받을만큼 받았는지. 0, 1, 2... 2 이상이면 많이 준 것으로 간주
    public bool fulfilled;

    public int angerness; //화. 총 5단계, 커질수록 빡침

    private IEnumerator bounceCoroutine;

    public bool aboutTodie = false;

    public GameObject fixedFace;

    void Start()
    {
        bounceCoroutine = Bounce();
        state = State.Born;
        spawnPoint = GameObject.Find("Spawn_Soldier").transform;
        stopPoint = GameObject.Find("StopPoint").transform;
        deathPoint = GameObject.Find("Spawn_Cook").transform;

        transform.position = spawnPoint.position;
        StartCoroutine(bounceCoroutine);
    }
    
    void Update()
    {
        switch (state)
        {
            case State.Born:
                UpdateBorn();
                break;
            case State.InProgress:
                UpdateInProgress();
                break;
            case State.AboutToDie:
                UpdateAboutToDie();
                break;
        }
    }

    void UpdateBorn()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > stopPoint.transform.position.x)
        {
            state = State.InProgress;
            StartCoroutine(GetComponent<RandomAppearance>().Show());
            SceneManager04.Instance.currentSoldier = GetComponent<SoldierMove>();
            StopCoroutine(bounceCoroutine);
            BaechicButton.Instance.DoneButtonActiveness();
        }
    }

    void UpdateInProgress()
    {
        if (fulfilled)
        {
            StartCoroutine(bounceCoroutine);
            state = State.AboutToDie;
        }
    }

    void UpdateAboutToDie()
    {
        BaechicButton.Instance.DoneButtonActiveness();
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > deathPoint.position.x) Destroy(gameObject);
        //저장 혹은 기억 작업
    }

    IEnumerator Bounce()
    {
        float t = 0.6f;
        while (true)
        {
            if (t > 0.4f)
                transform.Translate(Vector2.up * 1f * Time.deltaTime);
            else if (t > 0.2f)
                transform.Translate(Vector2.down * 1f * Time.deltaTime);
            else if (t <= 0f)
                t += 0.6f;
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Breath()
    {
        yield return new WaitForEndOfFrame();
    }

    public void CheckFulfilled() //배식에 만족했는지 체크
    {
        for(int i = 0; i<eachFulfill.Length; i++)
        {
            if (eachFulfill[i] <= 0)
            {
                return;
            }
        }
        fulfilled = true;

        //이게 Fulfilled 단일변수랑 나뉘어 있는게 바람직한가?
    }


    public void MakeSoldierDie()
    {
        StartCoroutine(GetComponent<RandomAppearance>().Hide());
        state = State.AboutToDie;
        aboutTodie = true;
        StartCoroutine(bounceCoroutine);
    }

    public void GetAngry()
    {
        SceneManager04.Instance.currentSoldier.GetComponent<RandomAppearance>().LookAngry(++angerness);
    }

}


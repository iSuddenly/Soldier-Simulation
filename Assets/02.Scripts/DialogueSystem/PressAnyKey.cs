using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject pak;

    void Awake()
    {
        pak = gameObject;
        StartCoroutine("PressAnyKeyFunc");
        StartCoroutine("Blink");
    }

    IEnumerator PressAnyKeyFunc()
    {
        yield return new WaitUntil(() => Input.anyKeyDown); //코루틴은 부른 곳에서 끝난다. 불린 곳이 종료되어도 부른 Monobehavior가 안끝나면 안끝난다고 한다.
        RoutineManager.MoveScene();
        print("Moving to Next Scene...");
    }


    IEnumerator Blink()
    {
        //코루틴 간단하게 하나로 합칠 수 없을까. 굳이 두개 쓰기 좀 그렇다
        while (!Input.anyKeyDown)
        {
            pak.SetActive(!pak.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

}

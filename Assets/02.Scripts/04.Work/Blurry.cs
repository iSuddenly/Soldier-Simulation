using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blurry : MonoBehaviour
{
    /// <summary>
    /// 일정 체력 이하일 시 화면 뿌옇게
    /// </summary>

    Material mat;

    float lerpTime = 2f;
    float currentLerpTime = 0f;

    void Start()
    {
        mat = GetComponent<Image>().material;
        if(StatusInGame.Instance.health <= 20) StartCoroutine(PingPongBlur());
    }
    
    IEnumerator PingPongBlur()
    {
        float perc;
        while (true)
        {
            perc = currentLerpTime / lerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);

            if (currentLerpTime <= lerpTime / 2)
            {
                float radius = Mathf.Lerp(1f, 7f, perc);
                mat.SetFloat("_Radius", radius);
            }
            else if (currentLerpTime <= lerpTime)
            {
                float radius = Mathf.Lerp(7f, 1f, perc);
                mat.SetFloat("_Radius", radius);
            }
            else if(currentLerpTime > lerpTime)
            {
                currentLerpTime = 0;
            }
            currentLerpTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

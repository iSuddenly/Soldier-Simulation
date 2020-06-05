using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text text;
    public GameObject over;

    private string endString = "그리하여 나는 체력이 바닥난 채로 영창에 가게 되었다. 나에게 다시 한번의 기회가 있다면...";
    
    void Start()
    {
        StartCoroutine(Flow());
    }
    
    IEnumerator Flow()
    {
        over.SetActive(false);
        yield return new WaitForSeconds(2f);
        over.SetActive(true);
        float currentLerpTime = 0f;
        var startScale = new Vector3(4f, 4f, 4f);
        var endScale = Vector3.one;
        while (currentLerpTime <= 0.5f)
        {
            var perc = currentLerpTime / 0.5f;
            perc = perc * perc * (perc * (6f * perc - 15f) + 10f);
            over.GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, endScale, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        StartCoroutine(Blink());

        for (int i = 0; i < endString.Length; i++)
        {
            text.text += endString[i];
            yield return new WaitForSeconds(0.02f);
        }
        

        yield return new WaitUntil(() => Input.anyKeyDown);
        RoutineManager.MoveScene(0);
    }

    IEnumerator Blink()
    {
        float currentLerpTime = 0f;
        var lerpDuration = 0.4f;
        
        Color32 startColor = Color.white;
        Color32 endColor = new Color32(200, 200, 200, 255);
        
        while (true)
        {
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                over.GetComponent<Image>().color = Color.Lerp(startColor, endColor, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                over.GetComponent<Image>().color = Color.Lerp(endColor, startColor, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
        }
    }

}

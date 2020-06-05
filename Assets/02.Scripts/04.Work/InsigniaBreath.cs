using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsigniaBreath : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Breath());
    }

    IEnumerator Breath()
    {
        var lerpDuration = 0.5f;
        float currentLerpTime = 0f;
        var startPos = transform.position;
        var endPos = transform.position + new Vector3(0, -0.3f, 0);
        while (true)
        {
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                transform.position = Vector3.Lerp(startPos, endPos, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                transform.position = Vector3.Lerp(endPos, startPos, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
        }
    }
}

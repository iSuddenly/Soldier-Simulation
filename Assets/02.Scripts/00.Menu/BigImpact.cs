using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Opening Animation!
/// </summary>
public class BigImpact : MonoBehaviour
{
    public GameObject blueBG;
    public GameObject jogi;
    public GameObject forkSpoon;
    public GameObject mainLogo;
    public GameObject soldiers;
    public GameObject pressAnyKey;

    private void Start()
    {
        mainLogo.GetComponent<RectTransform>().localScale = Vector3.one * 0.01f;
        pressAnyKey.SetActive(false);
    }
    
    private void OnEnable()
    {
        StartCoroutine(Flow());
    }

    private IEnumerator Flow()
    {
        print("Start Flow");
        StartCoroutine(ForkSpoonMove());
        StartCoroutine(SoldiersMove());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(JogiMove());
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(MainLogoMove());
        yield return new WaitForSeconds(1f);
        StartCoroutine(PressAnyKey());
        yield return new WaitUntil(() => Input.anyKeyDown);
        gameObject.SetActive(false);
    }

    private IEnumerator JogiBreath()
    {
        float currentLerpTime = 0f;
        var lerpDuration = 0.9f;

        var startPos = new Vector3(0, -0.3f, 0f);
        var endPos = Vector3.zero;
        while (true)
        {
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                jogi.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, endPos, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                jogi.GetComponent<RectTransform>().position = Vector3.Lerp(endPos, startPos, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
        }

    }

    private IEnumerator ForkSpoonBreath()
    {
        float currentLerpTime = 0f;
        var lerpDuration = 0.7f;

        var startScale = new Vector3(1.02f, 1.02f, 1.02f);
        var endScale = Vector3.one;
        while (true)
        {
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                forkSpoon.GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, endScale, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                forkSpoon.GetComponent<RectTransform>().localScale = Vector3.Lerp(endScale, startScale, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
        }
    }

    private IEnumerator SoldiersBreath()
    {
        float currentLerpTime = 0f;
        var lerpDuration = 0.7f;

        var startPos = Vector3.zero;
        var endPos = new Vector3(0f, -0.1f, 0f);
        while (true)
        {
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                soldiers.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, endPos, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                soldiers.GetComponent<RectTransform>().position = Vector3.Lerp(endPos, startPos, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
        }

    }

    private IEnumerator MainLogoBreath()
    {
        float currentLerpTime = 0f;
        var lerpDuration = 0.5f;

        var startScale = Vector3.one; 
        var endScale = new Vector3(0.95f, 0.95f, 0.95f);
        while (true)
        {
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                mainLogo.GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, endScale, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
            while (currentLerpTime <= lerpDuration)
            {
                var perc = currentLerpTime / lerpDuration;
                perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
                mainLogo.GetComponent<RectTransform>().localScale = Vector3.Lerp(endScale, startScale, perc);
                currentLerpTime += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smoothness
            }
            currentLerpTime = 0f;
        }
    }

    private IEnumerator PressAnyKey()
    {
        while (true)
        {
            pressAnyKey.SetActive(!pressAnyKey.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator JogiMove()
    {
        var lerpDuration = 0.5f;
        float currentLerpTime = 0f;
        var startPos = new Vector3(0, -8f, 0);
        var midPos = new Vector3(0, 0.1f, 0);
        var endPos = new Vector3(0, -0.3f, 0);
        while (currentLerpTime <= lerpDuration)
        {
            var perc = currentLerpTime / lerpDuration;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            jogi.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, midPos, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        while (currentLerpTime <=0.4f)
        {
            var perc = currentLerpTime / 0.4f;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            jogi.GetComponent<RectTransform>().position = Vector3.Lerp(midPos, endPos, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        StartCoroutine(JogiBreath());
    }

    private IEnumerator ForkSpoonMove()
    {
        var lerpDuration = 0.5f;
        float currentLerpTime = 0f;
        var startScale = new Vector3(1.6f, 1.6f, 1.6f);
        var midScale = Vector3.one;
        var endScale = new Vector3(1.02f, 1.02f, 1.02f);
        while (currentLerpTime <= lerpDuration)
        {
            var perc = currentLerpTime / lerpDuration;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            forkSpoon.GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, midScale, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        while (currentLerpTime <= 0.15f)
        {
            var perc = currentLerpTime / 0.15f;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            forkSpoon.GetComponent<RectTransform>().localScale = Vector3.Lerp(midScale, endScale, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        StartCoroutine(ForkSpoonBreath());
    }

    private IEnumerator MainLogoMove()
    {
        float currentLerpTime = 0f;
        var startScale = new Vector3(0.01f, 0.01f, 0.01f);
        var midScale = new Vector3(1.1f, 1.1f, 1.1f);
        var endScale = Vector3.one;
        while (currentLerpTime <= 0.5f)
        {
            var perc = currentLerpTime / 0.5f;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            mainLogo.GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, midScale, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        while (currentLerpTime <= 0.3f)
        {
            var perc = currentLerpTime / 0.3f;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            mainLogo.GetComponent<RectTransform>().localScale = Vector3.Lerp(midScale, endScale, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }

        StartCoroutine(MainLogoBreath());
    }

    private IEnumerator SoldiersMove()
    {
        var lerpDuration = 0.7f;
        float currentLerpTime = 0f;
        var startPos = new Vector3(0, -10, 0);
        var endPos = Vector3.zero;
        var startScale = new Vector3(1.3f, 1.3f, 1.3f);
        var endScale = Vector3.one;
        while (currentLerpTime <= lerpDuration)
        {
            var perc = currentLerpTime / lerpDuration;
            perc = perc * perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            soldiers.GetComponent<RectTransform>().localScale = Vector3.Lerp(startScale, endScale, perc);
            soldiers.GetComponent<RectTransform>().position = Vector3.Lerp(startPos, endPos, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;

        StartCoroutine(SoldiersBreath());
    }
    
}

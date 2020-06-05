using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    private Text speechText;
    public string targetString;
    private string currentString = "";
    private float delayBtwText = 0.04f; //Delay of typing effect
    public bool isBubbleDone = false; //Is speech finished

    public bool isFromLeft;
    private float lerpDuration = 0.5f;
    private Image image;
    private RectTransform rectTrans;

    private Color32 blankColor = new Color32(188, 188, 188, 0);
    private Color32 clearColor = Color.white;
    private Vector3 originalPos;
    private Vector3 spawnPos;

    void Start()
    {
        speechText = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();

        originalPos = GetComponent<RectTransform>().position;

        StartCoroutine(ShowSpeech());
        StartCoroutine(SlightMove(isFromLeft));
    }

    private IEnumerator ShowSpeech()
    {
        for(int i = 0; i < targetString.Length; i++)
        {
            currentString += targetString[i];
            speechText.text = currentString;
            yield return new WaitForSeconds(delayBtwText);
        }
        isBubbleDone = true;
    }

    private IEnumerator SlightMove(bool isFromLeft)
    {
        if (isFromLeft) spawnPos = originalPos + new Vector3(-1, 0, 0);
        else spawnPos = originalPos + new Vector3(1, 0, 0);

        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            image.color = Color32.Lerp(blankColor, clearColor, currentLerpTime / lerpDuration);
            rectTrans.position = Vector3.Lerp(spawnPos, originalPos, currentLerpTime / lerpDuration);
            currentLerpTime += 0.03f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
    }

}
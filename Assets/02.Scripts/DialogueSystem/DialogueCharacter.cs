using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Handles Animation of Character Image in Dialogue Frame
/// </summary>
public class DialogueCharacter : MonoBehaviour
{
    public static DialogueCharacter Instance;

    //나중에 Position과 Color 묶어 처리
    private Image image;
    private RectTransform rectTransform;

    private float lerpDuration = 0.1f;

    private Vector3 originalScale = Vector3.one;
    private Vector3 dimScale = new Vector3(0.95f, 0.95f, 0.95f);
    private Vector3 shrinkScale = new Vector3(0.8f, 0.8f, 0.8f);

    private Vector3 originalPos = Vector3.zero;
    private Vector3 dimPos = new Vector3(0, -0.25f, 0);
    private Vector3 shrinkPos = new Vector3(0, -1f, 0);

    private Color32 blankColor = new Color32(188, 188, 188, 0);
    private Color32 clearColor = Color.white;
    private Color32 dimColor = new Color32(188, 188, 188, 255);

    private void Awake()
    {
        if (Instance == null) Instance = this;
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    private void OnEnable()
    {
        StartCoroutine(Entry());
    }

    public IEnumerator Entry() //등장
    {
        StartCoroutine(DialogueBox.Instance.Entry());

        image.color = dimColor;
        rectTransform.position = shrinkPos;
        rectTransform.localScale = shrinkScale;

        float currentLerpTime = 0f;
        while(currentLerpTime <= lerpDuration)
        {
            image.color = Color32.Lerp(blankColor, clearColor, currentLerpTime / lerpDuration);
            rectTransform.position = Vector3.Lerp(shrinkPos, originalPos, currentLerpTime / lerpDuration);
            rectTransform.localScale = Vector3.Lerp(shrinkScale, originalScale, currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
    }

    
    public IEnumerator Exit() //퇴장
    {
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            image.color = Color32.Lerp(clearColor, blankColor, currentLerpTime / lerpDuration);
            rectTransform.position = Vector3.Lerp(originalPos, shrinkPos, currentLerpTime / lerpDuration);
            rectTransform.localScale = Vector3.Lerp(originalScale, shrinkScale, currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        DialogueFrame.Instance.ActivateDialogueFrame(false); //나중에 빼고싶다.
    }

    
    public IEnumerator Behind() //살짝 뒤로
    {
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            image.color = Color32.Lerp(clearColor, dimColor, currentLerpTime / lerpDuration);
            rectTransform.position = Vector3.Lerp(originalPos, dimPos, currentLerpTime / lerpDuration);
            rectTransform.localScale = Vector3.Lerp(originalScale, dimScale, currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
    }

    
    public IEnumerator Front() //살짝 앞으로
    {
        Color32 currentColor = image.color;
        Vector3 currentPos = rectTransform.position;
        Vector3 currentScale = rectTransform.localScale;
        
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            image.color = Color32.Lerp(currentColor, clearColor, currentLerpTime / lerpDuration);
            rectTransform.position = Vector3.Lerp(currentPos, originalPos, currentLerpTime / lerpDuration);
            rectTransform.localScale = Vector3.Lerp(currentScale, originalScale, currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
    }
    
    //여러 코루틴이 동시에 불리지 않도록 해야하지 않을까?
    //시작이랑 끝부분에 정확한 결과를 적어줘야할듯
    //currentLerpTime이 글로벌로 빠지는게 좋을까? 어차피 여러개 동시에 실행되면 안되잖아

}

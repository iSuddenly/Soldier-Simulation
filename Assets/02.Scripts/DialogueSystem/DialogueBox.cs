using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Handles Animation of Character Image in DialogueFrame
/// </summary>
public class DialogueBox : MonoBehaviour
{
    public static DialogueBox Instance;

    public class BoxInfo
    {
        public Vector3 pos;
        public Vector3 scale;
        public Color32 speechColor;
        public Color32 speakerColor;

        public BoxInfo(Vector3 pos, Vector3 scale, Color32 speechColor, Color32 speakerColor)
        {
            this.pos = pos;
            this.scale = scale;
            this.speechColor = speechColor;
            this.speakerColor = speakerColor;
        }
    }

    private BoxInfo BoxInfoLerp(BoxInfo a, BoxInfo b, float f)
    {
        return new BoxInfo(Vector3.Lerp(a.pos, b.pos, f), Vector3.Lerp(a.scale, b.scale, f), Color32.Lerp(a.speechColor, b.speechColor, f), Color32.Lerp(a.speakerColor, b.speakerColor, f));
    }

    private Image speechImage;
    private Image speakerImage;
    private RectTransform rectTransform;

    private float lerpDuration = 0.1f;

    private BoxInfo originalBoxInfo = new BoxInfo(Vector3.zero, Vector3.one, Color.white, Color.white);
    private BoxInfo shrinkBoxInfo = new BoxInfo(new Vector3(0, -0.5f, 0), new Vector3(1f, 1f, 1f), new Color32(255, 255, 255, 200), new Color32(255, 255, 255, 200));

    private void Awake()
    {
        if (Instance == null) Instance = this;
        speechImage = GetComponentsInChildren<Image>()[0];
        speakerImage = GetComponentsInChildren<Image>()[1];
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        //StartCoroutine(Entry());
    }

    private void SetDialogueBox(BoxInfo boxInfo)
    {
        speechImage.color = boxInfo.speechColor;
        speakerImage.color = boxInfo.speakerColor;
        rectTransform.position = boxInfo.pos;
        rectTransform.localScale = boxInfo.scale;
    }

    public IEnumerator Entry() //등장
    {
        SetDialogueBox(shrinkBoxInfo);
        //yield return new WaitForSeconds(0.25f);

        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            SetDialogueBox(BoxInfoLerp(shrinkBoxInfo, originalBoxInfo, currentLerpTime / lerpDuration));
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
            SetDialogueBox(BoxInfoLerp(originalBoxInfo, shrinkBoxInfo, currentLerpTime / lerpDuration));
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        //DialogueFrame.Instance.ActivateDialogueFrame(false); //나중에 빼고싶다.
    }

}

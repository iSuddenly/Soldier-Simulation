using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 대화시스템 화살표 애니메이션.
/// 타이핑이펙트 종료 시 애니메이션 실행
/// Fork 분기에서 비활성화
/// </summary>

public class DialogueArrow : MonoBehaviour
{
    DialogueFrame dialogueFrame;

    private Vector2 initialSize = new Vector2(1f, 1f);
    private Vector2 maxSize = new Vector2(1.5f, 1.5f);
    
    void Start()
    {
        dialogueFrame = GetComponentInParent<DialogueFrame>();
        StartCoroutine(AnimateArrow());
    }

    IEnumerator AnimateArrow()
    {
        float t = 1f;
        while (true)
        {
            if (dialogueFrame.isTyping)
            {
                transform.localScale = initialSize;
                yield return new WaitWhile (() => dialogueFrame.isTyping);
            }
            
            if (0.5f <= t && t <= 1f)
                transform.localScale = new Vector2(1f, 1f);
            else if (0f <= t && t < 0.5f)
                transform.localScale = new Vector2(0.9f, 0.9f);
            else if(t < 0f)
                t++;

            t -= Time.deltaTime;
            yield return null;
        }
    }
}

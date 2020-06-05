using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookMove : MonoBehaviour
{
    private Transform deathSpot;
    private float speed = 10f;

    public Image shadow;
    public Image realCook;
    public GameObject speechBubble;

    private Color32 blank = new Color32(255, 255, 255, 0);

    void Start()
    {
        realCook.color = blank;
        deathSpot = GameObject.Find("Spawn_Soldier").transform;
        StartCoroutine(Bounce());
    }

    public IEnumerator Bounce()
    {
        float t = 0.6f;
        while (this.gameObject != null && transform.position.x > deathSpot.position.x)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
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

    public IEnumerator Show()
    {
        float lerpDuration = 0.35f;
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            realCook.color = Color32.Lerp(blank, Color.white, currentLerpTime / lerpDuration);
            shadow.color = Color32.Lerp(Color.white, blank, currentLerpTime / lerpDuration);
            shadow.gameObject.transform.localScale = Vector3.Lerp(new Vector3(1.15f, 1.15f, 1.15f), new Vector3(1.4f, 1.4f, 1.4f), currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
    }

    public IEnumerator Hide()
    {
        float lerpDuration = 0.3f;
        float currentLerpTime = 0f;
        while (currentLerpTime <= lerpDuration)
        {
            realCook.color = Color32.Lerp(Color.white, blank, currentLerpTime / lerpDuration);
            shadow.color = Color32.Lerp(blank, Color.white, currentLerpTime / lerpDuration);
            shadow.gameObject.transform.localScale = Vector3.Lerp(new Vector3(1.4f, 1.4f, 1.4f), new Vector3(1.15f, 1.15f, 1.15f), currentLerpTime / lerpDuration);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}

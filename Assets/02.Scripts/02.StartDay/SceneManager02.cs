using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExtensionMethods;
/// <summary>
/// Set Vacation, Show Menu
/// </summary>
public class SceneManager02 : Manager, IPointerDownHandler
{
    public Text[] menuText;
    public Text[] bigMenuText;
    private Camera cam;
    
    public GameObject menuSmall;
    public GameObject calendarSmall;

    public GameObject menuPanel;
    public GameObject calendarPanel;

    void Start()
    {
        Useful.SetActiveness(false, menuPanel, calendarPanel);
        WriteMenu();
        cam = Camera.main;
        StartCoroutine("Flow");
    }

    IEnumerator Flow()
    {
        yield return new WaitWhile(() => DialogueManager.scene2Monologues.Count == 0);
        DialogueFrame.Instance.AddDialogue(DialogueManager.GetRandomDialogue(DialogueManager.scene2Monologues));
        yield return new WaitUntil(() => Input.anyKeyDown);
        DialogueFrame.Instance.ExecuteDialogue();
        yield return new WaitUntil(() => Input.anyKeyDown);

        while (DialogueFrame.dialogues.Count != 0)
        {
            if (Input.anyKeyDown) DialogueFrame.Instance.DialogueTouched();
            yield return null;
        }
        yield return new WaitUntil(() => Input.anyKeyDown);
        StartCoroutine("ZoomToBoard");
        menuSmall.GetComponent<BoxCollider2D>().enabled = true;
        calendarSmall.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //if (Input.GetMouseButtonDown(0))
    }

    private void WriteMenu()
    {
        for (int i = 0; i < menuText.Length; i++)
        {
            menuText[i].text = FoodManager.todayFoods[i].Name;
            bigMenuText[i].text = FoodManager.todayFoods[i].Name;
        }
    }

    private IEnumerator ZoomToBoard()
    {
        float lerpTime = 1f;
        float currentLerpTime = 0f;

        Vector2 startPos = Vector2.zero;
        Vector2 targetPos = new Vector2(1.28f, 1.87f);

        float startOrthoSize = 4.08f;
        float targetOrthoSize = 1.75f;

        while (currentLerpTime < lerpTime)
        {
            float perc = currentLerpTime / lerpTime;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            cam.transform.position = Vector2.Lerp(startPos, targetPos, perc);
            cam.orthographicSize = Mathf.Lerp(startOrthoSize, targetOrthoSize, perc);

            currentLerpTime += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }
    }
}

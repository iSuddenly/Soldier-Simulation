using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Drawer Movement in Scene 07
/// </summary>
public class Drawer : MonoBehaviour
{
    //Change to Instance
    public bool isDrawerOpened;
    public GameObject drawerPanel;
    public RectTransform drawerRect;
    public Image drawerImage;

    public Sprite drawerFactory;

    public GameObject backButton;

    private float lerpDuration = 0.2f;

    public bool chocoPie;

    
    private void OnEnable()
    {
        drawerPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        print("Drawer Clicked");
        if (!isDrawerOpened) StartCoroutine(SetDrawer(true));
        backButton.SetActive(false);
    }

    private Vector2 drawerOutPos = Vector2.zero;
    private Vector2 drawerInPos = new Vector2(0, 5.8f);
    
    public IEnumerator SetDrawer(bool isOpen)
    {
        if (chocoPie && CalendarManager.passedDays == 1) SceneManager07.Instance.chocoPie.SetActive(isOpen);
        SoundManager.Instance.PlaySound("Drawer");

        drawerImage.sprite = drawerFactory;
        drawerPanel.SetActive(true);
        isDrawerOpened = isOpen;

        float currentLerpTime = 0f;
        float perc;

        Vector2 a, b;
        if (isOpen)
        {
            a = drawerInPos;
            b = drawerOutPos;
        }
        else
        {
            a = drawerOutPos;
            b = drawerInPos;
        }

        while (currentLerpTime <= lerpDuration)
        {
            perc = currentLerpTime / lerpDuration;
            perc = perc * perc * perc * (perc * (6f * perc - 15f) + 10f);
            drawerRect.position = Vector2.Lerp(a, b, perc);
            currentLerpTime += 0.01f;
            yield return new WaitForSeconds(0.01f); //Smoothness
        }
        currentLerpTime = 0f;
        if (!isOpen)
        {
            drawerPanel.SetActive(false);
        }
    }
}
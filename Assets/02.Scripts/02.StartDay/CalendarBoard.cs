using System.Collections;
using UnityEngine;

public class CalendarBoard : MonoBehaviour
{
    public GameObject calendarPanel;
    public GameObject smallMenu;

    public void OnMouseDown()
    {
        calendarPanel.SetActive(true);
        smallMenu.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void BackButton()
    {
        calendarPanel.SetActive(false);
        smallMenu.GetComponent<BoxCollider2D>().enabled = true;

    }
}
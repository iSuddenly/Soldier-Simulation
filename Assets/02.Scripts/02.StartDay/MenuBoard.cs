using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBoard : MonoBehaviour
{
    public GameObject menuPanel;

    public void OnMouseDown()
    {
        menuPanel.SetActive(true);
    }

}
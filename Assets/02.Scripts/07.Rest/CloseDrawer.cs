using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDrawer : MonoBehaviour
{
    public Drawer drawer1;
    public Drawer drawer2;
    public GameObject backButton;

    private void OnMouseDown()
    {
        if(drawer1.isDrawerOpened) StartCoroutine(drawer1.SetDrawer(false));
        if(drawer2.isDrawerOpened) StartCoroutine(drawer2.SetDrawer(false));
        backButton.SetActive(true);
    }
}

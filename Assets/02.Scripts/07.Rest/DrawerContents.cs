using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerContents : MonoBehaviour
{
    public Drawer drawer1;
    public Drawer drawer2;

    private void OnMouseDown()
    {
        //if (drawer1.isDrawerOpened) ; //휴가증, 초코파이 등 사용
        if (drawer2.isDrawerOpened) RoutineManager.MoveScene();
    }
}

using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject managerFactory;

    public void NextScene()
    {
        RoutineManager.MoveScene();
    }
    
}

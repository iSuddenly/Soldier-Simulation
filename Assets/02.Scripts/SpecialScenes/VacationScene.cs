using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacationScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3.0f);
        RoutineManager.MoveScene(1);
    }
}

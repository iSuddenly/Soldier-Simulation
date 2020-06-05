using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Decide Daily Scene Routine, Manage Scene Movement
/// </summary>
public static class RoutineManager
{
    private static int routineIndex = 0;

    public static List<int> routine = new List<int>();
    private static List<int> normalRoutine = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });

    public static List<int> Routine
    {
        get { return routine; }
        private set {
            //routine = value;

            switch (value) //QuestManager에서 퀘스트번호를 가져옴
            {
                default:
                    routine = normalRoutine;
                    break;
            }
        }
    }

    public static void MoveScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void MoveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void QuitGame()
    {
        Application.Quit();
        //Saving
    }

}

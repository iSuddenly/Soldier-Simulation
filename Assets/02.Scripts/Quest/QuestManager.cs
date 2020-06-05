using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    List<string> lines = new List<string>();
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //private static List<>
}
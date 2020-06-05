using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
/// <summary>
/// Load managers in order
/// </summary>
public class ManagerLoader : MonoBehaviour
{
    public static ManagerLoader Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
    {
        yield return new WaitWhile(() => FoodManager.foodDict == null);
        FoodManager.ReadFoods();
        FoodManager.SetTodaysMenu();
        SoldierManager.ReadSoldiers();
        SoldierManager.SetMoreSoldiers();
        SoldierManager.SetSoldierTodayList();
        DialogueManager.LoadTexts();
        DialogueManager.Read();
        DialogueManager.ReadD();

        yield return null;
    }
}

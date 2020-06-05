using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryData : MonoBehaviour
{
    public static CarryData Instance;

    public int result; //1, 2, 3, 4 the higher the better
    public int[] leftFoodAmount = new int[5]; //남은 음식 양

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    IEnumerator Start()
    {
        int totalLeft = 0;
        for (int i = 0; i < leftFoodAmount.Length; i++)
        {
            totalLeft += leftFoodAmount[i];
        }

        if(totalLeft >= 30)
        {
            result = 1;
        }
        else if(totalLeft >= 20)
        {
            result = 2;
        }
        else if (totalLeft >= 10)
        {
            result = 3;
        }
        else
        {
            result = 4;
        }
        yield return new WaitWhile(() => SceneManager.GetActiveScene().buildIndex != 1);
        Destroy(gameObject);
    }
    
}

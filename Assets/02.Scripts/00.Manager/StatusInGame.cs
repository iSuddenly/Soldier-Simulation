using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 필요한가?
/// </summary>
public class StatusInGame : MonoBehaviour
{
    public static StatusInGame Instance;

    public static int playerRank = 1;

    [Range(0.0f, 100.0f)]
    public float health;

    [Range(0.0f, 100.0f)]
    public float reputation;

    //[Range(0.0f, 100.0f)]
    //public float[] affinities;
    
    public Dictionary<string, float> affinity;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void LoadStatus() //
    {

    }

    private void ResetStatus()
    {
        health = 80.0f;
        reputation = 50.0f;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faster : MonoBehaviour
{
    [Range (1.0f, 10.0f)]
    public float speed = 1.0f;

    void Start()
    {
        
    }
    #if UNITY_EDITOR
    void Update()
    {
        
        Time.timeScale = speed;
        
    }
    #endif
}

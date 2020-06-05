using UnityEngine;

public class SetResolution : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(504, 896, false);
    }
}
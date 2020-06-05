using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private string path = "Sound/";

    AudioClip anger, crazyGuy, dryCough, fairy, footStep, drawer, mistake, pageFlip, peopleNoise, splash, toast, wildBoar, writing;
    AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null) Instance = this;

        audioSource = gameObject.GetComponent<AudioSource>();

        anger = Resources.Load<AudioClip>("Anger");
        crazyGuy = Resources.Load<AudioClip>("CrazyGuy");
        dryCough = Resources.Load<AudioClip>(path+"DryCough");
        fairy = Resources.Load<AudioClip>(path + "Fairy");
        footStep = Resources.Load<AudioClip>(path + "FootStep");
        drawer = Resources.Load<AudioClip>(path + "Drawer");
        mistake = Resources.Load<AudioClip>(path + "Mistake");
        pageFlip = Resources.Load<AudioClip>(path + "PageFlip");
        peopleNoise = Resources.Load<AudioClip>(path + "PeopleNoise");
        splash = Resources.Load<AudioClip>(path + "Splash");
        toast = Resources.Load<AudioClip>(path + "Toast");
        wildBoar = Resources.Load<AudioClip>(path + "WildBoar");
        writing = Resources.Load<AudioClip>(path + "Writing");

    }

    public void PlaySound(string clip)
    {
        //Too much nogada
        switch (clip)
        {
            case "Anger":
                audioSource.PlayOneShot(anger);
                break;
            case "CrazyGuy":
                audioSource.PlayOneShot(crazyGuy);
                break;
            case "DryCough":
                audioSource.PlayOneShot(dryCough);
                break;
            case "Fairy":
                audioSource.PlayOneShot(fairy);
                break;
            case "FootStep":
                audioSource.PlayOneShot(footStep);
                break;
            case "Drawer":
                audioSource.PlayOneShot(drawer);
                break;
            case "Mistake":
                audioSource.PlayOneShot(mistake);
                break;
            case "PageFlip":
                audioSource.PlayOneShot(pageFlip);
                break;
            case "PeopleNoise":
                audioSource.PlayOneShot(peopleNoise);
                break;
            case "Splash":
                audioSource.PlayOneShot(splash);
                break;
            case "Toast":
                audioSource.PlayOneShot(toast);
                break;
            case "WildBoar":
                audioSource.PlayOneShot(wildBoar);
                break;
            case "Writing":
                audioSource.PlayOneShot(writing);
                break;
        }
    }
}

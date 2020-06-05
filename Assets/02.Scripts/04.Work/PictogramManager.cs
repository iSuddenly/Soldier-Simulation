using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictogramManager : MonoBehaviour
{
    public static PictogramManager Instance;
    
    public List<GameObject> pictograms; //게임오브젝트로 안하고 픽토그램 클래스 자체로 못하나?

    public Transform pictogramHouse; 
    public GameObject pictogramFactory;

    public Transform spawnPoint;

    public bool nextOneTrigger;

    public int pictogramCount = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (nextOneTrigger)
        {
            NextOne();
            nextOneTrigger = false;
        }
    }

    public void NextOne()
    {
        //Queue로 하는 게 나을 것 같
        for (int i = 0; i < pictograms.Count; i++)
        {
            if(pictograms[i] != null) pictograms[i].GetComponent<Pictogram>().StartCoroutine("Move");
        }

        try
        {
            pictograms.RemoveAt(0);

        }
        catch(Exception ex)
        {
            print(ex);
        }

        if(SoldierManager.soldierNum - 1 > pictogramCount)
        {
            GameObject newOne = Instantiate(pictogramFactory, spawnPoint.transform.position, Quaternion.identity, pictogramHouse);
            pictograms.Add(newOne);
            pictogramCount++;
        }
        
    }

}
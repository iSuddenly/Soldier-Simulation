using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareWinterFood : MonoBehaviour
{
    public static PrepareWinterFood Instance;

    public GameObject cookFactory;
    public Sprite[] cookImages = new Sprite[5]; //Cook Prefab
    public Transform bgCanvas; //Cook's parent

    public Transform rightSpawn;
    public Transform stopSpot;
    public Transform leftSpawn;

    public SpriteRenderer riceHouse, gookHouse;
    public Transform banchan1Mother, banchan2Mother, banchan3Mother;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        StartCoroutine(BringFood());
    }

    private IEnumerator BringFood()
    {
        GameObject[] cook = new GameObject[5];
        for (int i = 0; i < cookImages.Length; i++)
        {
            cook[i] = Instantiate(cookFactory, rightSpawn.position, Quaternion.identity);
            cook[i].transform.GetChild(1).GetComponent<Image>().sprite = cookImages[i];
            yield return new WaitWhile(() => cook[i].transform.position.x > stopSpot.position.x);
            cook[i].GetComponent<CookMove>().StopAllCoroutines();
            if (i == 2) BaechicButton.Instance.MoveBaechicdae();
            StartCoroutine(cook[i].GetComponent<CookMove>().Show());
            yield return new WaitForSeconds(0.5f);
            if (i == 4) BaechicButton.Instance.MoveBaechicdae();
            StartCoroutine(cook[i].GetComponent<CookMove>().Hide());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(cook[i].GetComponent<CookMove>().Bounce());
            yield return new WaitWhile(() => cook[i].transform.position.x > leftSpawn.position.x);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < cook.Length; i++) Destroy(cook[i]);
        SceneManager04.Instance.StartMainGame();
    }
    
}

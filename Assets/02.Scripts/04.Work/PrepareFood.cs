using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareFood : MonoBehaviour
{
    public static PrepareFood Instance;

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
            FillFood(i);
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

    private void FillFood(int i)
    {
        Quaternion Quat()
        {
            if(FoodManager.todayFoods[i].IsRotate) return Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            else return Quaternion.identity;
        }

        Vector3 prefabSize = SceneManager04.Instance.foodFactory.transform.localScale;

        switch (i)
        {
            case 0: //밥
                riceHouse.sprite = SceneManager04.Instance.foodSprites[0];
                riceHouse.sortingOrder = 8;
                break;
            case 1: //국
                gookHouse.sprite = SceneManager04.Instance.foodSprites[1];
                gookHouse.sortingOrder = 8;
                break;
            case 2: //반찬1
                for (int j = 0; j < SceneManager04.foodAmount[2]; j++)
                {
                    GameObject side = Instantiate(SceneManager04.Instance.foodFactory, RandomPointInObject(banchan1Mother, true), Quat(), banchan1Mother);
                    side.GetComponent<SpriteRenderer>().sprite = SceneManager04.Instance.foodSprites[2];
                    side.transform.localScale = prefabSize * FoodManager.todayFoods[2].SpriteSize;
                    SetBoxCollider(side);
                    side.tag = "Banchan1";
                }
                break;
            case 3: //반찬2
               for (int j = 0; j < SceneManager04.foodAmount[3]; j++)
                {
                    GameObject side = Instantiate(SceneManager04.Instance.foodFactory, RandomPointInObject(banchan2Mother, true), Quat(), banchan2Mother);
                    side.GetComponent<SpriteRenderer>().sprite = SceneManager04.Instance.foodSprites[3];
                    side.transform.localScale = prefabSize * FoodManager.todayFoods[3].SpriteSize;
                    SetBoxCollider(side);
                    side.tag = "Banchan2";
                }
                break;
            case 4: //반찬3
                for (int j = 0; j < SceneManager04.foodAmount[4]; j++)
                {
                    GameObject side = Instantiate(SceneManager04.Instance.foodFactory, RandomPointInObject(banchan3Mother, false), Quat(), banchan3Mother);
                    side.GetComponent<SpriteRenderer>().sprite = SceneManager04.Instance.foodSprites[4];
                    side.transform.localScale = prefabSize * FoodManager.todayFoods[4].SpriteSize;
                    SetBoxCollider(side);
                    side.tag = "Banchan3";
                }
                break;
        }
    }

    private void SetBoxCollider(GameObject go)
    {
        Vector2 s = SceneManager04.Instance.foodSprites[2].bounds.size;
        go.GetComponent<BoxCollider2D>().size = s;
        go.GetComponent<BoxCollider2D>().offset = new Vector2((/*s.x / 2*/0), 0);
    }

    private Vector3 RandomPointInObject(Transform mother, bool isGaro)
    {
        float x = mother.position.x;
        float y = mother.position.y;
        float scaleX = mother.localScale.x;
        float scaleY = mother.localScale.y;

        float mulToX, mulToY;
        if (isGaro) { mulToX = 0.4f; mulToY = 0.2f; }
        else { mulToX = 0.125f; mulToY = 0.45f; }
        return new Vector3(UnityEngine.Random.Range(x - scaleX * mulToX, x + scaleX * mulToX), UnityEngine.Random.Range(y - scaleY * mulToY, y + scaleY * mulToY), -0.1f);
    }

}

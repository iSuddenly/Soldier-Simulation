using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance = null;

    public bool isDragging = false;
    public GameObject draggingObj;

    public GameObject foodOnSikpan;

    public GameObject sikpan;

    public GameObject gookja;
    public GameObject jugeok;

    public GameObject dropEffect;

    Camera cam;

    int layerMask;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        gookja.SetActive(false);
        jugeok.SetActive(false);
        dropEffect.SetActive(false);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, layerMask);

        if(Input.GetMouseButtonDown(0))
        {
            if (hit.collider == null) return;
            else print("Click Detected : " + hit.collider.gameObject);

            if(hit.collider.CompareTag("Banchan1") || hit.collider.CompareTag("Banchan2") || hit.collider.CompareTag("Banchan3")) //반찬 클
            {
                isDragging = true;
                draggingObj = hit.collider.gameObject;
                draggingObj.GetComponent<SpriteRenderer>().sortingOrder = 10;
            }
            else if(hit.collider.CompareTag("Gook")) //국 클릭
            {
                isDragging = true;
                gookja.SetActive(true);
                draggingObj = gookja;
                
            }
            else if (hit.collider.CompareTag("Rice")) //밥 클릭
            {
                isDragging = true;
                jugeok.SetActive(true);
                draggingObj = jugeok;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (draggingObj == jugeok) //주걱을 끌고 있을 때
            {
                jugeok.SetActive(false);
                switch (hit.collider.gameObject.name)
                {
                    case "RiceCollider":
                        FillFood(0);
                        break;
                    default:
                        StartCoroutine(Dropped(hit.point));
                        //반찬 버리는 함수
                        break;
                }
            }
            else if (draggingObj == gookja) //국자를 끌고 있을 때
            {
                gookja.SetActive(false);
                switch (hit.collider.gameObject.name)
                {
                    case "RiceCollider":
                        break;
                    case "GookCollider":
                        FillFood(1);
                        break;
                    default:
                        StartCoroutine(Dropped(hit.point));
                        break;
                }
            }
            else if (draggingObj != null && draggingObj.layer == 8) //반찬을 끌고 있을 때
            {
                print("Dropped at : " + hit.collider.gameObject);
                switch (hit.collider.gameObject.name)
                {
                    case "GookCollider":
                        Destroy(draggingObj);
                        StartCoroutine(Dropped(hit.point));

                        break;
                    case "RiceCollider":
                        Destroy(draggingObj);
                        StartCoroutine(Dropped(hit.point));

                        break;
                    case "Banchan1Collider":
                        SikpanManager.Instance.PourBanchan(draggingObj, 0);
                        break;
                    case "Banchan2Collider":
                        SikpanManager.Instance.PourBanchan(draggingObj, 1);
                        break;
                    case "Banchan3Collider":
                        SikpanManager.Instance.PourBanchan(draggingObj, 2);
                        break;
                    default:
                        Destroy(draggingObj);
                        break;
                }
            }
            else if (hit.collider.tag == "BaechicTong1") //반찬통에 떨어트렸을 경우
            {
                if (draggingObj != null) draggingObj.GetComponent<Collider2D>().enabled = true;

                //원래 반찬통이면 원상복구
                //다른 반찬통이면 반찬 날라감
            }
            else if (hit.collider.tag == "BG" || hit.collider.tag == null) //빈 곳에 떨어트렸을 경우
            {
                StartCoroutine(Dropped(hit.point));
                Destroy(draggingObj); //반찬 날라감
                if (draggingObj != null) draggingObj.GetComponent<Collider2D>().enabled = true;
            }
            else
            {

                if (draggingObj != null) draggingObj.GetComponent<Collider2D>().enabled = true;

            }

            isDragging = false;
            //if(draggingObj != null) draggingObj.GetComponent<Collider2D>().enabled = true;
            draggingObj = null;

        }

        if (isDragging)
        {
            draggingObj.transform.position = hit.point; //로테이션은 수정 안되게. 일치하도록 따라오는게 아니라 처음에 클릭한 부분이랑 거리차이 유지하면서 따라오도록
            draggingObj.GetComponent<Collider2D>().enabled = false;
            layerMask = ~(LayerMask.GetMask("Food"));
        }
        else
        {
            layerMask = LayerMask.GetMask("Food"); 
        }

    }

    public void FillFood(int whichFood)
    {
        SceneManager04.Instance.currentSoldier.GetComponent<SoldierMove>().eachFulfill[whichFood]++;
        switch (whichFood)
        {
            case 0:
                SikpanManager.Instance.PourRice();
                SceneManager04.Instance.GiveFood(0);
                break;
            case 1:
                SikpanManager.Instance.PourGook();
                SceneManager04.Instance.GiveFood(1);
                break;
        }
    }

    IEnumerator Dropped(Vector2 droppedPos)
    {
        dropEffect.transform.position = droppedPos;
        dropEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        dropEffect.SetActive(false);
    }

}

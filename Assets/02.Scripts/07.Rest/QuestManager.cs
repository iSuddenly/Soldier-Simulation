//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class QuestManager : MonoBehaviour
//{
//    //호감도에 따라 오늘 실행 가능한 퀘스트 목록 가져오기
//    //퀘스트 중 몇가지를 추리기

//    /*퀘스트의 종류*/
//    //1. 메인퀘스트(기간동안 진행) : 선택의 유무와 관계없이 반드시 나오는 필수 퀘스트
//    //2. 이벤트(당일 종료) : 대화만 해서 호감도 높이는 것도 있고, 씬을 아예 옮기는 것도 있다.
//    //3. 퀘스트(다음날에 영향) : 

//    public static QuestManager Instance;

//    public GameObject questGiverFactory;
//    public List<GameObject> questGivers = new List<GameObject>();

//    private void Awake()
//    {
//        if (Instance == null) Instance = this;
//    }

//    private void Start()
//    {
//        CreateQuestGivers(GetQuestIndices());
//    }
    
//    void Update()
//    {
        
//    }

//    int[] GetQuestIndices()
//    {
//        int[] availableQuestIndices; //호감도 조건을 충족해서 실행할 수 있는 퀘스트 목록

//        int[] choosenQuestIndices = { 4, 6, 24, 10 }; //실행할 수 있는 퀘스트 목록 중 최대 4~5개를 추려 랜덤화 한 것. 일단은 임시로 넣어둠.
//        //혹은 퀘스트 목록에 우선순위를 줄까? 위쪽에 있을 수록 실행확률 높게?
//        //생성 스팟은 어떻게 정하나.
//        return choosenQuestIndices;
//    }

//    void CreateQuestGivers(int[] questIndices) //이렇게 인덱스를 받아오는게 아니라 퀘스트 클래스변수 자체를 받아와야 할지도 몰라.
//    {
//        for (int i = 0; i < questIndices.Length; i++)
//        {
//            GameObject questGiver = Instantiate(questGiverFactory);
//            questGiver.GetComponent<QuestGiver>().questNo = questIndices[i];
//            questGivers.Add(questGiver);
//        }
        
//    }

//    public void ExecuteQuest(int questNo)
//    {
//        RemoveDoneQuest(questNo);
//        //StartCoroutine("Quest" + questNo.ToString());
//    }

//    public void RemoveDoneQuest(int questNo) //SubQuestCSV에서 완료여부 참으로
//    {
        
//    }
//}

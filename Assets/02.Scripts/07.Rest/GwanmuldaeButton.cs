using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwanmuldaeButton : MonoBehaviour
{
    public GameObject gwanmuldaePanel;
    
    void Start()
    {
        gwanmuldaePanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        //딴거 진행중이 아니라면
        if (!DialogueFrame.Instance.isDialogueOnGoing)
        {
            gwanmuldaePanel.SetActive(true);
        }

    }

    public void GwanmuldaeActive(bool activeness)
    {
        gwanmuldaePanel.SetActive(activeness);

    }
}

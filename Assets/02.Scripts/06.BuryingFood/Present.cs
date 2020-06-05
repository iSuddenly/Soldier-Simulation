using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager06.Instance.boxClicked = true;
        print("Present Clicked");
    }
}

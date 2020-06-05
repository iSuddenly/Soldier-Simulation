using System.Collections;
using UnityEngine;

public class SayMyPosition : MonoBehaviour
{
    IEnumerator Start()
    {
        while (true)
        {
            print(gameObject.GetComponent<RectTransform>().position);
            yield return new WaitForSeconds(.1f);
        }
    }
}
